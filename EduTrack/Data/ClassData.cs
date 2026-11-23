using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EduTrack.Models;
using EduTrack.Utilities;
using MySql.Data.MySqlClient;

namespace EduTrack.Data
{
    /// <summary>
    /// Every query behind the class workspace (classes, people, modules, timetable, attendance, assessments), kept in one place
    /// so the screens only deal with objects. Methods throw on database errors; the screens show them.
    /// </summary>
    public static class ClassData
    {
        private static async Task<MySqlConnection> OpenAsync()
        {
            var connection = DatabaseConnectionManager.CreateConnection();
            await connection.OpenAsync();
            return connection;
        }

        private static async Task<int> NonQueryAsync(MySqlConnection c, MySqlTransaction t, string sql, params (string, object)[] args)
        {
            using (var command = new MySqlCommand(sql, c, t))
            {
                foreach (var (name, value) in args) command.Parameters.AddWithValue(name, value ?? DBNull.Value);
                return await command.ExecuteNonQueryAsync();
            }
        }

        private static async Task<long> InsertAsync(MySqlConnection c, MySqlTransaction t, string sql, params (string, object)[] args)
        {
            using (var command = new MySqlCommand(sql, c, t))
            {
                foreach (var (name, value) in args) command.Parameters.AddWithValue(name, value ?? DBNull.Value);
                await command.ExecuteNonQueryAsync();
                return command.LastInsertedId;
            }
        }

        public static int WeekdayOf(DateTime date) => ((int)date.DayOfWeek + 6) % 7 + 1;   // Monday = 1 ... Sunday = 7

        // ================================================================ classes

        public static async Task<List<ClassSummary>> GetClassesAsync()
        {
            var list = new List<ClassSummary>();
            const string sql = @"
SELECT c.ClassID, c.ClassName, c.major, c.Description, c.CreatedDate,
       (SELECT COUNT(*) FROM studentclasses sc WHERE sc.ClassID = c.ClassID) AS Students,
       (SELECT COUNT(*) FROM teacherclasses tc WHERE tc.ClassID = c.ClassID) AS Teachers,
       (SELECT COUNT(*) FROM modules m WHERE m.ClassID = c.ClassID) AS Modules
FROM classes c ORDER BY c.ClassName";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
            {
                while (await r.ReadAsync())
                    list.Add(new ClassSummary
                    {
                        ClassID = r.GetInt32(0), ClassName = r.Str("ClassName"), Major = r.Str("major"), Description = r.Str("Description"),
                        Created = r.Date("CreatedDate"), StudentCount = Convert.ToInt32(r["Students"]), TeacherCount = Convert.ToInt32(r["Teachers"]),
                        ModuleCount = Convert.ToInt32(r["Modules"])
                    });
            }
            return list;
        }

        /// <summary>Creates (ClassID == 0) or updates a class and returns its id.</summary>
        public static async Task<int> SaveClassAsync(ClassSummary k)
        {
            using (var c = await OpenAsync())
            {
                if (k.ClassID == 0)
                    return (int)await InsertAsync(c, null, "INSERT INTO classes (ClassName, major, Description, CreatedDate) VALUES (@n, @m, @d, @c)",
                        ("@n", k.ClassName), ("@m", k.Major), ("@d", k.Description), ("@c", k.Created));
                await NonQueryAsync(c, null, "UPDATE classes SET ClassName = @n, major = @m, Description = @d, CreatedDate = @c WHERE ClassID = @id",
                    ("@n", k.ClassName), ("@m", k.Major), ("@d", k.Description), ("@c", k.Created), ("@id", k.ClassID));
                return k.ClassID;
            }
        }

