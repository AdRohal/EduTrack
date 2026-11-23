using System;
using System.Security.Cryptography;
using EduTrack.Data;
using MySql.Data.MySqlClient;

namespace EduTrack.Utilities
{
    public enum AuthResult { Ok, WrongCredentials, DatabaseError }

    /// <summary>
    /// Handles sign-in and account creation. Passwords are stored as salted PBKDF2 hashes.
    /// Accounts created by older versions (plain text) still work and are upgraded the first time they sign in.
    /// </summary>
    public static class AuthService
    {
        private const string Prefix = "pbkdf2";
        private const int Iterations = 20000;   // stored inside each hash, so it can be raised later without breaking old accounts
        public const int MinPasswordLength = 6;

        public static string LastError { get; private set; }

        public static bool HasAnyUser()
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateOpenConnection())
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM login", connection))
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
            catch { return true; }
        }

        public static AuthResult SignIn(string username, string password)
        {
            LastError = null;
            try
            {
                using (var connection = DatabaseConnectionManager.CreateOpenConnection())
                {
                    string stored;
                    using (var command = new MySqlCommand("SELECT Password FROM login WHERE UserName = @u LIMIT 1", connection))
                    {
                        command.Parameters.AddWithValue("@u", username);
                        stored = command.ExecuteScalar() as string;
                    }
                    if (stored == null) return AuthResult.WrongCredentials;

                    if (stored.StartsWith(Prefix + "$", StringComparison.Ordinal))
                        return Verify(password, stored) ? AuthResult.Ok : AuthResult.WrongCredentials;

                    // Legacy plain-text account: compare, then upgrade to a hash.
                    if (stored != password) return AuthResult.WrongCredentials;
                    try
                    {
                        using (var upgrade = new MySqlCommand("UPDATE login SET Password = @p WHERE UserName = @u", connection))
                        {
                            upgrade.Parameters.AddWithValue("@p", Hash(password));
                            upgrade.Parameters.AddWithValue("@u", username);
                            upgrade.ExecuteNonQuery();
                        }
                    }
                    catch { /* column too narrow: keep working with the legacy value */ }
                    return AuthResult.Ok;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return AuthResult.DatabaseError;
            }
        }

        /// <summary>Creates an account. Returns null on success, otherwise a message that tells the user what to fix.</summary>
        public static string Register(string username, string password)
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateOpenConnection())
                {
                    using (var exists = new MySqlCommand("SELECT COUNT(*) FROM login WHERE UserName = @u", connection))
                    {
                        exists.Parameters.AddWithValue("@u", username);
                        if (Convert.ToInt32(exists.ExecuteScalar()) > 0)
                            return "That username is taken. Choose another one.";
                    }
                    using (var insert = new MySqlCommand("INSERT INTO login (UserName, Password) VALUES (@u, @p)", connection))
                    {
                        insert.Parameters.AddWithValue("@u", username);
                        insert.Parameters.AddWithValue("@p", Hash(password));
                        insert.ExecuteNonQuery();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return "Could not create the account: " + ex.Message;
            }
        }

        private static string Hash(string password)
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (var kdf = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                return string.Join("$", Prefix, Iterations, Convert.ToBase64String(salt), Convert.ToBase64String(kdf.GetBytes(32)));
        }

        private static bool Verify(string password, string stored)
        {
            var parts = stored.Split('$');
            if (parts.Length != 4 || !int.TryParse(parts[1], out int iterations)) return false;
            var salt = Convert.FromBase64String(parts[2]);
            var expected = Convert.FromBase64String(parts[3]);
            using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                var actual = kdf.GetBytes(expected.Length);
                int diff = 0;
                for (int i = 0; i < expected.Length; i++) diff |= expected[i] ^ actual[i];
                return diff == 0;
            }
        }
    }
}
