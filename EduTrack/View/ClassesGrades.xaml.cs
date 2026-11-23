using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// <summary>Enter each student's grade for one module of one class.</summary>
    public partial class ClassesGrades : UserControl
    {
        private const string GradeRule = "Use a number from 0 to 20, or a short word such as ABS.";

        public ClassesGrades()
        {
            InitializeComponent();
            Loaded += async (s, e) => await PopulateClassesComboBox();
        }

        public class Student
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string Grade { get; set; }
        }

        private void SetEmpty(string title, string text)
        {
            EmptyTitle.Text = title;
            EmptyText.Text = text;
        }

        private async Task PopulateClassesComboBox()
        {
            var classes = new List<ClassInfo>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT ClassID, ClassName FROM classes ORDER BY ClassName", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            classes.Add(new ClassInfo { ClassID = reader.GetInt32(0), ClassName = reader.GetString(1) });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The classes could not be loaded. Check that the database is running.\n" + ex.Message, "Can't load classes", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            classesComboBox.ItemsSource = classes;
            classesComboBox.DisplayMemberPath = "ClassName";
            classesComboBox.SelectedValuePath = "ClassID";
            if (classes.Count == 0) SetEmpty("No classes yet", "Create a class first, add students to it, then come back to grade them.");
            else classesComboBox.SelectedIndex = 0;
        }

        private async void classesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (classesComboBox.SelectedValue is int classId) await PopulateModuleComboBox(classId);
        }

        private async Task PopulateModuleComboBox(int classId)
        {
            var modules = new List<Module>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT ModuleID, ModuleName FROM modules WHERE ClassID = @c ORDER BY ModuleName", connection))
                    {
                        command.Parameters.AddWithValue("@c", classId);
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                modules.Add(new Module { ModuleID = reader.GetInt32(0), ModuleName = reader.GetString(1) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The modules could not be loaded.\n" + ex.Message, "Can't load modules", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ModuleComboBox.ItemsSource = modules;
            ModuleComboBox.DisplayMemberPath = "ModuleName";
            ModuleComboBox.SelectedValuePath = "ModuleID";
            if (modules.Count == 0)
            {
                studentsDataGrid.ItemsSource = null;
                SetEmpty("This class has no modules yet", "Add one with the box on the right (for example Algebra), then enter grades here.");
            }
            else
            {
                ModuleComboBox.SelectedIndex = 0;
            }
        }

        private async void ModuleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModuleComboBox.SelectedValue is int moduleId) await LoadStudentsAndGradesForSelectedModule(moduleId);
        }

        /// <summary>Every student of the module's class, with their grade if one has been saved.</summary>
        private async Task LoadStudentsAndGradesForSelectedModule(int moduleId)
        {
            var students = new List<Student>();
            const string query = @"
SELECT sr.StudentID, CONCAT(sr.f_name, ' ', sr.l_name) AS FullName, sm.Grade
FROM modules m
JOIN studentclasses sc ON sc.ClassID = m.ClassID
JOIN student_registry sr ON sr.StudentID = sc.StudentID
LEFT JOIN student_module sm ON sm.StudentID = sr.StudentID AND sm.ModuleID = m.ModuleID
WHERE m.ModuleID = @ModuleID
ORDER BY sr.l_name, sr.f_name";
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ModuleID", moduleId);
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                students.Add(new Student { StudentID = reader.GetInt32(0), FullName = reader.GetString(1), Grade = reader.Str("Grade") ?? "" });
                        }
                    }
                }
                studentsDataGrid.ItemsSource = students;
                if (students.Count == 0) SetEmpty("No students in this class yet", "Add students to the class from All classes, then grade them here.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The students could not be loaded.\n" + ex.Message, "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddModuleButton_Click(object sender, RoutedEventArgs e)
        {
            string moduleName = moduleNameTextBox.Text.Trim();
            if (!(classesComboBox.SelectedValue is int classId) || moduleName.Length == 0)
            {
                MessageBox.Show("Choose a class and type the name of the module first.", "Module not added", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("INSERT INTO modules (ModuleName, ClassID) VALUES (@n, @c)", connection))
                    {
                        command.Parameters.AddWithValue("@n", moduleName);
                        command.Parameters.AddWithValue("@c", classId);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                MessageBox.Show("Module added successfully.");
                moduleNameTextBox.Clear();
                await PopulateModuleComboBox(classId);
                ModuleComboBox.SelectedIndex = ModuleComboBox.Items.Count - 1;   // jump to the module just added
            }
            catch (Exception ex)
            {
                MessageBox.Show("The module could not be added.\n" + ex.Message, "Couldn't add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveGradeButton_Click(object sender, RoutedEventArgs e)
        {
            studentsDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            if (!(ModuleComboBox.SelectedValue is int moduleId))
            {
                MessageBox.Show("Choose a module first.", "No module chosen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var students = (studentsDataGrid.ItemsSource as IEnumerable<Student>)?.ToList() ?? new List<Student>();
            foreach (var student in students)
            {
                if (!IsValidGrade(student.Grade))
                {
                    MessageBox.Show("The grade for " + student.FullName + " isn't valid. " + GradeRule, "Check the grades", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var student in students)
                        {
                            string grade = (student.Grade ?? "").Trim().Replace(',', '.');
                            string sql = grade.Length == 0
                                ? "DELETE FROM student_module WHERE StudentID = @s AND ModuleID = @m"
                                : "INSERT INTO student_module (StudentID, ModuleID, Grade) VALUES (@s, @m, @g) ON DUPLICATE KEY UPDATE Grade = VALUES(Grade)";
                            using (var command = new MySqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@s", student.StudentID);
                                command.Parameters.AddWithValue("@m", moduleId);
                                if (grade.Length > 0) command.Parameters.AddWithValue("@g", grade);
                                await command.ExecuteNonQueryAsync();
                            }
                        }
                        transaction.Commit();
                    }
                }
                MessageBox.Show("Grades saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The grades could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool IsValidGrade(string grade)
        {
            grade = (grade ?? "").Trim().Replace(',', '.');
            if (grade.Length == 0) return true;
            if (decimal.TryParse(grade, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value)) return value >= 0 && value <= 20;
            return grade.Length <= 10 && grade.All(char.IsLetter);   // for example ABS
        }
    }
}
