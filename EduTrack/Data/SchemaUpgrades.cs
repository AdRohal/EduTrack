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

            ClassWorkspaceTables();
        }

        /// <summary>
        /// Tables behind the class workspace: who teaches which module, the weekly lessons, attendance per lesson and
        /// assessments with scores. The same statements are in installer\schema.sql for a fresh install.
        /// </summary>
        private static void ClassWorkspaceTables()
        {
            Run("ALTER TABLE modules ADD COLUMN TeacherID INT NULL");
            Run("ALTER TABLE modules ADD COLUMN Coefficient DECIMAL(4,2) NOT NULL DEFAULT 1");

            Run(@"CREATE TABLE IF NOT EXISTS class_lessons (
                    LessonID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    ClassID INT NOT NULL,
                    DayOfWeek TINYINT NOT NULL,
                    StartTime TIME NOT NULL,
                    EndTime TIME NOT NULL,
                    ModuleID INT NULL,
                    Title VARCHAR(100) NULL,
                    Room VARCHAR(50) NULL,
                    KEY ix_lessons_class (ClassID, DayOfWeek)
                  ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci");

            Run(@"CREATE TABLE IF NOT EXISTS lesson_sessions (
                    SessionID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    ClassID INT NOT NULL,
                    LessonID INT NULL,
                    SessionDate DATE NOT NULL,
                    StartTime TIME NOT NULL,
                    EndTime TIME NOT NULL,
                    ModuleID INT NULL,
                    Title VARCHAR(100) NULL,
                    TeacherID INT NULL,
                    UNIQUE KEY uq_session (ClassID, SessionDate, StartTime),
                    KEY ix_session_date (ClassID, SessionDate)
                  ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci");

            Run(@"CREATE TABLE IF NOT EXISTS attendance (
                    AttendanceID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    SessionID INT NOT NULL,
                    PersonType CHAR(1) NOT NULL,
                    PersonID INT NOT NULL,
                    Status CHAR(1) NOT NULL,
                    Note VARCHAR(200) NULL,
                    UNIQUE KEY uq_attendance (SessionID, PersonType, PersonID),
                    KEY ix_attendance_person (PersonType, PersonID)
                  ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci");

            Run(@"CREATE TABLE IF NOT EXISTS assessments (
                    AssessmentID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    ModuleID INT NOT NULL,
                    Title VARCHAR(100) NOT NULL,
                    Kind VARCHAR(20) NOT NULL DEFAULT 'Exam',
                    Weight DECIMAL(5,2) NOT NULL DEFAULT 1,
                    MaxScore DECIMAL(5,2) NOT NULL DEFAULT 20,
                    AssessDate DATE NULL,
                    KEY ix_assess_module (ModuleID)
                  ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci");

            Run(@"CREATE TABLE IF NOT EXISTS assessment_scores (
                    ScoreID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    AssessmentID INT NOT NULL,
                    StudentID INT NOT NULL,
                    Score DECIMAL(6,2) NULL,
                    Flag VARCHAR(5) NULL,
                    UNIQUE KEY uq_score (AssessmentID, StudentID)
                  ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci");

            Run("CREATE TABLE IF NOT EXISTS app_flags (FlagName VARCHAR(60) NOT NULL PRIMARY KEY) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci");

            // The old free-text timetable becomes lessons (once). The old table is left untouched.
            RunOnce("lessons_from_classschedules", LessonsFromOldTimetable());

            // Old single grades become one "Final grade" assessment per module (once).
            RunOnce("assessments_from_student_module",
                "INSERT INTO assessments (ModuleID, Title, Kind, Weight, MaxScore) " +
                "SELECT DISTINCT sm.ModuleID, 'Final grade', 'Exam', 1, 20 FROM student_module sm " +
                "WHERE sm.Grade REGEXP '^[0-9]+([.,][0-9]+)?$'",
                "INSERT INTO assessment_scores (AssessmentID, StudentID, Score) " +
                "SELECT a.AssessmentID, sm.StudentID, CAST(REPLACE(sm.Grade, ',', '.') AS DECIMAL(6,2)) FROM student_module sm " +
                "JOIN assessments a ON a.ModuleID = sm.ModuleID AND a.Title = 'Final grade' WHERE sm.Grade REGEXP '^[0-9]+([.,][0-9]+)?$'",
                "INSERT INTO assessment_scores (AssessmentID, StudentID, Flag) " +
                "SELECT a.AssessmentID, sm.StudentID, UPPER(LEFT(sm.Grade, 5)) FROM student_module sm " +
                "JOIN assessments a ON a.ModuleID = sm.ModuleID AND a.Title = 'Final grade' " +
                "WHERE sm.Grade IS NOT NULL AND sm.Grade <> '' AND sm.Grade NOT REGEXP '^[0-9]+([.,][0-9]+)?$'");
        }

        private static string[] LessonsFromOldTimetable()
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            var statements = new string[days.Length];
            for (int i = 0; i < days.Length; i++)
            {
                statements[i] =
                    "INSERT INTO class_lessons (ClassID, DayOfWeek, StartTime, EndTime, Title) " +
                    "SELECT cs.ClassID, " + (i + 1) + ", STR_TO_DATE(SUBSTRING_INDEX(cs.TimeSlot, '-', 1), '%H:%i'), " +
                    "STR_TO_DATE(SUBSTRING_INDEX(cs.TimeSlot, '-', -1), '%H:%i'), LEFT(TRIM(cs." + days[i] + "), 100) " +
                    "FROM classschedules cs JOIN classes c ON c.ClassID = cs.ClassID " +
                    "WHERE cs." + days[i] + " IS NOT NULL AND TRIM(cs." + days[i] + ") <> '' AND cs.TimeSlot LIKE '%-%'";
            }
            return statements;
        }

        /// <summary>Runs the statements once, in order, and remembers it in app_flags so a restart never repeats them.</summary>
        private static void RunOnce(string flag, params string[] statements)
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateOpenConnection())
                {
                    using (var check = new MySqlCommand("SELECT COUNT(*) FROM app_flags WHERE FlagName = @f", connection))
                    {
                        check.Parameters.AddWithValue("@f", flag);
                        if (Convert.ToInt32(check.ExecuteScalar()) > 0) return;
                    }
                    foreach (var sql in statements)
                        using (var command = new MySqlCommand(sql, connection)) command.ExecuteNonQuery();
                    using (var mark = new MySqlCommand("INSERT INTO app_flags (FlagName) VALUES (@f)", connection))
                    {
                        mark.Parameters.AddWithValue("@f", flag);
                        mark.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                // The tables may not exist yet on a very old server; the workspace then starts empty.
            }
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
