# EduTrack

EduTrack is a Windows desktop register for schools and training centres. It keeps students, teachers, staff and classes in one place: who they are, which documents you hold, who is in which class, when each class meets, who was present, and how everyone is graded.

![Home screen](docs/screenshots/home.png)

*The screenshots use demo data.*

## What's new

- **A new Classes workspace.** Pick a class once and work on it: Overview, People, Attendance, Grades and Timetable, all about that class. No more picking the class again on every page.
- **Attendance, per lesson.** Mark teachers and students Present, Absent, Late or Excused for each lesson of the timetable, then see who is missing too often in a report.
- **Grades with assessments.** Each module has exams, quizzes, homework and projects with their own weight. Averages, ranking and pass/fail are worked out live.
- **Eight languages**, including Arabic with the whole layout mirrored right to left.
- **Documents open inside the app**, images and PDFs, with zoom and drag.
- **An installer with its own database.** One Setup file, nothing else to install, and the database only runs while EduTrack is open.
- **A Home dashboard that fits the window** without scrolling, with new cards for majors, contracts about to end and incomplete records.
- **Smaller things:** the window now respects the Windows taskbar when maximised, a tray icon lets you open or quit EduTrack, and the About page has a Ko-fi button.

## Classes

![Class overview](docs/screenshots/class-overview.png)

Choose a class from the drop-down at the top (on wide screens it becomes a list on the left). Everything below is about that class.

- **Overview**: students, teachers, modules, attendance over the last 30 days and the class average. Today's lessons are listed with a one-click *Take attendance*. *Needs attention* shows people with three or more absences in the last 30 days, and assessments that still have scores missing. A table shows which teacher teaches which module and each module's class average.
- **People**: add and remove students and teachers, and search the class. A student belongs to one class at a time, so adding one who is elsewhere moves them (after asking).
- **Timetable**: the week, one column per day. Each lesson can be tied to a **module**, and its **teacher** follows from the module. Overlapping lessons are refused.
- **Attendance** and **Grades**: described below.

![Class overview on a wide screen](docs/screenshots/class-overview-wide.png)

### Attendance

![Attendance roll call](docs/screenshots/class-attendance.png)

1. Pick the **date** (arrows, calendar or *Today*). The lessons of that weekday appear from the timetable. *Extra lesson* adds a one-off lesson for that day.
2. Pick the **lesson**. Its teacher and the class's students are listed, everyone starting as *Present*.
3. Mark **Present / Absent / Late / Excused**. Keys `P`, `A`, `L` and `E` mark the selected row and move to the next. A note box appears for anyone who is not simply present. *Mark all present* resets the list.
4. **Save attendance.** If you move to another lesson, day or tab with unsaved marks, EduTrack asks (or saves them when you leave the tab).

The **Report** view adds up each student's (or teacher's) marks between two dates, with an attendance rate, and highlights people with three or more absences.

![Attendance report](docs/screenshots/class-attendance-report.png)

### Grades

![Grades of a module](docs/screenshots/class-grades.png)

Each **module** has a name, a teacher and a **coefficient**. Each module has **assessments** (exam, quiz, homework, project or other), each with a **weight** and a maximum score. In the grid you type a score in a cell; a number, or `ABS` / `EXC` when the student was absent or excused. Those two are left out of the average instead of counting as zero.

- Module average = the scores out of 20, weighted by each assessment's weight.
- Overall average = the module averages, weighted by each module's coefficient.
- The pass mark is 10 out of 20. Students are ranked by overall average (ties share a rank).

The **Summary** view shows every module, the overall average, the rank and the result for the whole class, with the class average, highest and lowest below.

![Grades summary](docs/screenshots/class-grades-summary.png)

![Timetable](docs/screenshots/class-timetable.png)

![People in a class](docs/screenshots/class-people.png)

## Everything else

