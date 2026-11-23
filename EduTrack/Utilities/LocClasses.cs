using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>Text of the class workspace (overview, people, attendance, grades, timetable) in every language.</summary>
    public static partial class Loc
    {
        // One row per sentence: English, then French, Spanish, Arabic, Indonesian, Chinese, Japanese, Korean.
        private static void A(string en, string fr, string es, string ar, string id, string zh, string ja, string ko)
        {
            Fr[en] = fr; Es[en] = es; Ar[en] = ar; Id[en] = id; Zh[en] = zh; Ja[en] = ja; Ko[en] = ko;
        }

        private static void InitClasses()
        {
            // ---- workspace
            A("Overview", "Aperçu", "Resumen", "نظرة عامة", "Ringkasan", "概览", "概要", "개요");
            A("People", "Personnes", "Personas", "الأشخاص", "Orang", "人员", "メンバー", "구성원");
            A("Attendance", "Présence", "Asistencia", "الحضور", "Kehadiran", "考勤", "出席", "출석");
            A("Timetable", "Emploi du temps", "Horario", "الجدول", "Jadwal", "课程表", "時間割", "시간표");
            A("Edit class", "Modifier la classe", "Editar clase", "تعديل القسم", "Ubah kelas", "编辑班级", "クラスを編集", "학급 편집");
            A("currently in", "actuellement dans", "actualmente en", "حالياً في", "saat ini di", "目前在", "現在のクラス：", "현재 학급:");
            A("Lesson", "Cours", "Clase", "حصة", "Pelajaran", "课程", "授業", "수업");

            // ---- overview
            A("Modules", "Modules", "Módulos", "المواد", "Modul", "模块", "科目", "과목");
            A("Attendance, last 30 days", "Présence, 30 derniers jours", "Asistencia, últimos 30 días", "الحضور، آخر 30 يوماً", "Kehadiran, 30 hari terakhir", "考勤，近 30 天", "出席率（直近30日）", "출석률(최근 30일)");
            A("Class average", "Moyenne de la classe", "Media de la clase", "معدل القسم", "Rata-rata kelas", "班级平均分", "クラス平均", "학급 평균");
            A("Today's lessons", "Cours du jour", "Clases de hoy", "حصص اليوم", "Pelajaran hari ini", "今日课程", "今日の授業", "오늘 수업");
            A("No lesson is planned for today.", "Aucun cours n'est prévu aujourd'hui.", "No hay clases planificadas para hoy.", "لا توجد حصص مبرمجة اليوم.", "Tidak ada pelajaran yang dijadwalkan hari ini.", "今天没有安排课程。", "今日の授業の予定はありません。", "오늘 예정된 수업이 없습니다.");
            A("Take attendance anyway", "Faire l'appel quand même", "Pasar lista de todos modos", "تسجيل الحضور على أي حال", "Catat kehadiran saja", "仍然点名", "それでも出欠をとる", "그래도 출석 체크");
            A("Take attendance", "Faire l'appel", "Pasar lista", "تسجيل الحضور", "Catat kehadiran", "点名", "出欠をとる", "출석 체크");
            A("Review attendance", "Revoir la présence", "Revisar asistencia", "مراجعة الحضور", "Tinjau kehadiran", "查看考勤", "出欠を確認", "출석 확인");
            A("Needs attention", "À surveiller", "Requiere atención", "يحتاج إلى متابعة", "Perlu perhatian", "需要关注", "要確認", "확인 필요");
            A("From the last 30 days", "Sur les 30 derniers jours", "De los últimos 30 días", "خلال آخر 30 يوماً", "Dari 30 hari terakhir", "来自近 30 天", "直近30日の状況", "최근 30일 기준");
            A("Nothing needs attention. Attendance and grades are up to date.", "Rien à signaler. Présence et notes sont à jour.", "Nada requiere atención. Asistencia y notas están al día.", "لا شيء يحتاج إلى متابعة. الحضور والنقط محدّثة.", "Tidak ada yang perlu diperhatikan. Kehadiran dan nilai sudah terbaru.", "没有需要关注的事项。考勤和成绩均已更新。", "確認事項はありません。出席と成績は最新です。", "확인할 내용이 없습니다. 출석과 성적이 최신 상태입니다.");
            A("Coefficient", "Coefficient", "Coeficiente", "المعامل", "Koefisien", "系数", "係数", "계수");
            A("Assessments", "Évaluations", "Evaluaciones", "التقييمات", "Penilaian", "考核", "評価", "평가");
            A("No modules yet", "Aucun module pour l'instant", "Aún no hay módulos", "لا توجد مواد بعد", "Belum ada modul", "暂无模块", "科目はまだありません", "아직 과목이 없습니다");
            A("Add modules in Grades", "Ajouter des modules dans Notes", "Añade módulos en Notas", "أضف المواد في النقط", "Tambahkan modul di Nilai", "在“成绩”中添加模块", "「成績」で科目を追加", "성적에서 과목 추가");
            A("No teacher", "Aucun enseignant", "Sin profesor", "بدون أستاذ", "Tanpa guru", "无教师", "教師なし", "교사 없음");
            A("Open report", "Ouvrir le rapport", "Abrir informe", "فتح التقرير", "Buka laporan", "打开报告", "レポートを開く", "보고서 열기");
            A("Enter scores", "Saisir les notes", "Introducir notas", "إدخال النقط", "Masukkan skor", "录入分数", "点数を入力", "점수 입력");
            A("The class could not be loaded.", "La classe n'a pas pu être chargée.", "No se pudo cargar la clase.", "تعذر تحميل القسم.", "Kelas tidak dapat dimuat.", "无法加载班级。", "クラスを読み込めませんでした。", "학급을 불러올 수 없습니다.");

            // ---- timetable
            A("Pick a lesson to change it, or add a new one. Attendance follows this timetable.", "Choisissez un cours pour le modifier, ou ajoutez-en un. La présence suit cet emploi du temps.", "Elige una clase para cambiarla o añade una nueva. La asistencia sigue este horario.", "اختر حصة لتعديلها أو أضف حصة جديدة. يتبع الحضور هذا الجدول.", "Pilih pelajaran untuk mengubahnya, atau tambahkan yang baru. Kehadiran mengikuti jadwal ini.", "选择课程进行修改，或添加新课程。考勤依据此课程表。", "授業を選んで変更するか、新しく追加します。出欠はこの時間割に従います。", "수업을 선택해 수정하거나 새로 추가하세요. 출석은 이 시간표를 따릅니다.");
            A("Add lesson", "Ajouter un cours", "Añadir clase", "إضافة حصة", "Tambah pelajaran", "添加课程", "授業を追加", "수업 추가");
            A("New lesson", "Nouveau cours", "Nueva clase", "حصة جديدة", "Pelajaran baru", "新课程", "新しい授業", "새 수업");
            A("Edit lesson", "Modifier le cours", "Editar clase", "تعديل الحصة", "Ubah pelajaran", "编辑课程", "授業を編集", "수업 편집");
            A("Day", "Jour", "Día", "اليوم", "Hari", "星期", "曜日", "요일");
            A("From", "De", "Desde", "من", "Dari", "从", "開始", "시작");
            A("To", "À", "Hasta", "إلى", "Sampai", "到", "終了", "종료");
            A("No module", "Aucun module", "Sin módulo", "بدون مادة", "Tanpa modul", "无模块", "科目なし", "과목 없음");
            A("Title (when there is no module)", "Titre (s'il n'y a pas de module)", "Título (si no hay módulo)", "العنوان (عند عدم وجود مادة)", "Judul (jika tanpa modul)", "标题（无模块时）", "タイトル（科目がない場合）", "제목(과목이 없을 때)");
            A("e.g. Project work", "ex. Travail de projet", "p. ej. Trabajo de proyecto", "مثال: عمل المشروع", "mis. Kerja proyek", "例如：项目实践", "例：プロジェクト作業", "예: 프로젝트 수업");
            A("Room", "Salle", "Aula", "القاعة", "Ruangan", "教室", "教室", "교실");
            A("e.g. A2", "ex. A2", "p. ej. A2", "مثال: A2", "mis. A2", "例如：A2", "例：A2", "예: A2");
            A("Delete lesson", "Supprimer le cours", "Eliminar clase", "حذف الحصة", "Hapus pelajaran", "删除课程", "授業を削除", "수업 삭제");
            A("Save lesson", "Enregistrer le cours", "Guardar clase", "حفظ الحصة", "Simpan pelajaran", "保存课程", "授業を保存", "수업 저장");
            A("No lesson planned yet", "Aucun cours planifié", "Aún no hay clases planificadas", "لا توجد حصص مبرمجة بعد", "Belum ada pelajaran terjadwal", "暂无课程安排", "授業の予定はまだありません", "아직 예정된 수업이 없습니다");
            A("Add the lessons of the week. Each one can be tied to a module, and its teacher follows.", "Ajoutez les cours de la semaine. Chacun peut être lié à un module, et son enseignant suit.", "Añade las clases de la semana. Cada una puede vincularse a un módulo y su profesor la acompaña.", "أضف حصص الأسبوع. يمكن ربط كل حصة بمادة وسيتبعها أستاذها.", "Tambahkan pelajaran minggu ini. Tiap pelajaran bisa dikaitkan dengan modul, dan gurunya mengikuti.", "添加本周课程。每节课可关联到模块，教师会自动带出。", "週の授業を追加します。各授業は科目に関連付けられ、担当教師も自動で付きます。", "이번 주 수업을 추가하세요. 각 수업은 과목과 연결할 수 있으며 담당 교사가 함께 표시됩니다.");
            A("Choose the start and end time.", "Choisissez l'heure de début et de fin.", "Elige la hora de inicio y de fin.", "اختر وقت البداية والنهاية.", "Pilih waktu mulai dan selesai.", "请选择开始和结束时间。", "開始と終了の時刻を選んでください。", "시작 및 종료 시간을 선택하세요.");
            A("The lesson must end after it starts.", "Le cours doit se terminer après son début.", "La clase debe terminar después de empezar.", "يجب أن تنتهي الحصة بعد بدايتها.", "Pelajaran harus berakhir setelah dimulai.", "课程必须在开始之后结束。", "授業は開始より後に終わる必要があります。", "수업은 시작 이후에 끝나야 합니다.");
            A("Choose a module or give the lesson a title.", "Choisissez un module ou donnez un titre au cours.", "Elige un módulo o ponle un título a la clase.", "اختر مادة أو أعطِ الحصة عنواناً.", "Pilih modul atau beri judul pelajaran.", "请选择模块或为课程命名。", "科目を選ぶか、授業にタイトルを付けてください。", "과목을 선택하거나 수업 제목을 입력하세요.");
            A("This overlaps another lesson on the same day:", "Ce cours chevauche un autre cours du même jour :", "Se solapa con otra clase del mismo día:", "تتداخل مع حصة أخرى في اليوم نفسه:", "Ini bertabrakan dengan pelajaran lain di hari yang sama:", "与同一天的另一节课时间重叠：", "同じ日の別の授業と重なっています：", "같은 날 다른 수업과 겹칩니다:");
            A("Delete this lesson from the timetable? Attendance already taken for it is kept.", "Supprimer ce cours de l'emploi du temps ? La présence déjà relevée est conservée.", "¿Eliminar esta clase del horario? La asistencia ya registrada se conserva.", "هل تريد حذف هذه الحصة من الجدول؟ يتم الاحتفاظ بالحضور المسجل لها.", "Hapus pelajaran ini dari jadwal? Kehadiran yang sudah dicatat tetap disimpan.", "从课程表中删除这节课？已记录的考勤会保留。", "この授業を時間割から削除しますか？記録済みの出欠は残ります。", "이 수업을 시간표에서 삭제하시겠습니까? 이미 기록된 출석은 유지됩니다.");
            A("Delete lesson", "Supprimer le cours", "Eliminar clase", "حذف الحصة", "Hapus pelajaran", "删除课程", "授業を削除", "수업 삭제");

            // ---- attendance
            A("Previous day", "Jour précédent", "Día anterior", "اليوم السابق", "Hari sebelumnya", "前一天", "前の日", "이전 날");
            A("Next day", "Jour suivant", "Día siguiente", "اليوم التالي", "Hari berikutnya", "后一天", "次の日", "다음 날");
            A("Today", "Aujourd'hui", "Hoy", "اليوم", "Hari ini", "今天", "今日", "오늘");
            A("Roll call", "Appel", "Pase de lista", "تسجيل الحضور", "Absensi", "点名", "出欠確認", "출석 체크");
            A("Report", "Rapport", "Informe", "التقرير", "Laporan", "报告", "レポート", "보고서");
            A("Extra lesson", "Cours supplémentaire", "Clase extra", "حصة إضافية", "Pelajaran tambahan", "加课", "追加の授業", "추가 수업");
            A("Title", "Titre", "Título", "العنوان", "Judul", "标题", "タイトル", "제목");
            A("e.g. Revision session", "ex. Séance de révision", "p. ej. Sesión de repaso", "مثال: حصة مراجعة", "mis. Sesi pengulangan", "例如：复习课", "例：復習の授業", "예: 복습 수업");
            A("Delete record", "Supprimer le relevé", "Eliminar registro", "حذف السجل", "Hapus catatan", "删除记录", "記録を削除", "기록 삭제");
            A("Mark all present", "Tout marquer présent", "Marcar todos presentes", "تحديد الجميع حاضرين", "Tandai semua hadir", "全部标记为出勤", "全員を出席にする", "모두 출석 처리");
            A("Save attendance", "Enregistrer la présence", "Guardar asistencia", "حفظ الحضور", "Simpan kehadiran", "保存考勤", "出欠を保存", "출석 저장");
            A("Present", "Présent", "Presente", "حاضر", "Hadir", "出勤", "出席", "출석");
            A("Absent", "Absent", "Ausente", "غائب", "Absen", "缺勤", "欠席", "결석");
            A("Late", "En retard", "Tarde", "متأخر", "Terlambat", "迟到", "遅刻", "지각");
            A("Excused", "Excusé", "Justificado", "معذور", "Izin", "请假", "公欠", "사유 결석");
            A("Present  (P)", "Présent  (P)", "Presente  (P)", "حاضر  (P)", "Hadir  (P)", "出勤  (P)", "出席  (P)", "출석  (P)");
            A("Absent  (A)", "Absent  (A)", "Ausente  (A)", "غائب  (A)", "Absen  (A)", "缺勤  (A)", "欠席  (A)", "결석  (A)");
            A("Late  (L)", "En retard  (L)", "Tarde  (L)", "متأخر  (L)", "Terlambat  (L)", "迟到  (L)", "遅刻  (L)", "지각  (L)");
            A("Excused  (E)", "Excusé  (E)", "Justificado  (E)", "معذور  (E)", "Izin  (E)", "请假  (E)", "公欠  (E)", "사유 결석  (E)");
            A("Note", "Remarque", "Nota", "ملاحظة", "Catatan", "备注", "メモ", "메모");
            A("No lesson on this day", "Aucun cours ce jour-là", "No hay clases este día", "لا توجد حصص في هذا اليوم", "Tidak ada pelajaran di hari ini", "当天没有课程", "この日は授業がありません", "이 날은 수업이 없습니다");
            A("Plan lessons in the Timetable, or add an extra lesson for this day.", "Planifiez les cours dans l'emploi du temps, ou ajoutez un cours supplémentaire pour ce jour.", "Planifica las clases en el Horario o añade una clase extra para este día.", "برمج الحصص في الجدول أو أضف حصة إضافية لهذا اليوم.", "Rencanakan pelajaran di Jadwal, atau tambahkan pelajaran tambahan untuk hari ini.", "请在课程表中安排课程，或为这一天添加加课。", "時間割で授業を計画するか、この日に追加の授業を加えてください。", "시간표에서 수업을 계획하거나 이 날에 추가 수업을 넣으세요.");
            A("Name", "Nom", "Nombre", "الاسم", "Nama", "姓名", "名前", "이름");
            A("Rate", "Taux", "Porcentaje", "النسبة", "Persentase", "出勤率", "出席率", "출석률");
            A("No attendance has been recorded in this period.", "Aucune présence n'a été enregistrée sur cette période.", "No se ha registrado asistencia en este periodo.", "لم يتم تسجيل أي حضور في هذه الفترة.", "Belum ada kehadiran yang tercatat pada periode ini.", "此期间没有考勤记录。", "この期間に記録された出欠はありません。", "이 기간에 기록된 출석이 없습니다.");
            A("Nobody to mark", "Personne à pointer", "Nadie a quien marcar", "لا أحد لتسجيل حضوره", "Tidak ada yang dicatat", "没有可点名的人员", "対象者がいません", "체크할 사람이 없습니다");
            A("Add students and teachers to this class first, in People.", "Ajoutez d'abord des étudiants et des enseignants à cette classe, dans Personnes.", "Añade primero estudiantes y profesores a esta clase, en Personas.", "أضف الطلاب والأساتذة إلى هذا القسم أولاً، في الأشخاص.", "Tambahkan siswa dan guru ke kelas ini dulu, di Orang.", "请先在“人员”中为此班级添加学生和教师。", "先に「メンバー」でこのクラスに生徒と教師を追加してください。", "먼저 구성원에서 이 학급에 학생과 교사를 추가하세요.");
            A("Changes not saved yet", "Modifications non enregistrées", "Cambios sin guardar", "تغييرات غير محفوظة", "Perubahan belum disimpan", "更改尚未保存", "変更は未保存です", "변경 사항이 저장되지 않았습니다");
            A("Saved", "Enregistré", "Guardado", "تم الحفظ", "Tersimpan", "已保存", "保存済み", "저장됨");
            A("Not saved yet. Everyone starts as present.", "Pas encore enregistré. Tout le monde est présent par défaut.", "Aún sin guardar. Todos empiezan como presentes.", "لم يُحفظ بعد. الجميع حاضرون افتراضياً.", "Belum disimpan. Semua dimulai sebagai hadir.", "尚未保存。默认所有人出勤。", "未保存です。全員が出席の状態から始まります。", "아직 저장되지 않았습니다. 모두 출석으로 시작합니다.");
            A("Attendance saved successfully.", "Présence enregistrée.", "Asistencia guardada.", "تم حفظ الحضور.", "Kehadiran disimpan.", "考勤已保存。", "出欠を保存しました。", "출석을 저장했습니다.");
            A("The attendance could not be saved.", "La présence n'a pas pu être enregistrée.", "No se pudo guardar la asistencia.", "تعذر حفظ الحضور.", "Kehadiran tidak dapat disimpan.", "无法保存考勤。", "出欠を保存できませんでした。", "출석을 저장할 수 없습니다.");
            A("You changed the attendance of this lesson. Save it before you leave?", "Vous avez modifié la présence de ce cours. L'enregistrer avant de partir ?", "Has cambiado la asistencia de esta clase. ¿Guardarla antes de salir?", "لقد غيّرت حضور هذه الحصة. هل تريد حفظه قبل المغادرة؟", "Anda mengubah kehadiran pelajaran ini. Simpan sebelum keluar?", "您修改了这节课的考勤。离开前保存吗？", "この授業の出欠を変更しました。離れる前に保存しますか？", "이 수업의 출석을 변경했습니다. 나가기 전에 저장하시겠습니까?");
            A("Delete the attendance record of this lesson? Everyone goes back to unmarked.", "Supprimer le relevé de présence de ce cours ? Tout le monde redevient non pointé.", "¿Eliminar el registro de asistencia de esta clase? Todos vuelven a quedar sin marcar.", "هل تريد حذف سجل حضور هذه الحصة؟ سيعود الجميع إلى غير محدد.", "Hapus catatan kehadiran pelajaran ini? Semua kembali tanpa tanda.", "删除这节课的考勤记录？所有人将恢复为未标记。", "この授業の出欠記録を削除しますか？全員が未記入に戻ります。", "이 수업의 출석 기록을 삭제하시겠습니까? 모두 미표시 상태로 돌아갑니다.");
            A("Unsaved changes", "Modifications non enregistrées", "Cambios sin guardar", "تغييرات غير محفوظة", "Perubahan belum disimpan", "未保存的更改", "未保存の変更", "저장하지 않은 변경 사항");
            A("Teacher", "Enseignant", "Profesor", "أستاذ", "Guru", "教师", "教師", "교사");

            // ---- grades
            A("Module name", "Nom du module", "Nombre del módulo", "اسم المادة", "Nama modul", "模块名称", "科目名", "과목 이름");
            A("New module", "Nouveau module", "Nuevo módulo", "مادة جديدة", "Modul baru", "新模块", "新しい科目", "새 과목");
            A("Edit module", "Modifier le module", "Editar módulo", "تعديل المادة", "Ubah modul", "编辑模块", "科目を編集", "과목 편집");
            A("Save module", "Enregistrer le module", "Guardar módulo", "حفظ المادة", "Simpan modul", "保存模块", "科目を保存", "과목 저장");
            A("Delete module", "Supprimer le module", "Eliminar módulo", "حذف المادة", "Hapus modul", "删除模块", "科目を削除", "과목 삭제");
            A("Module saved successfully.", "Module enregistré.", "Módulo guardado.", "تم حفظ المادة.", "Modul disimpan.", "模块已保存。", "科目を保存しました。", "과목을 저장했습니다.");
            A("Add module", "Ajouter un module", "Añadir módulo", "إضافة مادة", "Tambah modul", "添加模块", "科目を追加", "과목 추가");
            A("New assessment", "Nouvelle évaluation", "Nueva evaluación", "تقييم جديد", "Penilaian baru", "新考核", "新しい評価", "새 평가");
            A("Edit assessment", "Modifier l'évaluation", "Editar evaluación", "تعديل التقييم", "Ubah penilaian", "编辑考核", "評価を編集", "평가 편집");
            A("e.g. Midterm exam", "ex. Examen de mi-parcours", "p. ej. Examen parcial", "مثال: امتحان نصف الفصل", "mis. Ujian tengah semester", "例如：期中考试", "例：中間試験", "예: 중간고사");
            A("Type", "Type", "Tipo", "النوع", "Jenis", "类型", "種類", "유형");
            A("Weight", "Pondération", "Peso", "الوزن", "Bobot", "权重", "重み", "가중치");
            A("How much it counts in the module average", "Poids dans la moyenne du module", "Cuánto cuenta en la media del módulo", "مدى تأثيره في معدل المادة", "Seberapa besar pengaruhnya pada rata-rata modul", "在模块平均分中的占比", "科目平均への影響の大きさ", "과목 평균에 반영되는 비중");
            A("Out of", "Sur", "Sobre", "من", "Dari", "满分", "満点", "만점");
            A("Date", "Date", "Fecha", "التاريخ", "Tanggal", "日期", "日付", "날짜");
            A("Delete assessment", "Supprimer l'évaluation", "Eliminar evaluación", "حذف التقييم", "Hapus penilaian", "删除考核", "評価を削除", "평가 삭제");
            A("Save assessment", "Enregistrer l'évaluation", "Guardar evaluación", "حفظ التقييم", "Simpan penilaian", "保存考核", "評価を保存", "평가 저장");
            A("Add assessment", "Ajouter une évaluation", "Añadir evaluación", "إضافة تقييم", "Tambah penilaian", "添加考核", "評価を追加", "평가 추가");
            A("Summary", "Synthèse", "Resumen", "الخلاصة", "Ringkasan", "汇总", "まとめ", "요약");
            A("All modules", "Tous les modules", "Todos los módulos", "كل المواد", "Semua modul", "全部模块", "すべての科目", "모든 과목");
            A("You changed grades that are not saved. Save them before you leave?", "Vous avez modifié des notes non enregistrées. Les enregistrer avant de partir ?", "Has cambiado notas sin guardar. ¿Guardarlas antes de salir?", "لقد غيّرت نقطاً غير محفوظة. هل تريد حفظها قبل المغادرة؟", "Anda mengubah nilai yang belum disimpan. Simpan sebelum keluar?", "您修改了尚未保存的成绩。离开前保存吗？", "未保存の成績を変更しました。離れる前に保存しますか？", "저장하지 않은 성적을 변경했습니다. 나가기 전에 저장하시겠습니까?");
            A("Rank", "Rang", "Puesto", "الترتيب", "Peringkat", "名次", "順位", "순위");
            A("Average", "Moyenne", "Media", "المعدل", "Rata-rata", "平均分", "平均", "평균");
            A("Module average", "Moyenne du module", "Media del módulo", "معدل المادة", "Rata-rata modul", "模块平均分", "科目平均", "과목 평균");
            A("Result", "Résultat", "Resultado", "النتيجة", "Hasil", "结果", "結果", "결과");
            A("Passed", "Admis", "Aprobado", "ناجح", "Lulus", "及格", "合格", "통과");
            A("Below pass mark", "Sous la moyenne requise", "Por debajo del aprobado", "دون معدل النجاح", "Di bawah batas lulus", "未达及格线", "合格点未満", "합격선 미만");
            A("No score has been entered yet.", "Aucune note n'a encore été saisie.", "Aún no se ha introducido ninguna nota.", "لم يتم إدخال أي نقطة بعد.", "Belum ada skor yang dimasukkan.", "尚未录入任何分数。", "まだ点数が入力されていません。", "아직 입력된 점수가 없습니다.");
            A("Highest", "Plus haute", "Más alta", "الأعلى", "Tertinggi", "最高", "最高", "최고");
            A("Lowest", "Plus basse", "Más baja", "الأدنى", "Terendah", "最低", "最低", "최저");
            A("at or above 10", "à 10 ou plus", "con 10 o más", "بمعدل 10 أو أكثر", "dengan nilai 10 atau lebih", "达到或超过 10 分", "10点以上", "10점 이상");
            A("students graded", "étudiants notés", "estudiantes calificados", "طلاب لديهم نقط", "siswa dinilai", "名学生已评分", "人が採点済み", "명 채점됨");
            A("Type a score in a cell: a number, or ABS / EXC when the student was absent or excused.", "Saisissez une note dans une case : un nombre, ou ABS / EXC si l'étudiant était absent ou excusé.", "Escribe una nota en una celda: un número, o ABS / EXC si el estudiante estuvo ausente o justificado.", "اكتب نقطة في الخلية: رقماً، أو ABS / EXC عند غياب الطالب أو إعفائه.", "Ketik skor di sel: angka, atau ABS / EXC jika siswa absen atau izin.", "在单元格中输入分数：数字，或学生缺考/请假时输入 ABS / EXC。", "セルに点数を入力します：数字、または欠席・公欠の場合は ABS / EXC。", "셀에 점수를 입력하세요: 숫자, 또는 결석/사유 결석이면 ABS / EXC.");            A("Give the module a name.", "Donnez un nom au module.", "Ponle un nombre al módulo.", "أعطِ المادة اسماً.", "Beri nama pada modul.", "请为模块命名。", "科目に名前を付けてください。", "과목 이름을 입력하세요.");
            A("The coefficient must be a number above 0.", "Le coefficient doit être un nombre supérieur à 0.", "El coeficiente debe ser un número mayor que 0.", "يجب أن يكون المعامل رقماً أكبر من 0.", "Koefisien harus berupa angka di atas 0.", "系数必须是大于 0 的数字。", "係数は0より大きい数字にしてください。", "계수는 0보다 큰 숫자여야 합니다.");
            A("Give the assessment a title.", "Donnez un titre à l'évaluation.", "Ponle un título a la evaluación.", "أعطِ التقييم عنواناً.", "Beri judul pada penilaian.", "请为考核命名。", "評価にタイトルを付けてください。", "평가 제목을 입력하세요.");
            A("The weight must be a number above 0.", "La pondération doit être un nombre supérieur à 0.", "El peso debe ser un número mayor que 0.", "يجب أن يكون الوزن رقماً أكبر من 0.", "Bobot harus berupa angka di atas 0.", "权重必须是大于 0 的数字。", "重みは0より大きい数字にしてください。", "가중치는 0보다 큰 숫자여야 합니다.");
            A("The maximum score must be a number above 0.", "La note maximale doit être un nombre supérieur à 0.", "La nota máxima debe ser un número mayor que 0.", "يجب أن تكون النقطة القصوى رقماً أكبر من 0.", "Skor maksimum harus berupa angka di atas 0.", "满分必须是大于 0 的数字。", "満点は0より大きい数字にしてください。", "만점은 0보다 큰 숫자여야 합니다.");
            A("Exam", "Examen", "Examen", "امتحان", "Ujian", "考试", "試験", "시험");
            A("Quiz", "Quiz", "Prueba corta", "اختبار قصير", "Kuis", "小测验", "小テスト", "퀴즈");
            A("Homework", "Devoir", "Tarea", "واجب منزلي", "Pekerjaan rumah", "作业", "宿題", "숙제");
            A("Project", "Projet", "Proyecto", "مشروع", "Proyek", "项目", "プロジェクト", "프로젝트");
            A("Other", "Autre", "Otro", "أخرى", "Lainnya", "其他", "その他", "기타");        }

        /// <summary>Messages built from pieces (counts and names), per language.</summary>
        private static void InitClassPatterns()
        {
            P(FrPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "utilisez un nombre de 0 à " + m.Groups[1].Value + ", ou un mot court comme ABS.");
            P(EsPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "usa un número de 0 a " + m.Groups[1].Value + " o una palabra corta como ABS.");
            P(ArPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "استخدم رقماً من 0 إلى " + m.Groups[1].Value + " أو كلمة قصيرة مثل ABS.");
            P(IdPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "gunakan angka dari 0 sampai " + m.Groups[1].Value + ", atau kata singkat seperti ABS.");
            P(ZhPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "请输入 0 到 " + m.Groups[1].Value + " 之间的数字，或 ABS 之类的简短词。");
            P(JaPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "0〜" + m.Groups[1].Value + " の数字、または ABS のような短い語を使ってください。");
            P(KoPats, @"^use a number from 0 to ([\d.]+), or a short word such as ABS\.$", m => "0~" + m.Groups[1].Value + " 사이의 숫자 또는 ABS 같은 짧은 단어를 사용하세요.");
            P(FrPats, @"^(\d+) absences?$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " absence" : " absences"));
            P(FrPats, @"^(\d+) scores? missing$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " note manquante" : " notes manquantes"));
            P(FrPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "Supprimer le module « " + m.Groups[1].Value + " » ? Ses évaluations et ses notes seront aussi supprimées.");
            P(FrPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "Supprimer l'évaluation « " + m.Groups[1].Value + " » ? Toutes ses notes seront aussi supprimées.");

            P(EsPats, @"^(\d+) absences?$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " ausencia" : " ausencias"));
            P(EsPats, @"^(\d+) scores? missing$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " nota pendiente" : " notas pendientes"));
            P(EsPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "¿Eliminar el módulo «" + m.Groups[1].Value + "»? Sus evaluaciones y notas también se eliminan.");
            P(EsPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "¿Eliminar la evaluación «" + m.Groups[1].Value + "»? Todas sus notas también se eliminan.");

            P(ArPats, @"^(\d+) absences?$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " غياب" : " غيابات"));
            P(ArPats, @"^(\d+) scores? missing$", m => m.Groups[1].Value + " نقط ناقصة");
            P(ArPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "هل تريد حذف المادة «" + m.Groups[1].Value + "»؟ سيتم أيضاً حذف تقييماتها ونقطها.");
            P(ArPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "هل تريد حذف التقييم «" + m.Groups[1].Value + "»؟ سيتم أيضاً حذف جميع نقطه.");

            P(IdPats, @"^(\d+) absences?$", m => m.Groups[1].Value + " absen");
            P(IdPats, @"^(\d+) scores? missing$", m => m.Groups[1].Value + " skor belum diisi");
            P(IdPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "Hapus modul «" + m.Groups[1].Value + "»? Penilaian dan skornya juga akan dihapus.");
            P(IdPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "Hapus penilaian «" + m.Groups[1].Value + "»? Semua skornya juga akan dihapus.");

            P(ZhPats, @"^(\d+) absences?$", m => m.Groups[1].Value + " 次缺勤");
            P(ZhPats, @"^(\d+) scores? missing$", m => "缺少 " + m.Groups[1].Value + " 个分数");
            P(ZhPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "删除模块“" + m.Groups[1].Value + "”？其考核和分数也将一并删除。");
            P(ZhPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "删除考核“" + m.Groups[1].Value + "”？其所有分数也将一并删除。");

            P(JaPats, @"^(\d+) absences?$", m => m.Groups[1].Value + " 回の欠席");
            P(JaPats, @"^(\d+) scores? missing$", m => "未入力の点数が " + m.Groups[1].Value + " 件");
            P(JaPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "科目「" + m.Groups[1].Value + "」を削除しますか？評価と点数も削除されます。");
            P(JaPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "評価「" + m.Groups[1].Value + "」を削除しますか？すべての点数も削除されます。");

            P(KoPats, @"^(\d+) absences?$", m => m.Groups[1].Value + "회 결석");
            P(KoPats, @"^(\d+) scores? missing$", m => "미입력 점수 " + m.Groups[1].Value + "개");
            P(KoPats, @"^Delete the module “(.*)”\? Its assessments and scores are deleted too\.$", m => "'" + m.Groups[1].Value + "' 과목을 삭제하시겠습니까? 평가와 점수도 함께 삭제됩니다.");
            P(KoPats, @"^Delete the assessment “(.*)”\? All its scores are deleted too\.$", m => "'" + m.Groups[1].Value + "' 평가를 삭제하시겠습니까? 모든 점수도 함께 삭제됩니다.");
        }
    }
}
