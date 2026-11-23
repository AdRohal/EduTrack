using System;
using EduTrack.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    /// <summary>Export a list to XML and restore it later. Only the three people tables can be exported or restored.</summary>
    public partial class Backup : UserControl
    {
        private static readonly Dictionary<string, string> Tables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Students", "student_registry" },
            { "Teachers", "teacher_registry" },
            { "Employees", "employee_registry" }
        };

        // Older versions wrote <StudentData><Student>...; map those root names too.
        private static readonly Dictionary<string, string> LegacyNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Student", "student_registry" }, { "Teacher", "teacher_registry" }, { "Employee", "employee_registry" }
        };

        private string filePath;
        private string lastExport;

        public Backup()
        {
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            string label = (comboBox1.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (label == null || !Tables.TryGetValue(label, out string table))
            {
                MessageBox.Show("Choose what to save first.", "Nothing chosen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new SaveFileDialog
            {
                Title = Loc.T("Save " + label.ToLower()),
                Filter = "XML files (*.xml)|*.xml",
                FileName = table + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".xml",
                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EduTrack")
            };
            if (dialog.ShowDialog() != true) return;

            try
            {
                var data = new DataTable(table);
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();
                    new MySqlDataAdapter("SELECT * FROM " + table, connection).Fill(data);
                }
                data.WriteXml(dialog.FileName, XmlWriteMode.WriteSchema);

                lastExport = dialog.FileName;
                ExportResultText.Text = data.Rows.Count + (data.Rows.Count == 1 ? " record saved" : " records saved");
                ExportResult.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowFolder_Click(object sender, RoutedEventArgs e)
        {
            if (lastExport != null && File.Exists(lastExport))
                Process.Start("explorer.exe", "/select,\"" + lastExport + "\"");
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "XML files (*.xml)|*.xml", Title = "Choose a saved copy" };
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                fileNameTextBlock.Text = Path.GetFileName(filePath);
                importButton.IsEnabled = true;
            }
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Choose a file to restore first.", "No file chosen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dataSet = new DataSet();
            try { dataSet.ReadXml(filePath); }
            catch (Exception ex)
            {
                MessageBox.Show("That file isn't a readable EduTrack copy.\n" + ex.Message, "Can't read the file", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("That file has no records to restore.", "Nothing to restore", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DataTable source = dataSet.Tables[0];
            string table = ResolveTable(source.TableName, Path.GetFileNameWithoutExtension(filePath));
            if (table == null)
            {
                MessageBox.Show("EduTrack can only restore students, teachers or employees saved with the Save a copy button.",
                    "Unknown file", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show("Restore " + source.Rows.Count + " records into " + table.Replace("_registry", "") + "s? Records with the same ID will be replaced.",
                "Restore data", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    // Only columns that really exist in the table are written; the table name never comes from the file.
                    var known = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    using (var schema = new MySqlCommand("SELECT * FROM " + table + " LIMIT 0", connection))
                    using (var reader = schema.ExecuteReader())
                        for (int i = 0; i < reader.FieldCount; i++) known.Add(reader.GetName(i));

                    var columns = source.Columns.Cast<DataColumn>().Where(c => known.Contains(c.ColumnName)).Select(c => c.ColumnName).ToList();
                    if (columns.Count == 0)
                    {
                        MessageBox.Show("None of the columns in that file match the " + table + " table.", "Can't restore", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string sql = "INSERT INTO " + table + " (" + string.Join(", ", columns.Select(c => "`" + c + "`")) + ") VALUES (" +
                                 string.Join(", ", columns.Select((c, i) => "@p" + i)) + ") ON DUPLICATE KEY UPDATE " +
                                 string.Join(", ", columns.Select(c => "`" + c + "` = VALUES(`" + c + "`)"));

                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (DataRow row in source.Rows)
                        {
                            using (var command = new MySqlCommand(sql, connection, transaction))
                            {
                                for (int i = 0; i < columns.Count; i++)
                                {
                                    object value = row[columns[i]];
                                    command.Parameters.AddWithValue("@p" + i, value == null || value is DBNull ? DBNull.Value : value);
                                }
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                }
                MessageBox.Show(source.Rows.Count + " records were restored successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nothing was changed. The restore stopped because:\n" + ex.Message, "Couldn't restore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string ResolveTable(string xmlTableName, string fileName)
        {
            foreach (var candidate in new[] { xmlTableName, fileName })
            {
                if (candidate == null) continue;
                foreach (var table in Tables.Values)
                    if (candidate.StartsWith(table, StringComparison.OrdinalIgnoreCase)) return table;
                if (LegacyNames.TryGetValue(candidate, out string legacy)) return legacy;
            }
            return null;
        }
    }
}