- **Students, teachers and employees**: search by name, open a record, edit it, attach scans of ID cards, contracts, resumes and diplomas, delete it.
- **Documents open inside EduTrack.** The eye icon next to a file opens it over the app, not in a new window. Images and PDFs both work. Zoom with the mouse wheel, `+` / `-` or the buttons, drag to move around, double-click to switch between fit-to-screen and full size, `Esc` to close. PDFs show one page at a time (arrow keys, or the page buttons). Reading PDFs needs Windows 10 or later.
- **Home**: live counts, the newest students, class sizes, students by major, contracts ending within 90 days and records missing a photo or an ID scan. It fits the window at any size.
- **Backup and restore**: save the students, teachers or employees list to an XML file and restore it later. This covers those three lists; attendance and grades live in the database, so back the database up too.
- **Tray icon**: *Open EduTrack* and *Exit EduTrack* from the Windows notification area. Exiting closes the built-in database.
- **Keyboard**: `Ctrl+1` to `Ctrl+7` jump between Home, Students, Teachers, Employees, Classes, Settings and About.

| Students | New student |
| --- | --- |
| ![Students](docs/screenshots/students.png) | ![New student](docs/screenshots/new-student.png) |

![A document open inside the app](docs/screenshots/document-viewer.png)

### Languages

English, Français, Español, العربية, Bahasa Indonesia, 中文（简体）, 日本語 and 한국어. Open **Settings > Appearance** and pick one in the *Language* drop-down; the whole app changes at once and EduTrack remembers your choice. Arabic mirrors the layout from right to left. What you type (names, majors, class names) is never translated, and the country list and the Male/Female values stay in English because they are stored that way.

| Arabic (right to left) | Japanese |
| --- | --- |
| ![Arabic](docs/screenshots/language-arabic.png) | ![Japanese](docs/screenshots/language-japanese.png) |

### Themes

Three themes: **Classic** (the original dark blue look, the default), **Paper** (light green) and **Slate** (dark green). Pick one in Settings, or cycle with the button in the sidebar. EduTrack remembers your choice.

| Paper | Slate |
| --- | --- |
| ![Paper theme](docs/screenshots/theme-paper.png) | ![Slate theme](docs/screenshots/theme-slate.png) |

| Settings | Backup and restore |
| --- | --- |
| ![Settings](docs/screenshots/settings.png) | ![Backup and restore](docs/screenshots/backup.png) |

| About | First run |
| --- | --- |
| ![About](docs/screenshots/about.png) | ![First run: create the administrator account](docs/screenshots/sign-in.png) |

## Installation

EduTrack needs Windows 10 or 11 (64-bit). There are two ways to run it.

### Option A: the installer (recommended)

Run **`EduTrack Installer V2.exe`**. It installs for your Windows account by default (no administrator prompt), or for all users if you choose. **Nothing else needs to be installed**: EduTrack carries its own private MariaDB database.

- The database starts when you open EduTrack and **stops when you close it**, so nothing keeps running in the background.
- It is **not reachable over the network**. EduTrack talks to it through a private local channel, and its password is random, made on your computer and stored encrypted for your Windows user.
- Your data lives in `%LOCALAPPDATA%\EduTrack\data`. Uninstalling keeps it unless you answer Yes to the question the uninstaller asks.
- On first start EduTrack creates the database and opens the sign-in window in *Create account* mode. Choose a username and a password of at least 6 characters.

The installer is not code-signed yet, so Windows SmartScreen may warn when you run it.

