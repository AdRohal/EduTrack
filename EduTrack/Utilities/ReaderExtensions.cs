using System;
using MySql.Data.MySqlClient;

namespace EduTrack.Utilities
{
    /// <summary>Null-safe column readers, so list pages don't repeat IsDBNull checks on every column.</summary>
    public static class ReaderExtensions
    {
        public static string Str(this MySqlDataReader reader, string column)
        {
            int i = reader.GetOrdinal(column);
            return reader.IsDBNull(i) ? null : Convert.ToString(reader.GetValue(i));
        }

        public static DateTime? Date(this MySqlDataReader reader, string column)
        {
            int i = reader.GetOrdinal(column);
            return reader.IsDBNull(i) ? (DateTime?)null : reader.GetDateTime(i);
        }

        public static int? Int(this MySqlDataReader reader, string column)
        {
            int i = reader.GetOrdinal(column);
            return reader.IsDBNull(i) ? (int?)null : Convert.ToInt32(reader.GetValue(i));
        }
    }
}
