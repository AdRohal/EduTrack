using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace EduTrack.Data
{
    /// <summary>
    /// Centralized database connection manager for the EduTrack application.
    /// Provides a single point of configuration for all database connections.
    /// </summary>
    public static class DatabaseConnectionManager
    {
        #region Database Configuration Constants
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "database";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";
        #endregion

        #region Connection String Property
        /// <summary>
        /// Gets the complete database connection string.
        /// </summary>
        public static string ConnectionString =>
            $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
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
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
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
        #endregion
    }
}