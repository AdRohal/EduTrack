using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Xml;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;
using EduTrack.Data;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for Backup.xaml
    /// </summary>
    public partial class Backup : UserControl
    {
        private string filePath;

        public Backup()
        {
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTable = ((ComboBoxItem)comboBox1.SelectedItem).Content.ToString().ToLower();
            switch (selectedTable)
            {
                case "teacher":
                    DownloadTeacherData();
                    break;
                case "employee":
                    DownloadEmployeeData();
                    break;
                case "student":
                    DownloadStudentData();
                    break;
                default:
                    MessageBox.Show("Please select a valid option.");
                    break;
            }
        }

        private void DownloadTeacherData()
        {
            string query = "SELECT * FROM teacher_registry";

            using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
            {
                try
                {
                    connection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EduTrack", "Back UP");

                    Directory.CreateDirectory(directoryPath);

                    string path = Path.Combine(directoryPath, "teacher_registry.xml");

                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        outputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        outputFile.WriteLine("<TeacherData>");

                        foreach (DataRow row in dt.Rows)
                        {
                            outputFile.WriteLine("\t<Teacher>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                string value = row[column].ToString();
                                if (column.DataType == typeof(DateTime))
                                {
                                    DateTime date;
                                    if (DateTime.TryParse(value, out date))
                                    {
                                        value = date.ToString("yyyy-MM-dd");
                                    }
                                }
                                outputFile.WriteLine($"\t\t<{column.ColumnName}>{value}</{column.ColumnName}>");
                            }
                            outputFile.WriteLine("\t</Teacher>");
                        }

                        outputFile.WriteLine("</TeacherData>");
                    }

                    MessageBox.Show("Teacher data has been downloaded as XML.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("An error occurred while connecting to the database: " + ex.Message);
                }
            }
        }

        private void DownloadEmployeeData()
        {
            string query = "SELECT * FROM employee_registry";

            using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
            {
                try
                {
                    connection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EduTrack", "Back UP");

                    Directory.CreateDirectory(directoryPath);

                    string path = Path.Combine(directoryPath, "employee_registry.xml");

                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        outputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        outputFile.WriteLine("<EmployeeData>");

                        foreach (DataRow row in dt.Rows)
                        {
                            outputFile.WriteLine("\t<Employee>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                string value = row[column].ToString();
                                if (column.DataType == typeof(DateTime))
                                {
                                    DateTime date;
                                    if (DateTime.TryParse(value, out date))
                                    {
                                        value = date.ToString("yyyy-MM-dd");
                                    }
                                }
                                outputFile.WriteLine($"\t\t<{column.ColumnName}>{value}</{column.ColumnName}>");
                            }
                            outputFile.WriteLine("\t</Employee>");
                        }

                        outputFile.WriteLine("</EmployeeData>");
                    }

                    MessageBox.Show("Employee data has been downloaded as XML.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("An error occurred while connecting to the database: " + ex.Message);
                }
            }
        }

        private void DownloadStudentData()
        {
            string query = "SELECT * FROM student_registry";

            using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
            {
                try
                {
                    connection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EduTrack", "Back UP");

                    Directory.CreateDirectory(directoryPath);

                    string path = Path.Combine(directoryPath, "student_registry.xml");

                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        outputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        outputFile.WriteLine("<StudentData>");

                        foreach (DataRow row in dt.Rows)
                        {
                            outputFile.WriteLine("\t<Student>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                string value = row[column].ToString();
                                if (column.DataType == typeof(DateTime))
                                {
                                    DateTime date;
                                    if (DateTime.TryParse(value, out date))
                                    {
                                        value = date.ToString("yyyy-MM-dd");
                                    }
                                }
                                outputFile.WriteLine($"\t\t<{column.ColumnName}>{value}</{column.ColumnName}>");
                            }
                            outputFile.WriteLine("\t</Student>");
                        }

                        outputFile.WriteLine("</StudentData>");
                    }

                    MessageBox.Show("Student data has been downloaded as XML.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("An error occurred while connecting to the database: " + ex.Message);
                }
            }
        }
        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                fileNameTextBlock.Text = Path.GetFileName(filePath);
            }
        }
        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file to import.");
                return;
            }

            string tableName = Path.GetFileNameWithoutExtension(filePath);

            DataSet ds = new DataSet();
            ds.ReadXml(filePath);

            using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
            {
                try
                {
                    connection.Open();

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        var values = new List<string>();
                        foreach (var item in row.ItemArray)
                        {
                            string value;
                            if (item is DBNull)
                            {
                                value = "NULL";
                            }
                            else
                            {
                                value = item.ToString().Replace("'", "''");
                                value = value.Replace("\\", "\\\\");
                                value = $"'{value}'";
                            }
                            values.Add(value);
                        }

                        string query = $"INSERT INTO {tableName} ({string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => $"`{c.ColumnName}`"))}) VALUES ({string.Join(", ", values)})";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data has been imported successfully.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("An error occurred while connecting to the database: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}