using System;
using System.Configuration;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EduTrack.Data
{
    /// <summary>
    /// Centralized database connection manager for the EduTrack application.
    /// Connection details come from the appSettings in EduTrack.exe.config and default to a local XAMPP / MySQL / MariaDB server.
    /// </summary>
    public static class DatabaseConnectionManager
    {
        #region Database Configuration
        private static string Setting(string key, string fallback)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        }

        public static string Server => Setting("Db.Server", "127.0.0.1");
        public static string Port => Setting("Db.Port", "3306");
        public static string DatabaseName => Setting("Db.Name", "database");
        private static string DatabaseUser => Setting("Db.User", "root");
        private static string DatabasePassword => ConfigurationManager.AppSettings["Db.Password"] ?? string.Empty;
        #endregion

        #region Connection String Property
        /// <summary>
        /// Gets the complete database connection string.
        /// </summary>
        public static string ConnectionString => EmbeddedDatabase.IsBundled
            ? EmbeddedDatabase.ConnectionBase + "Database=" + DatabaseName + ";"
            : $"Server={Server};Port={Port};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};CharSet=utf8mb4;";
        #endregion

        #region Connection Factory Methods
        /// <summary>
        /// Creates a new MySqlConnection instance with the configured connection string.
        /// </summary>
        /// <returns>A new MySqlConnection instance</returns>
        public static MySqlConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// Creates and opens a new MySqlConnection instance.
        /// </summary>
        /// <returns>An opened MySqlConnection instance</returns>
        public static MySqlConnection CreateOpenConnection()
        {
            var connection = CreateConnection();
            connection.Open();
            return connection;
        }
        #endregion

        #region Connection Testing
        /// <summary>
        /// Tests the database connection to ensure it's working properly.
        /// </summary>
        /// <returns>True if the connection is successful, false otherwise</returns>
        public static bool TestConnection()
        {
            return TestConnection(out _);
        }

        /// <summary>
        /// Tests the database connection and returns any error message.
        /// </summary>
        /// <param name="errorMessage">The error message if connection fails</param>
        /// <returns>True if the connection is successful, false otherwise</returns>
        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    errorMessage = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Runs one tiny asynchronous query so the first real page doesn't pay the start-up cost of the async code path.
        /// </summary>
        public static async Task WarmUpAsync()
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT 1", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                    }
                }
            }
            catch (Exception)
            {
                // The splash screen reports connection problems; warming up is only an optimisation.
            }
        }
        #endregion
    }
}