**Building the installer yourself:** install [Inno Setup 6](https://jrsoftware.org/isinfo.php) (`winget install JRSoftware.InnoSetup`), have MariaDB installed (its files are copied into the installer; the default path is `C:\Program Files\MariaDB 13.0`), then run

```
powershell -ExecutionPolicy Bypass -File installer\build.ps1
```

The result is `installer\output\EduTrack Installer V2.exe`. Pass `-Version 2.1.0` to change the version shown in Windows, or `-MariaDb "D:\path\to\MariaDB"` to use a MariaDB installed somewhere else.

### Option B: use your own MySQL or MariaDB server

1. **Install a MySQL-compatible server.** XAMPP ([Apache Friends](https://www.apachefriends.org/index.html)), MySQL or MariaDB all work. Start MySQL (in XAMPP, press Start next to MySQL).
2. **Create the database.** Open phpMyAdmin (or any MySQL client), create a database named `database`, select it and import `installer/schema.sql`, the complete current schema. (The older `datebase.sql` also works; EduTrack adds the missing tables when it starts.)
3. **Run EduTrack.** Open `EduTrack.sln` in Visual Studio 2022 and press F5, or build from a terminal:

   ```
   msbuild EduTrack.sln -restore -p:Configuration=Release
   ```

   The program is `EduTrack\bin\Release\EduTrack.exe`.
4. **Create your account.** On a new database the sign-in window opens in "Create account" mode.

If the database is somewhere else, or has a different name or password, edit the `appSettings` in `EduTrack.exe.config` (or `App.config` before building):

```xml
<add key="Db.Server" value="127.0.0.1" />
<add key="Db.Port" value="3306" />
<add key="Db.Name" value="database" />
<add key="Db.User" value="root" />
<add key="Db.Password" value="" />
```

These settings are only used when EduTrack has no built-in database next to it (that is, outside the installed version). On start-up EduTrack checks the connection and tells you what is wrong if it can't reach the database.

## Notes for upgrading

From the 2024 version:

- Passwords are now stored as salted hashes. Existing accounts keep working and are upgraded the first time they sign in.
- On start-up EduTrack makes small, repeatable changes to an older database: it widens `login.Password` so a hash fits, and it adds a unique key on `student_module (StudentID, ModuleID)`.
- Saving a student, teacher or employee with an empty date now stores an empty date instead of `0001-01-01`.
- The program has always connected to a database called `database` (the old README said `datebase`).

For the Classes workspace, EduTrack creates these tables when it starts (nothing to do by hand): `class_lessons`, `lesson_sessions`, `attendance`, `assessments`, `assessment_scores` and `app_flags`, and adds `TeacherID` and `Coefficient` to `modules`.

- **Your old timetable** (the free-text weekly grid) is turned into lessons once. The old `classschedules` table is left untouched.
- **Your old grades** (one value per student and module) are copied once into a "Final grade" assessment for each module. The old `student_module` table is left untouched.
- Teachers are not attached to modules in older data, so open a module in **Grades** and choose its teacher and coefficient.

## Design

The default theme, Classic, keeps the original charcoal and cyan colours. The optional Paper and Slate themes use a chalkboard-green sidebar, paper-white (or dark) pages and one pencil-yellow accent. All colours live in `EduTrack/Styles/Theme.Classic.xaml`, `Theme.Light.xaml` (Paper) and `Theme.Dark.xaml` (Slate), and every control (buttons, inputs, tables, scroll bars) is styled in `Styles/Controls.xaml`, so changing the palette is a one-file edit.

Text is set in [Atkinson Hyperlegible](https://brailleinstitute.org/freefont), designed by the Braille Institute for legibility. It is bundled in `EduTrack/Fonts` under the SIL Open Font License.

Translations are plain dictionaries in `EduTrack/Utilities/Loc*.cs` (one file per language, plus `LocClasses.cs` for the class workspace), keyed by the English text, so adding a language means adding one file.

## License

This project is licensed under the MIT License. See the LICENSE.txt file for details.

The installer bundles MariaDB Server, which is licensed under the GPL v2; its licence files are installed alongside it in the `db` folder.

## Acknowledgements

EduTrack is supported by GitHub Student Pack and Expertus.ma.

## Contact

For more information, visit [GitHub](https://github.com/AdRohal/EduTrack-) or contact Adam Rohal at rohal.dev@hotmail.com .

## Support the developer

EduTrack is built and kept up to date by one person. If it saves you time, a coffee helps keep it free and improving.

[![Support me on Ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/adrohal)

Portfolio: [adamrohal.tech](https://adamrohal.tech) · Ko-fi: [ko-fi.com/adrohal](https://ko-fi.com/adrohal)
