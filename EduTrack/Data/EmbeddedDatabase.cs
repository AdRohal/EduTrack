using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;

namespace EduTrack.Data
{
    /// <summary>
    /// The private MariaDB that ships inside the installed app (folder "db" next to EduTrack.exe).
    /// It runs only while EduTrack runs, is not reachable over the network (named pipe only), and its root password is
    /// random per machine and stored encrypted for the current Windows user. When the folder is missing (developer machine,
    /// XAMPP, a shared server) none of this is used and the settings in EduTrack.exe.config apply instead.
    /// </summary>
    public static class EmbeddedDatabase
    {
        public const string PipeName = "EduTrackDb";

        private static readonly string Root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db");
        private static readonly string DataRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduTrack");
        private static readonly string DataDir = Path.Combine(DataRoot, "data");
        private static readonly string KeyFile = Path.Combine(DataRoot, "db.key");
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("EduTrack.db.v1");

        private static Process _server;
        private static IntPtr _job = IntPtr.Zero;
        private static string _password;
        private static bool _started;

        /// <summary>True when the installed copy carries its own database server.</summary>
        public static bool IsBundled => File.Exists(Path.Combine(Root, "bin", "mariadbd.exe"));

        /// <summary>Connection string (without database name) for the private server.</summary>
        public static string ConnectionBase =>
            "Server=.;Protocol=pipe;PipeName=" + PipeName + ";User ID=root;Password=" + (_password ?? string.Empty) + ";CharSet=utf8mb4;";

        // ---------------------------------------------------------------- start

        public static bool EnsureStarted(string databaseName, out string error)
        {
            error = null;
            if (!IsBundled) return true;
            if (_started) return true;

            try
            {
                Directory.CreateDirectory(DataRoot);
                bool fresh = !Directory.Exists(Path.Combine(DataDir, "mysql"));

                if (fresh)
                {
                    _password = NewPassword();
                    SavePassword(_password);
                    InitialiseDataDirectory();
                }
                else
                {
                    _password = LoadPassword();
                    if (_password == null)
                    {
                        error = "The database exists but its key could not be read for this Windows user (" + KeyFile + ").";
                        return false;
                    }
                }

                if (!CanConnect()) StartServer();
                if (!WaitUntilReady(TimeSpan.FromSeconds(90)))
                {
                    error = "The database did not start. See " + Path.Combine(DataRoot, "data") + " for its log.";
                    Stop();
                    return false;
                }

                using (var connection = new MySqlConnection(ConnectionBase))
                {
                    connection.Open();
                    if (fresh) Harden(connection);
                    if (!DatabaseExists(connection, databaseName)) CreateDatabase(connection, databaseName);
                }

                _started = true;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                try { Stop(); } catch { }
                return false;
            }
        }

        private static void InitialiseDataDirectory()
        {
            var start = new ProcessStartInfo(Path.Combine(Root, "bin", "mysql_install_db.exe"),
                "--datadir=\"" + DataDir + "\" --password=" + _password + " --skip-networking --socket=" + PipeName + " --silent")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            using (var process = Process.Start(start))
            {
                process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                if (!process.WaitForExit(180000) || process.ExitCode != 0)
                    throw new InvalidOperationException("The database could not be created. " + errors);
            }
        }

        private static void StartServer()
        {
            // Small footprint, no network, no extras. The root of the install is the base directory.
            string args = "--no-defaults --basedir=\"" + Root.TrimEnd('\\') + "\" --datadir=\"" + DataDir + "\"" +
                          " --skip-networking --enable-named-pipe --socket=" + PipeName +
                          " --innodb-buffer-pool-size=64M --innodb-log-file-size=16M --max-connections=30 --performance-schema=OFF" +
                          " --local-infile=0 --skip-name-resolve --character-set-server=utf8mb4 --collation-server=utf8mb4_general_ci";
            var start = new ProcessStartInfo(Path.Combine(Root, "bin", "mariadbd.exe"), args)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            _server = Process.Start(start);

            // If EduTrack is killed, Windows closes the job and takes the database down with it.
            try
            {
                _job = CreateJobObject(IntPtr.Zero, null);
                var info = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION();
                info.BasicLimitInformation.LimitFlags = 0x2000; // JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
                int size = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
                IntPtr ptr = Marshal.AllocHGlobal(size);
                try
                {
                    Marshal.StructureToPtr(info, ptr, false);
                    SetInformationJobObject(_job, 9, ptr, (uint)size);
                }
                finally { Marshal.FreeHGlobal(ptr); }
                AssignProcessToJobObject(_job, _server.Handle);
            }
            catch { /* the normal shutdown path still stops it */ }
        }

