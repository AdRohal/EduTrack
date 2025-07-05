using EduTrack.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EduTrack.Data;

namespace EduTrack.View
{
    public partial class ClassesGrades : UserControl
    {
        public ClassesGrades()
        {
            InitializeComponent();
            Loaded += ClassesGrades_Loaded;
        }

        private async void ClassesGrades_Loaded(object sender, RoutedEventArgs e)
        {
            await PopulateClassesComboBox();
        }

        private async Task PopulateClassesComboBox()
        {
            string query = "SELECT ClassID, ClassName FROM classes";
            List<ClassInfo> classInfoList = new List<ClassInfo>();

            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Fix for Problem 1 & 2: Explicit cast to MySqlDataReader
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                classInfoList.Add(new ClassInfo
                                {
                                    ClassID = reader.GetInt32("ClassID"),
                                    ClassName = reader.GetString("ClassName")
                                });
                            }
                        }
                    }
                }

                classesComboBox.ItemsSource = classInfoList;
                classesComboBox.DisplayMemberPath = "ClassName";
                classesComboBox.SelectedValuePath = "ClassID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void classesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (classesComboBox.SelectedValue is int selectedClassId)
            {
                await PopulateModuleComboBox(selectedClassId);
                await LoadStudentsForClass(selectedClassId);
            }
        }


        private async Task PopulateModuleComboBox(int classId)
        {
            string query = $"SELECT ModuleID, ModuleName FROM modules WHERE ClassID = {classId}";
            List<Module> moduleList = new List<Module>();

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Fix for Problem 1 & 2: Explicit cast to MySqlDataReader
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                moduleList.Add(new Module
                                {
                                    ModuleID = reader.GetInt32("ModuleID"),
                                    ModuleName = reader.GetString("ModuleName")
                                });
                            }
                        }
                    }
                }

                ModuleComboBox.ItemsSource = moduleList;
                ModuleComboBox.DisplayMemberPath = "ModuleName";
                ModuleComboBox.SelectedValuePath = "ModuleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ModuleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModuleComboBox.SelectedValue is int selectedModuleId)
            {
                await LoadStudentsForModule(selectedModuleId);
                await LoadStudentsAndGradesForSelectedModule(selectedModuleId);
            }
        }

        public class Student
        {
            public int StudentID { get; set; } // Ensure this property exists
            public string FullName { get; set; } // Ensure this property exists
            public string Grade { get; set; } // Ensure this property exists
        }

        private async Task LoadStudentsForModule(int moduleId)
        {
            await Task.Delay(1000);
        }

        private async Task LoadStudentsForClass(int classId)
        {
            List<Student> students = new List<Student>();
            string query = @"
SELECT sr.StudentID, CONCAT(sr.f_name, ' ', sr.l_name) AS FullName
FROM student_registry sr
JOIN studentclasses sc ON sr.StudentID = sc.StudentID
WHERE sc.ClassID = @ClassID";

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", classId);

                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                students.Add(new Student
                                {
                                    StudentID = reader.GetInt32("StudentID"),
                                    FullName = reader.GetString("FullName")
                                });
                            }
                        }

                    }
                }

                studentsDataGrid.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadStudentsAndGradesForSelectedModule(int moduleId)
        {
            List<Student> students = new List<Student>();
            string query = @"
SELECT sr.StudentID, CONCAT(sr.f_name, ' ', sr.l_name) AS FullName, sm.Grade
FROM student_registry sr
LEFT JOIN student_module sm ON sr.StudentID = sm.StudentID AND sm.ModuleID = @ModuleID
WHERE sr.StudentID IN (
    SELECT StudentID
    FROM student_module
    WHERE ModuleID = @ModuleID
)
ORDER BY FullName";

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ModuleID", moduleId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var student = new Student
                                {
                                    // Use column indices
                                    StudentID = reader.GetInt32(0), // Assuming StudentID is the first column
                                    FullName = reader.GetString(1), // Assuming FullName is the second column
                                    Grade = reader.IsDBNull(2) ? "" : reader.GetString(2) // Assuming Grade is the third column
                                };
                                students.Add(student);
                            }
                        }
                    }
                }

                studentsDataGrid.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }







        private async void AddModuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (classesComboBox.SelectedValue is int selectedClassId && !string.IsNullOrWhiteSpace(moduleNameTextBox.Text))
            {
                var moduleName = moduleNameTextBox.Text.Trim();
                try
                {
                    using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                    {
                        await connection.OpenAsync();
                        string query = "INSERT INTO modules (ModuleName, ClassID) VALUES (@ModuleName, @ClassID)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ModuleName", moduleName);
                            command.Parameters.AddWithValue("@ClassID", selectedClassId);

                            int result = await command.ExecuteNonQueryAsync();
                            if (result > 0)
                            {
                                MessageBox.Show("Module added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                moduleNameTextBox.Clear();
                                await PopulateModuleComboBox(selectedClassId);
                            }
                            else
                            {
                                MessageBox.Show("Failed to add the module.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a class and enter a module name.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void SaveGradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ModuleComboBox.SelectedValue is int selectedModuleId)
            {
                foreach (var item in studentsDataGrid.Items)
                {
                    // Check if the item is of type Student before casting
                    if (item is Student student)
                    {
                        string grade = student.Grade; // Grade comes directly from the DataGrid binding

                        string query = @"
INSERT INTO student_module (StudentID, ModuleID, Grade)
VALUES (@StudentID, @ModuleID, @Grade)
ON DUPLICATE KEY UPDATE Grade = @Grade";

                        try
                        {
                            using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                            {
                                await connection.OpenAsync();
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@StudentID", student.StudentID);
                                    command.Parameters.AddWithValue("@ModuleID", selectedModuleId);
                                    command.Parameters.AddWithValue("@Grade", grade);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                MessageBox.Show("Grades saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a module.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }






        // Placeholder for a method to show a dialog and return module details
        private ModuleDetails ShowAddModuleDialog()
        {
            // Implement the dialog display logic here
            // Return the module details as a ModuleDetails object
            return new ModuleDetails
            {
                ModuleName = "Example Module",
                ClassID = 1
            };
        }

        // Placeholder class for module details
        public class ModuleDetails
        {
            public string ModuleName { get; set; }
            public int ClassID { get; set; }
        }

    }
}