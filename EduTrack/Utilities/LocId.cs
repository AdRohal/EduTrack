using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> Indonesian.</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Id = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "Beranda", ["Students"] = "Siswa", ["Teachers"] = "Guru", ["Employees"] = "Karyawan",
            ["Classes"] = "Kelas", ["Settings"] = "Pengaturan", ["About"] = "Tentang",
            ["Home  (Ctrl+1)"] = "Beranda  (Ctrl+1)", ["Students  (Ctrl+2)"] = "Siswa  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "Guru  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "Karyawan  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "Kelas  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "Pengaturan  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "Tentang  (Ctrl+7)",
            ["Administrator"] = "Administrator", ["Change theme"] = "Ganti tema", ["Sign out"] = "Keluar",
            ["Minimize"] = "Kecilkan", ["Maximize"] = "Besarkan", ["Restore"] = "Pulihkan", ["Close"] = "Tutup",
            ["Student"] = "Siswa", ["Teacher"] = "Guru", ["Employee"] = "Karyawan", ["Class"] = "Kelas",
            ["OK"] = "OK", ["Yes"] = "Ya", ["No"] = "Tidak", ["Cancel"] = "Batal", ["Delete"] = "Hapus", ["Keep it"] = "Pertahankan",
            ["Something went wrong"] = "Terjadi kesalahan", ["Check this first"] = "Periksa ini dulu", ["Please confirm"] = "Mohon konfirmasi",
            ["year"] = "tahun", ["years"] = "tahun", ["month"] = "bulan", ["months"] = "bulan",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - Masuk",
            ["One register for your whole school."] = "Satu register untuk seluruh sekolah Anda.",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "Siswa, guru, staf, dan kelas, tersimpan bersama dan mudah ditemukan.",
            ["Sign in"] = "Masuk", ["Use your administrator account to open the register."] = "Gunakan akun administrator untuk membuka register.",
            ["Signed in last time"] = "Masuk terakhir kali", ["Username"] = "Nama pengguna", ["Password"] = "Kata sandi",
            ["Confirm password"] = "Konfirmasi kata sandi", ["At least 6 characters."] = "Minimal 6 karakter.",
            ["Not you? Use another account"] = "Bukan Anda? Gunakan akun lain", ["First time here?"] = "Baru pertama kali?", ["Create an account"] = "Buat akun",
            ["Connecting to the database"] = "Menghubungkan ke basis data", ["Starting the database"] = "Memulai basis data",
            ["Getting things ready"] = "Menyiapkan semuanya", ["Ready"] = "Siap", ["Can't reach the database"] = "Basis data tidak dapat dijangkau",
            ["Try again"] = "Coba lagi", ["Open anyway"] = "Buka saja",
            ["No accounts exist yet. Create the first administrator to get started."] = "Belum ada akun. Buat administrator pertama untuk memulai.",
            ["Welcome back"] = "Selamat datang kembali", ["Enter your password to continue."] = "Masukkan kata sandi untuk melanjutkan.",
            ["Enter your username."] = "Masukkan nama pengguna.", ["Enter your password."] = "Masukkan kata sandi.",
            ["Use at least 6 characters for the password."] = "Gunakan minimal 6 karakter untuk kata sandi.",
            ["The two passwords don't match."] = "Kedua kata sandi tidak cocok.",
            ["That username and password don't match. Check them and try again."] = "Nama pengguna dan kata sandi tidak cocok. Periksa lalu coba lagi.",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "Basis data tidak dapat dijangkau. Pastikan MySQL berjalan, lalu coba lagi.",
            ["EduTrack is already running."] = "EduTrack sudah berjalan.",
            ["Unexpected error"] = "Kesalahan tak terduga",
            ["Open EduTrack"] = "Buka EduTrack", ["Exit EduTrack"] = "Keluar dari EduTrack",

            // ---- home
            ["Good morning"] = "Selamat pagi", ["Good afternoon"] = "Selamat siang", ["Good evening"] = "Selamat malam",
            ["Here is what the register holds right now."] = "Inilah isi register saat ini.",
            ["Welcome. Let's set up your school."] = "Selamat datang. Mari siapkan sekolah Anda.",
            ["New class"] = "Kelas baru", ["New teacher"] = "Guru baru", ["New student"] = "Siswa baru", ["New employee"] = "Karyawan baru",
            ["Your register is empty. Start here."] = "Register Anda masih kosong. Mulai dari sini.",
            ["Set things up in this order and everything else falls into place."] = "Atur dengan urutan ini dan semuanya akan tertata.",
            ["Create a class"] = "Buat kelas", ["A class is the group students and teachers belong to."] = "Kelas adalah kelompok tempat siswa dan guru bergabung.",
            ["Add your teachers"] = "Tambahkan guru Anda", ["Contracts, modules and documents live on each teacher's page."] = "Kontrak, modul, dan dokumen ada di halaman setiap guru.",
            ["Enrol your students"] = "Daftarkan siswa Anda", ["Pick a class for each student while you add them."] = "Pilih kelas untuk setiap siswa saat menambahkannya.",
            ["Recently added students"] = "Siswa yang baru ditambahkan", ["See all"] = "Lihat semua",
            ["Students you add will be listed here."] = "Siswa yang Anda tambahkan akan muncul di sini.",
            ["Class sizes"] = "Jumlah siswa per kelas", ["Classes you create will show how many students they hold."] = "Kelas yang Anda buat akan menampilkan jumlah siswanya.",
            ["Students by major"] = "Siswa menurut jurusan", ["Majors appear here once students have one."] = "Jurusan muncul di sini setelah siswa memilikinya.",
            ["Contracts ending soon"] = "Kontrak segera berakhir", ["Teachers and employees, next 90 days"] = "Guru dan karyawan, 90 hari ke depan",
            ["No contract ends in the next 90 days."] = "Tidak ada kontrak yang berakhir dalam 90 hari ke depan.",
            ["Records to complete"] = "Data yang perlu dilengkapi", ["Missing a photo or an ID card scan"] = "Foto atau pindaian kartu identitas belum ada",
            ["Every record has a photo and both ID scans."] = "Setiap data memiliki foto dan kedua pindaian kartu identitas.",
            ["today"] = "hari ini",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "Staf administrasi dan pendukung, beserta kontrak dan gaji.",
            ["All employees"] = "Semua karyawan", ["Everyone enrolled, with their contact details and documents."] = "Semua yang terdaftar, beserta kontak dan dokumennya.",
            ["All students"] = "Semua siswa", ["The teaching staff, their modules and contracts."] = "Tenaga pengajar, modul, dan kontraknya.",
            ["All teachers"] = "Semua guru", ["All classes"] = "Semua kelas",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "Kelompok siswa: siapa yang mengajar, kapan bertemu, dan bagaimana penilaiannya.",
            ["Schedule"] = "Jadwal", ["Grades"] = "Nilai",
            ["Reload the list"] = "Muat ulang daftar", ["Open details"] = "Buka detail",
            ["Joined"] = "Bergabung", ["Contract ends"] = "Kontrak berakhir",
            ["Add the first employee"] = "Tambahkan karyawan pertama", ["Add the first student"] = "Tambahkan siswa pertama", ["Add the first teacher"] = "Tambahkan guru pertama",
            ["Create the first class"] = "Buat kelas pertama", ["Choose a class"] = "Pilih kelas",
            ["Pick a class above to see its teachers and students."] = "Pilih kelas di atas untuk melihat guru dan siswanya.",
            ["No employees yet"] = "Belum ada karyawan", ["Employees you add will show up here, ready to search and open."] = "Karyawan yang Anda tambahkan akan muncul di sini, siap dicari dan dibuka.",
            ["No students yet"] = "Belum ada siswa", ["Students you add will show up here, ready to search and open."] = "Siswa yang Anda tambahkan akan muncul di sini, siap dicari dan dibuka.",
            ["No teachers yet"] = "Belum ada guru", ["Teachers you add will show up here, ready to search and open."] = "Guru yang Anda tambahkan akan muncul di sini, siap dicari dan dibuka.",
            ["Check the spelling, or try just a first or last name."] = "Periksa ejaan, atau coba hanya nama depan atau nama belakang.",
            ["Add to this class"] = "Tambahkan ke kelas ini", ["A student"] = "Seorang siswa", ["A teacher"] = "Seorang guru", ["Add to class"] = "Tambahkan ke kelas",
            ["Remove from this class"] = "Keluarkan dari kelas ini",
            ["No teacher has been assigned to this class yet."] = "Belum ada guru yang ditugaskan ke kelas ini.",
            ["No student is in this class yet. Use the Add to class bar above."] = "Belum ada siswa di kelas ini. Gunakan bilah «Tambahkan ke kelas» di atas.",
            ["Delete class"] = "Hapus kelas", ["Class details"] = "Detail kelas",
            ["A class groups students, teachers, a weekly schedule and grades."] = "Kelas menghimpun siswa, guru, jadwal mingguan, dan nilai.",
            ["Class name *"] = "Nama kelas *", ["Start date"] = "Tanggal mulai", ["Description"] = "Deskripsi", ["Save class"] = "Simpan kelas",
            ["Last class added"] = "Kelas terakhir ditambahkan",
            ["No class has been added yet. The one you save will appear here."] = "Belum ada kelas yang ditambahkan. Kelas yang Anda simpan akan muncul di sini.",
            ["The last class could not be loaded."] = "Kelas terakhir tidak dapat dimuat.", ["no date"] = "tanpa tanggal",

            // ---- grades / schedule
            ["Module"] = "Modul", ["Add a module to this class"] = "Tambahkan modul ke kelas ini", ["Add module"] = "Tambah modul",
            ["Grade"] = "Nilai", ["No grades to enter yet"] = "Belum ada nilai untuk dimasukkan",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "Pilih kelas dan modul di atas. Siswa kelas itu akan muncul di sini, siap diberi nilai.",
            ["Save grades"] = "Simpan nilai",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "Klik sel lalu ketik mata pelajaran atau ruangan. Tab pindah ke hari berikutnya.",
            ["Unsaved changes"] = "Perubahan belum disimpan", ["Save schedule"] = "Simpan jadwal", ["Time"] = "Waktu",
            ["Monday"] = "Senin", ["Tuesday"] = "Selasa", ["Wednesday"] = "Rabu", ["Thursday"] = "Kamis", ["Friday"] = "Jumat", ["Saturday"] = "Sabtu", ["Sunday"] = "Minggu",
            ["No classes to schedule yet"] = "Belum ada kelas untuk dijadwalkan", ["Create a class first, then plan its week here."] = "Buat kelas terlebih dahulu, lalu rencanakan minggunya di sini.",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "Gunakan angka 0 sampai 20, atau kata singkat seperti ABS.",
            ["Check the grades"] = "Periksa nilai",

            // ---- forms / details
            ["Back to the list"] = "Kembali ke daftar", ["Back to employees"] = "Kembali ke karyawan", ["Back to students"] = "Kembali ke siswa", ["Back to teachers"] = "Kembali ke guru",
            ["Employee details"] = "Detail karyawan", ["Student details"] = "Detail siswa", ["Teacher details"] = "Detail guru",
            ["Edit anything below, then choose Save changes."] = "Ubah apa saja di bawah, lalu pilih Simpan perubahan.",
            ["Choose photo"] = "Pilih foto", ["JPG or PNG"] = "JPG atau PNG",
            ["Personal information"] = "Informasi pribadi", ["Fields marked with * are required."] = "Kolom bertanda * wajib diisi.",
            ["First name *"] = "Nama depan *", ["Middle name"] = "Nama tengah", ["Last name *"] = "Nama belakang *", ["First name"] = "Nama depan", ["Last name"] = "Nama belakang",
            ["Gender"] = "Jenis kelamin", ["Email"] = "Email", ["Phone"] = "Telepon", ["Date of birth"] = "Tanggal lahir", ["Nationality"] = "Kewarganegaraan",
            ["ID card (CIN)"] = "Kartu identitas (CIN)", ["Address"] = "Alamat",
            ["Professional details"] = "Detail profesional", ["Organization"] = "Organisasi", ["Role"] = "Jabatan",
            ["Date of appointment"] = "Tanggal pengangkatan", ["Experience"] = "Pengalaman", ["Contract"] = "Kontrak", ["Contract start"] = "Mulai kontrak",
            ["Contract end"] = "Akhir kontrak", ["Duration"] = "Durasi", ["Salary (MAD per month)"] = "Gaji (MAD per bulan)",
            ["Education"] = "Pendidikan", ["Highest degree"] = "Gelar tertinggi", ["Graduation year"] = "Tahun kelulusan", ["University"] = "Universitas",
            ["Specialization"] = "Spesialisasi", ["Module taught"] = "Modul yang diajarkan",
            ["Documents"] = "Dokumen", ["Scans or photos. Open any of them from here later."] = "Pindaian atau foto. Buka kapan saja dari sini.",
            ["ID card, front"] = "Kartu identitas, depan", ["ID card, back"] = "Kartu identitas, belakang", ["Open the file"] = "Buka file",
            ["Resume"] = "CV", ["Diploma"] = "Ijazah", ["Choose file"] = "Pilih file", ["No file chosen"] = "Belum ada file dipilih",
            ["Delete employee"] = "Hapus karyawan", ["Delete student"] = "Hapus siswa", ["Delete teacher"] = "Hapus guru",
            ["Save changes"] = "Simpan perubahan", ["Save employee"] = "Simpan karyawan", ["Save student"] = "Simpan siswa", ["Save teacher"] = "Simpan guru",
            ["Enrolment"] = "Pendaftaran", ["Where the student comes from and which class they join."] = "Asal siswa dan kelas yang akan diikutinya.",
            ["School or university"] = "Sekolah atau universitas", ["Major"] = "Jurusan", ["Joining date"] = "Tanggal bergabung", ["Previous studies."] = "Pendidikan sebelumnya.",
            ["High school or university"] = "SMA atau universitas", ["Emergency contact"] = "Kontak darurat",
            ["Who to call first, usually a parent or guardian."] = "Siapa yang dihubungi pertama, biasanya orang tua atau wali.",
            ["Relation"] = "Hubungan", ["Emergency contact ID card, front"] = "Kartu identitas kontak darurat, depan",
            ["Emergency contact ID card, back"] = "Kartu identitas kontak darurat, belakang", ["Male"] = "Laki-laki", ["Female"] = "Perempuan", ["Select"] = "Pilih",
            ["Search by first, middle or last name"] = "Cari berdasarkan nama depan, tengah, atau belakang",
            ["Choose who to add"] = "Pilih siapa yang ditambahkan", ["Anything worth knowing about this class"] = "Hal penting yang perlu diketahui tentang kelas ini",
            ["Calculated from the appointment date"] = "Dihitung dari tanggal pengangkatan", ["Calculated"] = "Dihitung", ["Choose a module"] = "Pilih modul",
            ["Parent, guardian..."] = "Orang tua, wali...", ["e.g. 2A Computer Science"] = "mis. 2A Ilmu Komputer", ["e.g. Algebra"] = "mis. Aljabar", ["e.g. Computer Science"] = "mis. Ilmu Komputer",
            ["Search this class"] = "Cari di kelas ini", ["Every teacher is already in this class"] = "Semua guru sudah ada di kelas ini",
            ["Every student is already in this class"] = "Semua siswa sudah ada di kelas ini", ["No major"] = "Tanpa jurusan", ["started"] = "mulai",

            // ---- document viewer
            ["Previous page"] = "Halaman sebelumnya", ["Next page"] = "Halaman berikutnya", ["Zoom out  (-)"] = "Perkecil  (-)", ["Zoom in  (+)"] = "Perbesar  (+)",
            ["Fit to screen  (0 or double-click)"] = "Sesuaikan layar  (0 atau klik dua kali)", ["Close  (Esc)"] = "Tutup  (Esc)", ["Document"] = "Dokumen",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "Tampilan EduTrack, dan cara menyimpan salinan data Anda.",
            ["Appearance"] = "Tampilan", ["Backup and restore"] = "Cadangan dan pemulihan", ["Theme"] = "Tema", ["Language"] = "Bahasa",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "Klasik adalah tampilan asli EduTrack. Kertas dan Batu Tulis adalah alternatif hijau. EduTrack mengingat pilihan Anda.",
            ["Classic (original)"] = "Klasik (asli)", ["Paper (light green)"] = "Kertas (hijau muda)", ["Slate (dark green)"] = "Batu Tulis (hijau tua)",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "Pilih bahasa seluruh aplikasi. EduTrack mengingat pilihan Anda.",
            ["Save a copy"] = "Simpan salinan", ["Export one list to an XML file you can keep safe or move to another computer."] = "Ekspor satu daftar ke file XML yang dapat Anda simpan atau pindahkan ke komputer lain.",
            ["What to save"] = "Yang akan disimpan", ["Save to file"] = "Simpan ke file", ["Show in folder"] = "Tampilkan di folder",
            ["Restore from a copy"] = "Pulihkan dari salinan", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "Pilih file yang disimpan di atas. Data dengan ID yang sama akan diganti dengan data dalam file.",
            ["Restore data"] = "Pulihkan data",
            ["Choose what to save first."] = "Pilih dulu apa yang akan disimpan.", ["Choose a saved copy"] = "Pilih salinan tersimpan",
            ["Choose a file to restore first."] = "Pilih dulu file yang akan dipulihkan.", ["That file has no records to restore."] = "File itu tidak memiliki data untuk dipulihkan.",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "EduTrack hanya dapat memulihkan siswa, guru, atau karyawan yang disimpan dengan tombol «Simpan salinan».",
            ["Couldn't save"] = "Gagal menyimpan", ["Can't restore"] = "Tidak dapat memulihkan", ["Couldn't restore"] = "Gagal memulihkan",
            ["Can't read the file"] = "Tidak dapat membaca file", ["Nothing chosen"] = "Belum ada yang dipilih", ["Unknown file"] = "File tidak dikenal", ["Nothing to restore"] = "Tidak ada yang dipulihkan",

            // ---- about
            ["About EduTrack"] = "Tentang EduTrack", ["A desktop register for schools and training centres."] = "Register desktop untuk sekolah dan pusat pelatihan.",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "EduTrack menyimpan siswa, guru, staf, dan kelas di satu tempat. Catat siapa mereka dan dokumen apa yang Anda miliki, susun jadwal mingguan setiap kelas, masukkan nilai, dan simpan salinan XML data Anda.",
            ["Contact"] = "Kontak", ["Scan to visit the website"] = "Pindai untuk mengunjungi situs web",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "Arahkan kamera ponsel Anda ke sini untuk membuka adamrohal.tech. Ada pertanyaan atau ide? Tulis ke email di sebelah kiri.",
            ["Support the developer"] = "Dukung pengembang",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "EduTrack dibuat dan dirawat oleh satu orang. Jika menghemat waktu Anda, secangkir kopi di Ko-fi membantu menjaganya tetap gratis dan terus berkembang.",
            ["Support on Ko-fi"] = "Dukung di Ko-fi",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "Yakin ingin menghapus karyawan ini?",
            ["Are you sure you want to delete this student?"] = "Yakin ingin menghapus siswa ini?",
            ["Are you sure you want to delete this Teacher?"] = "Yakin ingin menghapus guru ini?",
            ["Employee has been deleted successfully."] = "Karyawan telah dihapus.", ["Student has been deleted successfully."] = "Siswa telah dihapus.", ["Teacher has been deleted successfully."] = "Guru telah dihapus.",
            ["Employee information updated successfully!"] = "Informasi karyawan diperbarui!", ["Student information updated successfully!"] = "Informasi siswa diperbarui!", ["Teacher information updated successfully!"] = "Informasi guru diperbarui!",
            ["Failed to update employee information!"] = "Gagal memperbarui informasi karyawan!", ["Failed to update student information!"] = "Gagal memperbarui informasi siswa!", ["Failed to update teacher information!"] = "Gagal memperbarui informasi guru!",
            ["Employee information saved successfully!"] = "Informasi karyawan disimpan!", ["Student information saved successfully!"] = "Informasi siswa disimpan!", ["Teacher information saved successfully!"] = "Informasi guru disimpan!",
            ["Failed to save employee information!"] = "Gagal menyimpan informasi karyawan!", ["Failed to save student information!"] = "Gagal menyimpan informasi siswa!", ["Failed to save teacher information!"] = "Gagal menyimpan informasi guru!",
            ["Please select a class."] = "Mohon pilih kelas.", ["Give the class a name."] = "Beri nama pada kelas.",
            ["Class saved successfully."] = "Kelas disimpan.", ["The class could not be saved."] = "Kelas tidak dapat disimpan.",
            ["The class was deleted."] = "Kelas telah dihapus.", ["Module added successfully."] = "Modul ditambahkan.", ["Grades saved successfully."] = "Nilai disimpan.",
            ["Choose a class and type the name of the module first."] = "Pilih kelas dan ketik nama modul terlebih dahulu.",
            ["Choose a module first."] = "Pilih modul terlebih dahulu.", ["Choose a class first."] = "Pilih kelas terlebih dahulu.", ["Choose who to add first."] = "Pilih siapa yang ditambahkan terlebih dahulu.",
            ["Choose a file first."] = "Pilih file terlebih dahulu.",
            ["The classes could not be loaded. Check that the database is running."] = "Kelas tidak dapat dimuat. Pastikan basis data berjalan.",
            ["The modules could not be loaded."] = "Modul tidak dapat dimuat.", ["The students could not be loaded."] = "Siswa tidak dapat dimuat.",
            ["The module could not be added."] = "Modul tidak dapat ditambahkan.", ["The grades could not be saved."] = "Nilai tidak dapat disimpan.",
            ["The schedule could not be loaded."] = "Jadwal tidak dapat dimuat.", ["The schedule could not be saved."] = "Jadwal tidak dapat disimpan.",
            ["The teachers of this class could not be loaded."] = "Guru kelas ini tidak dapat dimuat.", ["The students of this class could not be loaded."] = "Siswa kelas ini tidak dapat dimuat.",
            ["The list of people could not be loaded."] = "Daftar orang tidak dapat dimuat.", ["They could not be added to the class."] = "Mereka tidak dapat ditambahkan ke kelas.",
            ["That change could not be saved."] = "Perubahan itu tidak dapat disimpan.", ["The class could not be deleted."] = "Kelas tidak dapat dihapus.",
            ["The employee list could not be loaded. Check that the database is running."] = "Daftar karyawan tidak dapat dimuat. Pastikan basis data berjalan.",
            ["The student list could not be loaded. Check that the database is running."] = "Daftar siswa tidak dapat dimuat. Pastikan basis data berjalan.",
            ["The teacher list could not be loaded. Check that the database is running."] = "Daftar guru tidak dapat dimuat. Pastikan basis data berjalan.",
            ["The file could not be saved."] = "File tidak dapat disimpan.", ["That file isn't a readable EduTrack copy."] = "File itu bukan salinan EduTrack yang dapat dibaca.",
            ["Nothing was changed. The restore stopped because:"] = "Tidak ada yang diubah. Pemulihan berhenti karena:",
            ["This file could not be opened."] = "File ini tidak dapat dibuka.", ["This page could not be shown."] = "Halaman ini tidak dapat ditampilkan.",
            ["EduTrack hit a problem it could not recover from:"] = "EduTrack mengalami masalah yang tidak dapat dipulihkan:",
            ["An error occurred: "] = "Terjadi kesalahan: ",
            ["Can't load classes"] = "Tidak dapat memuat kelas", ["Can't load employees"] = "Tidak dapat memuat karyawan", ["Can't load modules"] = "Tidak dapat memuat modul",
            ["Can't load people"] = "Tidak dapat memuat orang", ["Can't load schedule"] = "Tidak dapat memuat jadwal", ["Can't load students"] = "Tidak dapat memuat siswa",
            ["Can't load teachers"] = "Tidak dapat memuat guru", ["Confirmation"] = "Konfirmasi", ["Couldn't add"] = "Gagal menambahkan", ["Couldn't delete"] = "Gagal menghapus",
            ["Error"] = "Kesalahan", ["Information"] = "Informasi", ["Module not added"] = "Modul tidak ditambahkan", ["Move student"] = "Pindahkan siswa", ["No class chosen"] = "Belum ada kelas dipilih",
            ["No module chosen"] = "Belum ada modul dipilih", ["Remove from class"] = "Keluarkan dari kelas",
        };

        private static readonly List<Pat> IdPats = new List<Pat>();

        private static string NounId(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "siswa";
                case "teacher": case "teachers": return "guru";
                case "employee": case "employees": return "karyawan";
                default: return english;
            }
        }

        private static void InitId()
        {
            Dictionaries[Indonesian] = Id;
            Patterns[Indonesian] = IdPats;
            P(IdPats, @"^(\d+) (student|teacher|employee)s?$", m => m.Groups[1].Value + " " + NounId(m.Groups[2].Value));
            P(IdPats, @"^(\d+) matches?$", m => m.Groups[1].Value + " hasil");
            P(IdPats, @"^Version (.*)$", m => "Versi " + m.Groups[1].Value);
            P(IdPats, @"^Save (students|teachers|employees)$", m => "Simpan " + NounId(m.Groups[1].Value));
            P(IdPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => "Pulihkan " + m.Groups[1].Value + " data ke dalam " + NounId(m.Groups[2].Value) + "? Data dengan ID yang sama akan diganti.");
            P(IdPats, @"^None of the columns in that file match the (.*) table\.$", m => "Tidak ada kolom dalam file itu yang cocok dengan tabel " + NounId(m.Groups[1].Value) + ".");
            P(IdPats, @"^The grade for (.*) isn't valid\. (.*)$", m => "Nilai untuk " + m.Groups[1].Value + " tidak valid. " + T(m.Groups[2].Value));
            P(IdPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => "Anda memiliki perubahan yang belum disimpan pada " + m.Groups[1].Value + ". Ganti kelas dan kehilangannya?");
            P(IdPats, @"^Schedule saved for (.*)\.$", m => "Jadwal disimpan untuk " + m.Groups[1].Value + ".");
            P(IdPats, @"^Teachers  \((\d+)\)$", m => "Guru  (" + m.Groups[1].Value + ")");
            P(IdPats, @"^Students  \((\d+) found\)$", m => "Siswa  (" + m.Groups[1].Value + " ditemukan)");
            P(IdPats, @"^Students  \((\d+)\)$", m => "Siswa  (" + m.Groups[1].Value + ")");
            P(IdPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => "Keluarkan " + m.Groups[1].Value + " dari " + m.Groups[2].Value + "? " + (m.Groups[3].Value == "student" ? "Data siswanya tetap disimpan." : "Data gurunya tetap disimpan."));
            P(IdPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "Hapus kelas «" + m.Groups[1].Value + "»? Jadwal, modul, dan nilainya juga akan dihapus. " + T(m.Groups[2].Value));
            P(IdPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "Tidak ada " + NounId(m.Groups[1].Value) + " yang cocok dengan «" + m.Groups[2].Value + "»");
            P(IdPats, @"^created (.*)$", m => "dibuat " + m.Groups[1].Value);
            P(IdPats, @"^ended (\d+) days? ago$", m => "berakhir " + m.Groups[1].Value + " hari lalu");
            P(IdPats, @"^in (\d+) days?$", m => m.Groups[1].Value + " hari lagi");
            P(IdPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + ", " + m.Groups[2].Value);
            P(IdPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(IdPats, @"^(Error [a-z ]+|Error): (.*)$", m => "Kesalahan: " + m.Groups[2].Value);
        }
    }
}