        private static bool WaitUntilReady(TimeSpan timeout)
        {
            var until = DateTime.UtcNow + timeout;
            while (DateTime.UtcNow < until)
            {
                if (_server != null && _server.HasExited) return false;
                if (CanConnect()) return true;
                Thread.Sleep(250);
            }
            return false;
        }

        private static bool CanConnect()
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionBase + "Connection Timeout=3;"))
                {
                    connection.Open();
                    return true;
                }
            }
            catch { return false; }
        }

        // ---------------------------------------------------------------- first-run set-up

        /// <summary>Only the app's own password may open root; no other root host, no anonymous users, no sample database.</summary>
        private static void Harden(MySqlConnection connection)
        {
            string sql =
                "ALTER USER 'root'@'localhost' IDENTIFIED VIA mysql_native_password USING PASSWORD('" + _password + "');" +
                "DELETE FROM mysql.global_priv WHERE NOT (User = 'root' AND Host = 'localhost') AND User NOT IN ('mariadb.sys', 'mysql', 'PUBLIC');" +
                "DROP DATABASE IF EXISTS test;" +
                "FLUSH PRIVILEGES;";
            using (var command = new MySqlCommand(sql, connection)) command.ExecuteNonQuery();
        }

        private static bool DatabaseExists(MySqlConnection connection, string name)
        {
            using (var command = new MySqlCommand("SELECT COUNT(*) FROM information_schema.SCHEMATA WHERE SCHEMA_NAME = @n", connection))
            {
                command.Parameters.AddWithValue("@n", name);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private static void CreateDatabase(MySqlConnection connection, string name)
        {
            using (var command = new MySqlCommand("CREATE DATABASE `" + name.Replace("`", "``") + "` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci", connection))
                command.ExecuteNonQuery();

            string schema = Path.Combine(Root, "schema.sql");
            if (!File.Exists(schema)) return;
            using (var inDatabase = new MySqlConnection(ConnectionBase + "Database=" + name + ";"))
            {
                inDatabase.Open();
                new MySqlScript(inDatabase, File.ReadAllText(schema, Encoding.UTF8)).Execute();
            }
        }

        // ---------------------------------------------------------------- stop

        /// <summary>Shuts the database down cleanly (and forcibly if it does not obey), so nothing keeps running once EduTrack is closed.</summary>
        public static void Stop()
        {
            if (!IsBundled) return;
            _started = false;

            try
            {
                if (_password != null && CanConnect())
                {
                    using (var connection = new MySqlConnection(ConnectionBase + "Connection Timeout=3;"))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand("SHUTDOWN", connection)) command.ExecuteNonQuery();
                    }
                }
            }
            catch { /* the connection drops while it shuts down */ }

            try
            {
                if (_server != null && !_server.WaitForExit(15000)) _server.Kill();
            }
            catch { }

            if (_job != IntPtr.Zero) { CloseHandle(_job); _job = IntPtr.Zero; }
            _server = null;
        }

        // ---------------------------------------------------------------- password

        private static string NewPassword()
        {
            var bytes = new byte[24];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes).Replace('+', 'x').Replace('/', 'y').Replace('=', 'z');
        }

        private static void SavePassword(string password)
        {
            byte[] sealedBytes = ProtectedData.Protect(Encoding.UTF8.GetBytes(password), Entropy, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(KeyFile, sealedBytes);
        }

        private static string LoadPassword()
        {
            try
            {
                if (!File.Exists(KeyFile)) return null;
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(File.ReadAllBytes(KeyFile), Entropy, DataProtectionScope.CurrentUser));
            }
            catch { return null; }
        }

        // ---------------------------------------------------------------- Windows job object

        [StructLayout(LayoutKind.Sequential)]
        private struct IO_COUNTERS { public ulong a, b, c, d, e, f; }

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public long PerProcessUserTimeLimit, PerJobUserTimeLimit;
            public uint LimitFlags;
            public UIntPtr MinimumWorkingSetSize, MaximumWorkingSetSize;
            public uint ActiveProcessLimit;
            public UIntPtr Affinity;
            public uint PriorityClass, SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public UIntPtr ProcessMemoryLimit, JobMemoryLimit, PeakProcessMemoryUsed, PeakJobMemoryUsed;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateJobObject(IntPtr attributes, string name);

        [DllImport("kernel32.dll")]
        private static extern bool SetInformationJobObject(IntPtr job, int infoClass, IntPtr info, uint size);

        [DllImport("kernel32.dll")]
        private static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);
    }
}
