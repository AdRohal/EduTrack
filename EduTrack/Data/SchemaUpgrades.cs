using System;
using MySql.Data.MySqlClient;

namespace EduTrack.Data
{
    /// <summary>
    /// Small, safe changes to databases created from older copies of datebase.sql. Every step can be repeated
    /// and a failed step (for example missing permission) is ignored, because the app also works without it.
    /// </summary>
    public static class SchemaUpgrades
    {
        public static void Apply()
        {
            Run("ALTER TABLE login MODIFY Password VARCHAR(255) NULL");                // room for a password hash

            // One grade per student per module: remove older duplicates first, then enforce it so saving grades updates instead of duplicating.
            Run("DELETE a FROM student_module a JOIN student_module b ON a.StudentID = b.StudentID AND a.ModuleID = b.ModuleID AND a.StudentModuleID < b.StudentModuleID");
            Run("ALTER TABLE student_module ADD UNIQUE KEY uq_student_module (StudentID, ModuleID)");

            // One row per class and hour in the timetable.
            Run("DELETE a FROM classschedules a JOIN classschedules b ON a.ClassID = b.ClassID AND a.TimeSlot = b.TimeSlot AND a.ScheduleID < b.ScheduleID");
        }

        private static void Run(string sql)
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateOpenConnection())
                using (var command = new MySqlCommand(sql, connection))
                    command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // Already applied, or not allowed on this server: nothing to do.
            }
        }
    }
}
