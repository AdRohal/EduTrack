using EduTrack.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EduTrack.View
{
    public partial class ClassesSchedule : UserControl
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        public ClassesSchedule()
        {
            InitializeComponent();
            LoadClasses();
            LoadSchedule();
        }

        private async void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxClass.SelectedItem != null)
            {
                string selectedClass = comboBoxClass.SelectedItem.ToString();
                await LoadScheduleForClass(selectedClass);
            }
        }
        private void LoadSchedule()
        {
            var scheduleSlots = new List<ScheduleSlot>();
            for (int hour = 7; hour <= 19; hour++)
            {
                scheduleSlots.Add(new ScheduleSlot { TimeSlot = $"{hour}:00-{hour + 1}:00" });
            }

            scheduleDataGrid.ItemsSource = scheduleSlots;
        }


        private async Task LoadStudentsAsync(string className = null)
        {
            // Implementation needed
            await Task.CompletedTask; // Placeholder to avoid warning
        }

        private async void LoadClasses()
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = "SELECT ClassName FROM classes";

            List<string> classNames = new List<string>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = await command.ExecuteReaderAsync() as MySqlDataReader)
                    {
                        while (await reader.ReadAsync())
                        {
                            string className = reader.GetString("ClassName");
                            classNames.Add(className);
                        }
                    }
                }

                Dispatcher.Invoke(() =>
                {
                    comboBoxClass.ItemsSource = classNames;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadScheduleForClass(string className)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            // Initialize a list to hold the complete schedule, including all time slots.
            List<ScheduleSlot> completeSchedule = new List<ScheduleSlot>();
            // First, populate this list with all time slots, leaving class-specific details empty for now.
            for (int hour = 7; hour <= 19; hour++)
            {
                completeSchedule.Add(new ScheduleSlot { TimeSlot = $"{hour}:00-{hour + 1}:00" });
            }

            string query = $"SELECT cs.* FROM ClassSchedules cs JOIN classes c ON cs.ClassID = c.ClassID WHERE c.ClassName = @ClassName";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", className);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Find the corresponding time slot in completeSchedule and update it with class-specific details.
                                var timeSlot = reader.GetString(reader.GetOrdinal("TimeSlot"));
                                var scheduleSlot = completeSchedule.FirstOrDefault(s => s.TimeSlot == timeSlot);
                                if (scheduleSlot != null)
                                {
                                    scheduleSlot.ScheduleID = reader.GetInt32(reader.GetOrdinal("ScheduleID"));
                                    scheduleSlot.Monday = reader.IsDBNull(reader.GetOrdinal("Monday")) ? null : reader.GetString(reader.GetOrdinal("Monday"));
                                    scheduleSlot.Tuesday = reader.IsDBNull(reader.GetOrdinal("Tuesday")) ? null : reader.GetString(reader.GetOrdinal("Tuesday"));
                                    scheduleSlot.Wednesday = reader.IsDBNull(reader.GetOrdinal("Wednesday")) ? null : reader.GetString(reader.GetOrdinal("Wednesday"));
                                    scheduleSlot.Thursday = reader.IsDBNull(reader.GetOrdinal("Thursday")) ? null : reader.GetString(reader.GetOrdinal("Thursday"));
                                    scheduleSlot.Friday = reader.IsDBNull(reader.GetOrdinal("Friday")) ? null : reader.GetString(reader.GetOrdinal("Friday"));
                                    scheduleSlot.Saturday = reader.IsDBNull(reader.GetOrdinal("Saturday")) ? null : reader.GetString(reader.GetOrdinal("Saturday"));
                                    scheduleSlot.Sunday = reader.IsDBNull(reader.GetOrdinal("Sunday")) ? null : reader.GetString(reader.GetOrdinal("Sunday"));
                                }
                            }
                        }
                    }
                }

                // Finally, set the complete schedule as the ItemsSource for the DataGrid.
                scheduleDataGrid.ItemsSource = completeSchedule;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task InsertDataIntoDatabase(ScheduleSlot scheduleSlot, int classId)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = @"INSERT INTO classschedules (ClassID, TimeSlot, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday) 
                     VALUES (@ClassID, @TimeSlot, @Monday, @Tuesday, @Wednesday, @Thursday, @Friday, @Saturday, @Sunday)";

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", classId);
                    command.Parameters.AddWithValue("@TimeSlot", scheduleSlot.TimeSlot);
                    command.Parameters.AddWithValue("@Monday", (object)scheduleSlot.Monday ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Tuesday", (object)scheduleSlot.Tuesday ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Wednesday", (object)scheduleSlot.Wednesday ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Thursday", (object)scheduleSlot.Thursday ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Friday", (object)scheduleSlot.Friday ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Saturday", (object)scheduleSlot.Saturday ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Sunday", (object)scheduleSlot.Sunday ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        private async void scheduleDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridTextColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    var textBox = e.EditingElement as TextBox;
                    var newValue = textBox.Text;
                    var scheduleSlot = e.Row.Item as ScheduleSlot;

                    try
                    {
                        // Now you have the new value, the day of the week, and the time slot
                        // You can use these to update your database
                        await UpdateDatabase(scheduleSlot, bindingPath, newValue);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while updating the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async Task UpdateDatabase(ScheduleSlot scheduleSlot, string dayOfWeek, string newValue)
        {
            await Task.Yield(); // Placeholder to avoid warning
                                // Future asynchronous database operations go here
        }


        private void AddScheduleEntry_Click(object sender, RoutedEventArgs e)
        {
            // Implementation remains the same as provided
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxClass.SelectedItem is string selectedClassName)
            {
                int classId = await GetClassIdByName(selectedClassName);
                if (classId > 0)
                {
                    // Assuming you have a way to identify or collect all modified ScheduleSlots
                    var modifiedScheduleSlots = scheduleDataGrid.SelectedItems.Cast<ScheduleSlot>().ToList();

                    if (modifiedScheduleSlots.Any())
                    {
                        try
                        {
                            foreach (var scheduleSlot in modifiedScheduleSlots)
                            {
                                await InsertDataIntoDatabase(scheduleSlot, classId);
                            }
                            MessageBox.Show("Schedules updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            await LoadScheduleForClass(selectedClassName); // Refresh the DataGrid
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while inserting data into the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select at least one schedule slot to save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to find the selected class in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a class.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private async Task<int> GetClassIdByName(string className)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = "SELECT ClassID FROM classes WHERE ClassName = @ClassName LIMIT 1";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", className);

                        object result = await command.ExecuteScalarAsync();
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving the class ID: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
        }

    }
}