        public static async Task DeleteClassAsync(int classId)
        {
            using (var c = await OpenAsync())
            using (var t = c.BeginTransaction())
            {
                var id = ("@c", (object)classId);
                string[] statements =
                {
                    "DELETE FROM attendance WHERE SessionID IN (SELECT SessionID FROM lesson_sessions WHERE ClassID = @c)",
                    "DELETE FROM lesson_sessions WHERE ClassID = @c",
                    "DELETE FROM class_lessons WHERE ClassID = @c",
                    "DELETE FROM assessment_scores WHERE AssessmentID IN (SELECT AssessmentID FROM assessments WHERE ModuleID IN (SELECT ModuleID FROM modules WHERE ClassID = @c))",
                    "DELETE FROM assessments WHERE ModuleID IN (SELECT ModuleID FROM modules WHERE ClassID = @c)",
                    "DELETE FROM student_module WHERE ModuleID IN (SELECT ModuleID FROM modules WHERE ClassID = @c)",
                    "DELETE FROM modules WHERE ClassID = @c",
                    "DELETE FROM classschedules WHERE ClassID = @c",
                    "DELETE FROM studentclasses WHERE ClassID = @c",
                    "DELETE FROM teacherclasses WHERE ClassID = @c",
                    "DELETE FROM classes WHERE ClassID = @c"
                };
                foreach (var sql in statements) await NonQueryAsync(c, t, sql, id);
                t.Commit();
            }
        }

        // ================================================================ people

        public static async Task<List<Student>> GetStudentsAsync(int classId, string search = null)
        {
            var list = new List<Student>();
            string sql = "SELECT s.StudentID, s.p_pic, s.f_name, s.m_name, s.l_name, s.email, s.phone FROM student_registry s " +
                         "JOIN studentclasses sc ON s.StudentID = sc.StudentID WHERE sc.ClassID = @c";
            if (!string.IsNullOrWhiteSpace(search)) sql += " AND (s.f_name LIKE @q OR s.m_name LIKE @q OR s.l_name LIKE @q OR CONCAT(s.f_name, ' ', s.l_name) LIKE @q)";
            sql += " ORDER BY s.l_name, s.f_name";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                if (!string.IsNullOrWhiteSpace(search)) command.Parameters.AddWithValue("@q", "%" + search.Trim() + "%");
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                    while (await r.ReadAsync())
                        list.Add(new Student { Id = r.GetInt32(0), P_Pic = r.Str("p_pic"), F_Name = r.Str("f_name"), M_Name = r.Str("m_name"), L_Name = r.Str("l_name"), Email = r.Str("email"), Phone = r.Str("phone") });
            }
            return list;
        }

