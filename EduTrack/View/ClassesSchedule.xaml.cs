using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    /// <summary>Weekly timetable for one class. Edit cells directly, then save the whole week at once.</summary>
    public partial class ClassesSchedule : UserControl
    {
        private const int FirstHour = 7;
        private const int LastHour = 19;

        private bool _dirty;
        private bool _switching;
        private string _loadedClass;

        public ClassesSchedule()
        {
            InitializeComponent();
            scheduleDataGrid.ItemsSource = BlankWeek();
            Loaded += async (s, e) => await LoadClassesAsync();
        }

        private static List<ScheduleSlot> BlankWeek()
        {
            var slots = new List<ScheduleSlot>();
            for (int hour = FirstHour; hour <= LastHour; hour++)
                slots.Add(new ScheduleSlot { TimeSlot = $"{hour}:00-{hour + 1}:00" });
            return slots;
        }

        private void SetDirty(bool dirty)
        {
            _dirty = dirty;
            DirtyText.Visibility = dirty ? Visibility.Visible : Visibility.Collapsed;
            SaveBtn.IsEnabled = dirty && comboBoxClass.SelectedItem != null;
        }

        private async Task LoadClassesAsync()
        {
            var names = new List<string>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT ClassName FROM classes ORDER BY ClassName", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) names.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The classes could not be loaded. Check that the database is running.\n" + ex.Message, "Can't load classes", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            comboBoxClass.ItemsSource = names;
            EmptyState.Visibility = names.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            if (names.Count > 0) comboBoxClass.SelectedIndex = 0;
        }

        private async void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_switching) return;
            string selected = comboBoxClass.SelectedItem as string;
            if (selected == null) return;

            if (_dirty && _loadedClass != null && selected != _loadedClass)
            {
                var answer = MessageBox.Show("You have unsaved changes to " + _loadedClass + ". Switch class and lose them?",
                    "Unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (answer != MessageBoxResult.Yes)
                {
                    _switching = true;
                    comboBoxClass.SelectedItem = _loadedClass;
                    _switching = false;
                    return;
                }
            }
            await LoadScheduleForClass(selected);
        }

        private async Task LoadScheduleForClass(string className)
        {
            var week = BlankWeek();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    const string sql = "SELECT cs.* FROM classschedules cs JOIN classes c ON cs.ClassID = c.ClassID WHERE c.ClassName = @n ORDER BY cs.ScheduleID";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@n", className);
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var slot = week.FirstOrDefault(s => s.TimeSlot == reader.Str("TimeSlot"));
                                if (slot == null) continue;
                                slot.ScheduleID = reader.GetInt32(reader.GetOrdinal("ScheduleID"));
                                slot.Monday = reader.Str("Monday");
                                slot.Tuesday = reader.Str("Tuesday");
                                slot.Wednesday = reader.Str("Wednesday");
                                slot.Thursday = reader.Str("Thursday");
                                slot.Friday = reader.Str("Friday");
                                slot.Saturday = reader.Str("Saturday");
                                slot.Sunday = reader.Str("Sunday");
                            }
                        }
                    }
                }
                scheduleDataGrid.ItemsSource = week;
                _loadedClass = className;
                SetDirty(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The schedule could not be loaded.\n" + ex.Message, "Can't load schedule", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void scheduleDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit) SetDirty(true);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            scheduleDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            string className = comboBoxClass.SelectedItem as string;
            if (className == null)
            {
                MessageBox.Show("Choose a class first.", "No class chosen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var slots = (scheduleDataGrid.ItemsSource as IEnumerable<ScheduleSlot>)?.ToList() ?? new List<ScheduleSlot>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    int classId;
                    using (var idCommand = new MySqlCommand("SELECT ClassID FROM classes WHERE ClassName = @n LIMIT 1", connection))
                    {
                        idCommand.Parameters.AddWithValue("@n", className);
                        object result = await idCommand.ExecuteScalarAsync();
                        if (result == null) throw new InvalidOperationException("The class no longer exists.");
                        classId = Convert.ToInt32(result);
                    }

                    // Rewrite the week in one go: removes old duplicates and keeps empty hours out of the table.
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var delete = new MySqlCommand("DELETE FROM classschedules WHERE ClassID = @c", connection, transaction))
                        {
                            delete.Parameters.AddWithValue("@c", classId);
                            await delete.ExecuteNonQueryAsync();
                        }
                        foreach (var slot in slots.Where(HasAnyContent))
                        {
                            const string insert = "INSERT INTO classschedules (ClassID, TimeSlot, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday) " +
                                                  "VALUES (@c, @t, @mo, @tu, @we, @th, @fr, @sa, @su)";
                            using (var command = new MySqlCommand(insert, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@c", classId);
                                command.Parameters.AddWithValue("@t", slot.TimeSlot);
                                command.Parameters.AddWithValue("@mo", Clean(slot.Monday));
                                command.Parameters.AddWithValue("@tu", Clean(slot.Tuesday));
                                command.Parameters.AddWithValue("@we", Clean(slot.Wednesday));
                                command.Parameters.AddWithValue("@th", Clean(slot.Thursday));
                                command.Parameters.AddWithValue("@fr", Clean(slot.Friday));
                                command.Parameters.AddWithValue("@sa", Clean(slot.Saturday));
                                command.Parameters.AddWithValue("@su", Clean(slot.Sunday));
                                await command.ExecuteNonQueryAsync();
                            }
                        }
                        transaction.Commit();
                    }
                }
                SetDirty(false);
                MessageBox.Show("Schedule saved for " + className + ".");
                await LoadScheduleForClass(className);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The schedule could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool HasAnyContent(ScheduleSlot s) =>
            new[] { s.Monday, s.Tuesday, s.Wednesday, s.Thursday, s.Friday, s.Saturday, s.Sunday }.Any(v => !string.IsNullOrWhiteSpace(v));

        private static object Clean(string value) => string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
    }
}