        public static async Task<List<Teacher>> GetTeachersAsync(int classId)
        {
            var list = new List<Teacher>();
            const string sql = "SELECT t.TeacherID, t.p_pic, t.f_name, t.m_name, t.l_name, t.email, t.phone FROM teacher_registry t " +
                               "JOIN teacherclasses tc ON t.TeacherID = tc.TeacherID WHERE tc.ClassID = @c ORDER BY t.l_name, t.f_name";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                    while (await r.ReadAsync())
                        list.Add(new Teacher { Id = r.GetInt32(0), P_Pic = r.Str("p_pic"), FirstName = r.Str("f_name"), MiddleName = r.Str("m_name"), LastName = r.Str("l_name"), Email = r.Str("email"), Phone = r.Str("phone") });
            }
            return list;
        }

        public static async Task<List<PersonOption>> GetCandidatesAsync(int classId, bool teacher)
        {
            var people = new List<PersonOption>();
            string sql = teacher
                ? "SELECT t.TeacherID AS Id, t.f_name, t.m_name, t.l_name, NULL AS ClassID, NULL AS ClassName FROM teacher_registry t " +
                  "WHERE t.TeacherID NOT IN (SELECT TeacherID FROM teacherclasses WHERE ClassID = @c) ORDER BY t.l_name, t.f_name"
                : "SELECT s.StudentID AS Id, s.f_name, s.m_name, s.l_name, c.ClassID, c.ClassName FROM student_registry s " +
                  "LEFT JOIN studentclasses sc ON s.StudentID = sc.StudentID LEFT JOIN classes c ON c.ClassID = sc.ClassID " +
                  "WHERE sc.ClassID IS NULL OR sc.ClassID <> @c ORDER BY s.l_name, s.f_name";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                    while (await r.ReadAsync())
                    {
                        string className = r.Str("ClassName");
                        people.Add(new PersonOption
                        {
                            Id = Convert.ToInt32(r["Id"]), Name = NameHelper.Join(r.Str("f_name"), r.Str("m_name"), r.Str("l_name")),
                            Note = className == null ? "" : "currently in " + className, CurrentClassId = r.Int("ClassID"), CurrentClassName = className
                        });
                    }
            }
            return people;
        }

        public static async Task AddPersonAsync(int classId, int personId, bool teacher)
        {
            using (var c = await OpenAsync())
                await NonQueryAsync(c, null, teacher
                    ? "INSERT INTO teacherclasses (TeacherID, ClassID) VALUES (@p, @c)"
                    : "INSERT INTO studentclasses (StudentID, ClassID) VALUES (@p, @c) ON DUPLICATE KEY UPDATE ClassID = VALUES(ClassID)",
                    ("@p", personId), ("@c", classId));
        }

        public static async Task RemovePersonAsync(int classId, int personId, bool teacher)
        {
            using (var c = await OpenAsync())
            {
                await NonQueryAsync(c, null, teacher
                    ? "DELETE FROM teacherclasses WHERE TeacherID = @p AND ClassID = @c"
                    : "DELETE FROM studentclasses WHERE StudentID = @p AND ClassID = @c", ("@p", personId), ("@c", classId));
                // A teacher who leaves the class no longer teaches its modules.
                if (teacher) await NonQueryAsync(c, null, "UPDATE modules SET TeacherID = NULL WHERE ClassID = @c AND TeacherID = @p", ("@p", personId), ("@c", classId));
            }
        }

        // ================================================================ modules

        public static async Task<List<ModuleInfo>> GetModulesAsync(int classId)
        {
            var list = new List<ModuleInfo>();
            const string sql = @"SELECT m.ModuleID, m.ClassID, m.ModuleName, m.TeacherID, m.Coefficient, CONCAT_WS(' ', t.f_name, t.l_name) AS TeacherName
FROM modules m LEFT JOIN teacher_registry t ON t.TeacherID = m.TeacherID WHERE m.ClassID = @c ORDER BY m.ModuleName";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                    while (await r.ReadAsync())
                        list.Add(new ModuleInfo
                        {
                            ModuleID = r.GetInt32(0), ClassID = r.GetInt32(1), ModuleName = r.Str("ModuleName"), TeacherID = r.Int("TeacherID"),
                            Coefficient = Convert.ToDecimal(r["Coefficient"]), TeacherName = r.Str("TeacherName")
                        });
            }
            return list;
        }

        public static async Task<int> SaveModuleAsync(ModuleInfo m)
        {
            using (var c = await OpenAsync())
            {
                if (m.ModuleID == 0)
                    return (int)await InsertAsync(c, null, "INSERT INTO modules (ModuleName, ClassID, TeacherID, Coefficient) VALUES (@n, @c, @t, @k)",
                        ("@n", m.ModuleName), ("@c", m.ClassID), ("@t", m.TeacherID), ("@k", m.Coefficient));
                await NonQueryAsync(c, null, "UPDATE modules SET ModuleName = @n, TeacherID = @t, Coefficient = @k WHERE ModuleID = @id",
                    ("@n", m.ModuleName), ("@t", m.TeacherID), ("@k", m.Coefficient), ("@id", m.ModuleID));
                return m.ModuleID;
            }
        }

        public static async Task DeleteModuleAsync(int moduleId)
        {
            using (var c = await OpenAsync())
            using (var t = c.BeginTransaction())
            {
                var id = ("@m", (object)moduleId);
                await NonQueryAsync(c, t, "DELETE FROM assessment_scores WHERE AssessmentID IN (SELECT AssessmentID FROM assessments WHERE ModuleID = @m)", id);
                await NonQueryAsync(c, t, "DELETE FROM assessments WHERE ModuleID = @m", id);
                await NonQueryAsync(c, t, "DELETE FROM student_module WHERE ModuleID = @m", id);
                await NonQueryAsync(c, t, "UPDATE class_lessons SET ModuleID = NULL WHERE ModuleID = @m", id);
                await NonQueryAsync(c, t, "DELETE FROM modules WHERE ModuleID = @m", id);
                t.Commit();
            }
        }

        // ================================================================ timetable

        public static async Task<List<LessonInfo>> GetLessonsAsync(int classId)
        {
            var list = new List<LessonInfo>();
            const string sql = @"SELECT l.LessonID, l.ClassID, l.DayOfWeek, l.StartTime, l.EndTime, l.ModuleID, l.Title, l.Room, m.ModuleName,
       CONCAT_WS(' ', t.f_name, t.l_name) AS TeacherName
FROM class_lessons l LEFT JOIN modules m ON m.ModuleID = l.ModuleID LEFT JOIN teacher_registry t ON t.TeacherID = m.TeacherID
WHERE l.ClassID = @c ORDER BY l.DayOfWeek, l.StartTime";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                    while (await r.ReadAsync())
                        list.Add(new LessonInfo
                        {
                            LessonID = r.GetInt32(0), ClassID = r.GetInt32(1), DayOfWeek = Convert.ToInt32(r["DayOfWeek"]),
                            Start = (TimeSpan)r["StartTime"], End = (TimeSpan)r["EndTime"], ModuleID = r.Int("ModuleID"), Title = r.Str("Title"),
                            Room = r.Str("Room"), ModuleName = r.Str("ModuleName"), TeacherName = r.Str("TeacherName")
                        });
            }
            return list;
        }

        public static async Task<int> SaveLessonAsync(LessonInfo l)
        {
            using (var c = await OpenAsync())
            {
                if (l.LessonID == 0)
                    return (int)await InsertAsync(c, null,
                        "INSERT INTO class_lessons (ClassID, DayOfWeek, StartTime, EndTime, ModuleID, Title, Room) VALUES (@c, @d, @s, @e, @m, @t, @r)",
                        ("@c", l.ClassID), ("@d", l.DayOfWeek), ("@s", l.Start), ("@e", l.End), ("@m", l.ModuleID), ("@t", l.Title), ("@r", l.Room));
                await NonQueryAsync(c, null, "UPDATE class_lessons SET DayOfWeek = @d, StartTime = @s, EndTime = @e, ModuleID = @m, Title = @t, Room = @r WHERE LessonID = @id",
                    ("@d", l.DayOfWeek), ("@s", l.Start), ("@e", l.End), ("@m", l.ModuleID), ("@t", l.Title), ("@r", l.Room), ("@id", l.LessonID));
                return l.LessonID;
            }
        }

        public static async Task DeleteLessonAsync(int lessonId)
        {
            using (var c = await OpenAsync()) await NonQueryAsync(c, null, "DELETE FROM class_lessons WHERE LessonID = @id", ("@id", lessonId));
        }

        // ================================================================ attendance

        /// <summary>The lessons of one date: the timetable of that weekday, plus any lesson already recorded that day (including extra ones).</summary>
        public static async Task<List<SessionInfo>> GetSessionsAsync(int classId, DateTime date)
        {
            date = date.Date;
            var result = new List<SessionInfo>();
            using (var c = await OpenAsync())
            {
                var lessons = new List<LessonInfo>();
                const string lessonSql = @"SELECT l.LessonID, l.StartTime, l.EndTime, l.ModuleID, l.Title, m.ModuleName, m.TeacherID, CONCAT_WS(' ', t.f_name, t.l_name) AS TeacherName
FROM class_lessons l LEFT JOIN modules m ON m.ModuleID = l.ModuleID LEFT JOIN teacher_registry t ON t.TeacherID = m.TeacherID
WHERE l.ClassID = @c AND l.DayOfWeek = @d ORDER BY l.StartTime";
                var planned = new List<SessionInfo>();
                using (var command = new MySqlCommand(lessonSql, c))
                {
                    command.Parameters.AddWithValue("@c", classId);
                    command.Parameters.AddWithValue("@d", WeekdayOf(date));
                    using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                            planned.Add(new SessionInfo
                            {
                                ClassID = classId, LessonID = r.GetInt32(0), Date = date, Start = (TimeSpan)r["StartTime"], End = (TimeSpan)r["EndTime"],
                                ModuleID = r.Int("ModuleID"), Title = r.Str("ModuleName") ?? r.Str("Title"), TeacherID = r.Int("TeacherID"), TeacherName = r.Str("TeacherName")
                            });
                }

                var recorded = new List<SessionInfo>();
                const string sessionSql = @"SELECT s.SessionID, s.LessonID, s.StartTime, s.EndTime, s.ModuleID, s.Title, s.TeacherID, m.ModuleName,
       CONCAT_WS(' ', t.f_name, t.l_name) AS TeacherName,
       (SELECT COUNT(*) FROM attendance a WHERE a.SessionID = s.SessionID) AS Marked
FROM lesson_sessions s LEFT JOIN modules m ON m.ModuleID = s.ModuleID LEFT JOIN teacher_registry t ON t.TeacherID = s.TeacherID
WHERE s.ClassID = @c AND s.SessionDate = @d ORDER BY s.StartTime";
                using (var command = new MySqlCommand(sessionSql, c))
                {
                    command.Parameters.AddWithValue("@c", classId);
                    command.Parameters.AddWithValue("@d", date);
                    using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                            recorded.Add(new SessionInfo
                            {
                                SessionID = r.GetInt32(0), ClassID = classId, LessonID = r.Int("LessonID"), Date = date, Start = (TimeSpan)r["StartTime"], End = (TimeSpan)r["EndTime"],
                                ModuleID = r.Int("ModuleID"), Title = r.Str("ModuleName") ?? r.Str("Title"), TeacherID = r.Int("TeacherID"), TeacherName = r.Str("TeacherName"),
                                Taken = Convert.ToInt32(r["Marked"]) > 0
                            });
                }

                foreach (var p in planned)
                {
                    var match = recorded.FirstOrDefault(x => x.Start == p.Start);
                    if (match != null) { recorded.Remove(match); result.Add(match); } else result.Add(p);
                }
                result.AddRange(recorded);   // extra lessons
            }
            return result.OrderBy(s => s.Start).ToList();
        }

        /// <summary>The roster for a lesson: its teacher (or the class's teachers) first, then the students, with any saved marks applied.</summary>
        public static async Task<List<AttendanceEntry>> LoadAttendanceAsync(SessionInfo session)
        {
            var rows = new List<AttendanceEntry>();
            var teachers = await GetTeachersAsync(session.ClassID);
            if (session.TeacherID.HasValue) teachers = teachers.Where(t => t.Id == session.TeacherID.Value).ToList();
            foreach (var t in teachers) rows.Add(new AttendanceEntry { PersonType = 'T', PersonID = t.Id, FullName = t.FullName, P_Pic = t.P_Pic, Initials = t.Initials });
            foreach (var s in await GetStudentsAsync(session.ClassID)) rows.Add(new AttendanceEntry { PersonType = 'S', PersonID = s.Id, FullName = s.FullName, P_Pic = s.P_Pic, Initials = s.Initials });

            if (session.SessionID > 0)
            {
                using (var c = await OpenAsync())
                using (var command = new MySqlCommand("SELECT PersonType, PersonID, Status, Note FROM attendance WHERE SessionID = @s", c))
                {
                    command.Parameters.AddWithValue("@s", session.SessionID);
                    using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                        {
                            char type = r.GetString(0)[0]; int id = r.GetInt32(1);
                            var row = rows.FirstOrDefault(x => x.PersonType == type && x.PersonID == id);
                            if (row != null) { row.Status = r.GetString(2); row.Note = r.Str("Note"); }
                        }
                }
            }
            return rows;
        }

        /// <summary>Saves the marks; the lesson is recorded (a session row) the first time.</summary>
        public static async Task<int> SaveAttendanceAsync(SessionInfo session, IEnumerable<AttendanceEntry> rows)
        {
            using (var c = await OpenAsync())
            using (var t = c.BeginTransaction())
            {
                int id = session.SessionID;
                if (id == 0)
                {
                    id = (int)await InsertAsync(c, t,
                        "INSERT INTO lesson_sessions (ClassID, LessonID, SessionDate, StartTime, EndTime, ModuleID, Title, TeacherID) VALUES (@c, @l, @d, @s, @e, @m, @t, @tid) " +
                        "ON DUPLICATE KEY UPDATE EndTime = VALUES(EndTime), ModuleID = VALUES(ModuleID), Title = VALUES(Title), TeacherID = VALUES(TeacherID), SessionID = LAST_INSERT_ID(SessionID)",
                        ("@c", session.ClassID), ("@l", session.LessonID), ("@d", session.Date.Date), ("@s", session.Start), ("@e", session.End),
                        ("@m", session.ModuleID), ("@t", session.Title), ("@tid", session.TeacherID));
                }
                foreach (var row in rows)
                    await NonQueryAsync(c, t,
                        "INSERT INTO attendance (SessionID, PersonType, PersonID, Status, Note) VALUES (@s, @pt, @p, @st, @n) " +
                        "ON DUPLICATE KEY UPDATE Status = VALUES(Status), Note = VALUES(Note)",
                        ("@s", id), ("@pt", row.PersonType.ToString()), ("@p", row.PersonID), ("@st", row.Status),
                        ("@n", string.IsNullOrWhiteSpace(row.Note) ? null : row.Note.Trim()));
                t.Commit();
                return id;
            }
        }

        public static async Task DeleteSessionAsync(int sessionId)
        {
            if (sessionId == 0) return;
            using (var c = await OpenAsync())
            using (var t = c.BeginTransaction())
            {
                await NonQueryAsync(c, t, "DELETE FROM attendance WHERE SessionID = @s", ("@s", sessionId));
                await NonQueryAsync(c, t, "DELETE FROM lesson_sessions WHERE SessionID = @s", ("@s", sessionId));
                t.Commit();
            }
        }

        /// <summary>Totals per student and per teacher between two dates; over <paramref name="alertAbsences"/> absences is flagged.</summary>
        public static async Task<List<AttendanceReportRow>> GetReportAsync(int classId, DateTime from, DateTime to, int alertAbsences = 3)
        {
            var list = new List<AttendanceReportRow>();
            const string sql = @"
SELECT a.PersonType, a.PersonID,
       CASE a.PersonType WHEN 'S' THEN CONCAT_WS(' ', s.f_name, s.l_name) ELSE CONCAT_WS(' ', t.f_name, t.l_name) END AS FullName,
       CASE a.PersonType WHEN 'S' THEN s.p_pic ELSE t.p_pic END AS Pic,
       SUM(a.Status = 'P') AS P, SUM(a.Status = 'L') AS L, SUM(a.Status = 'A') AS A, SUM(a.Status = 'E') AS E
FROM attendance a
JOIN lesson_sessions ls ON ls.SessionID = a.SessionID
LEFT JOIN student_registry s ON a.PersonType = 'S' AND s.StudentID = a.PersonID
LEFT JOIN teacher_registry t ON a.PersonType = 'T' AND t.TeacherID = a.PersonID
WHERE ls.ClassID = @c AND ls.SessionDate BETWEEN @f AND @t
GROUP BY a.PersonType, a.PersonID, FullName, Pic
ORDER BY a.PersonType DESC, FullName";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                command.Parameters.AddWithValue("@f", from.Date);
                command.Parameters.AddWithValue("@t", to.Date);
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                    while (await r.ReadAsync())
                    {
                        string name = r.Str("FullName");
                        if (string.IsNullOrWhiteSpace(name)) continue;       // the person was deleted from the registry
                        var parts = name.Split(' ');
                        var row = new AttendanceReportRow
                        {
                            PersonType = r.GetString(0)[0], PersonID = r.GetInt32(1), FullName = name, P_Pic = r.Str("Pic"),
                            Initials = NameHelper.Initials(parts[0], parts.Length > 1 ? parts[parts.Length - 1] : ""),
                            Present = Convert.ToInt32(r["P"]), Late = Convert.ToInt32(r["L"]), Absent = Convert.ToInt32(r["A"]), Excused = Convert.ToInt32(r["E"])
                        };
                        row.NeedsAttention = row.Absent >= alertAbsences;
                        list.Add(row);
                    }
            }
            return list;
        }

        /// <summary>Attendance rate of the whole class over a range (present or late / all marks), or null when nothing was recorded.</summary>
        public static async Task<double?> GetClassRateAsync(int classId, DateTime from, DateTime to)
        {
            const string sql = @"SELECT SUM(a.Status IN ('P','L')) AS Good, COUNT(*) AS Total FROM attendance a
JOIN lesson_sessions ls ON ls.SessionID = a.SessionID WHERE ls.ClassID = @c AND a.PersonType = 'S' AND ls.SessionDate BETWEEN @f AND @t";
            using (var c = await OpenAsync())
            using (var command = new MySqlCommand(sql, c))
            {
                command.Parameters.AddWithValue("@c", classId);
                command.Parameters.AddWithValue("@f", from.Date);
                command.Parameters.AddWithValue("@t", to.Date);
                using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (!await r.ReadAsync() || r.IsDBNull(0)) return null;
                    double total = Convert.ToDouble(r["Total"]);
                    return total == 0 ? (double?)null : Convert.ToDouble(r["Good"]) * 100.0 / total;
                }
            }
        }

        // ================================================================ assessments and grades

        public static async Task<GradeBook> GetGradeBookAsync(int classId)
        {
            var book = new GradeBook { Modules = await GetModulesAsync(classId) };
            using (var c = await OpenAsync())
            {
                using (var command = new MySqlCommand(
                    "SELECT a.AssessmentID, a.ModuleID, a.Title, a.Kind, a.Weight, a.MaxScore, a.AssessDate FROM assessments a JOIN modules m ON m.ModuleID = a.ModuleID " +
                    "WHERE m.ClassID = @c ORDER BY a.ModuleID, a.AssessDate, a.AssessmentID", c))
                {
                    command.Parameters.AddWithValue("@c", classId);
                    using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                            book.Assessments.Add(new AssessmentInfo
                            {
                                AssessmentID = r.GetInt32(0), ModuleID = r.GetInt32(1), Title = r.Str("Title"), Kind = r.Str("Kind"),
                                Weight = Convert.ToDecimal(r["Weight"]), MaxScore = Convert.ToDecimal(r["MaxScore"]), Date = r.Date("AssessDate")
                            });
                }
                using (var command = new MySqlCommand(
                    "SELECT sc.AssessmentID, sc.StudentID, sc.Score, sc.Flag FROM assessment_scores sc JOIN assessments a ON a.AssessmentID = sc.AssessmentID " +
                    "JOIN modules m ON m.ModuleID = a.ModuleID WHERE m.ClassID = @c", c))
                {
                    command.Parameters.AddWithValue("@c", classId);
                    using (var r = (MySqlDataReader)await command.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                        {
                            string text = !r.IsDBNull(2) ? Convert.ToDecimal(r["Score"]).ToString("0.##", CultureInfo.InvariantCulture) : (r.Str("Flag") ?? "");
                            book.Raw[(r.GetInt32(0), r.GetInt32(1))] = text;
                        }
                }
            }
            foreach (var s in await GetStudentsAsync(classId))
                book.Students.Add(new GradeRow { StudentID = s.Id, FullName = s.FullName, P_Pic = s.P_Pic, Initials = s.Initials });
            return book;
        }

        public static async Task<int> SaveAssessmentAsync(AssessmentInfo a)
        {
            using (var c = await OpenAsync())
            {
                if (a.AssessmentID == 0)
                    return (int)await InsertAsync(c, null, "INSERT INTO assessments (ModuleID, Title, Kind, Weight, MaxScore, AssessDate) VALUES (@m, @t, @k, @w, @x, @d)",
                        ("@m", a.ModuleID), ("@t", a.Title), ("@k", a.Kind), ("@w", a.Weight), ("@x", a.MaxScore), ("@d", a.Date));
                await NonQueryAsync(c, null, "UPDATE assessments SET Title = @t, Kind = @k, Weight = @w, MaxScore = @x, AssessDate = @d WHERE AssessmentID = @id",
                    ("@t", a.Title), ("@k", a.Kind), ("@w", a.Weight), ("@x", a.MaxScore), ("@d", a.Date), ("@id", a.AssessmentID));
                return a.AssessmentID;
            }
        }

        public static async Task DeleteAssessmentAsync(int assessmentId)
        {
            using (var c = await OpenAsync())
            using (var t = c.BeginTransaction())
            {
                await NonQueryAsync(c, t, "DELETE FROM assessment_scores WHERE AssessmentID = @a", ("@a", assessmentId));
                await NonQueryAsync(c, t, "DELETE FROM assessments WHERE AssessmentID = @a", ("@a", assessmentId));
                t.Commit();
            }
        }

        /// <summary>Saves the changed cells. Empty text removes the score; a number is the score; a short word (ABS, EXC) is a flag.</summary>
        public static async Task SaveScoresAsync(IEnumerable<ScoreCell> cells)
        {
            using (var c = await OpenAsync())
            using (var t = c.BeginTransaction())
            {
                foreach (var cell in cells)
                {
                    string text = (cell.Text ?? "").Trim().Replace(',', '.');
                    var key = (("@a", (object)cell.Assessment.AssessmentID), ("@s", (object)cell.StudentID));
                    if (text.Length == 0)
                        await NonQueryAsync(c, t, "DELETE FROM assessment_scores WHERE AssessmentID = @a AND StudentID = @s", key.Item1, key.Item2);
                    else if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal score))
                        await NonQueryAsync(c, t, "INSERT INTO assessment_scores (AssessmentID, StudentID, Score, Flag) VALUES (@a, @s, @v, NULL) ON DUPLICATE KEY UPDATE Score = VALUES(Score), Flag = NULL",
                            key.Item1, key.Item2, ("@v", score));
                    else
                        await NonQueryAsync(c, t, "INSERT INTO assessment_scores (AssessmentID, StudentID, Score, Flag) VALUES (@a, @s, NULL, @f) ON DUPLICATE KEY UPDATE Score = NULL, Flag = VALUES(Flag)",
                            key.Item1, key.Item2, ("@f", text.ToUpperInvariant()));
                }
                t.Commit();
            }
        }

        /// <summary>True when the text is empty, a number from 0 to <paramref name="max"/>, or a short flag such as ABS.</summary>
        public static bool IsValidScore(string text, decimal max)
        {
            text = (text ?? "").Trim().Replace(',', '.');
            if (text.Length == 0) return true;
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal v)) return v >= 0 && v <= max;
            return text.Length <= 5 && text.All(char.IsLetter);
        }
    }
}
