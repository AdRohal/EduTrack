using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> Arabic (the layout is mirrored by Loc when this language is chosen).</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Ar = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "الرئيسية", ["Students"] = "الطلاب", ["Teachers"] = "الأساتذة", ["Employees"] = "الموظفون",
            ["Classes"] = "الأقسام", ["Settings"] = "الإعدادات", ["About"] = "حول",
            ["Home  (Ctrl+1)"] = "الرئيسية  (Ctrl+1)", ["Students  (Ctrl+2)"] = "الطلاب  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "الأساتذة  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "الموظفون  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "الأقسام  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "الإعدادات  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "حول  (Ctrl+7)",
            ["Administrator"] = "المسؤول", ["Change theme"] = "تغيير المظهر", ["Sign out"] = "تسجيل الخروج",
            ["Minimize"] = "تصغير", ["Maximize"] = "تكبير", ["Restore"] = "استعادة", ["Close"] = "إغلاق",
            ["Student"] = "طالب", ["Teacher"] = "أستاذ", ["Employee"] = "موظف", ["Class"] = "القسم",
            ["OK"] = "موافق", ["Yes"] = "نعم", ["No"] = "لا", ["Cancel"] = "إلغاء", ["Delete"] = "حذف", ["Keep it"] = "الإبقاء عليه",
            ["Something went wrong"] = "حدث خطأ ما", ["Check this first"] = "تحقق من هذا أولاً", ["Please confirm"] = "يرجى التأكيد",
            ["year"] = "سنة", ["years"] = "سنوات", ["month"] = "شهر", ["months"] = "أشهر",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - تسجيل الدخول",
            ["One register for your whole school."] = "سجل واحد لمدرستك بأكملها.",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "الطلاب والأساتذة والموظفون والأقسام في مكان واحد وسهلة الوصول.",
            ["Sign in"] = "تسجيل الدخول", ["Use your administrator account to open the register."] = "استخدم حساب المسؤول لفتح السجل.",
            ["Signed in last time"] = "سجّل الدخول في المرة الأخيرة", ["Username"] = "اسم المستخدم", ["Password"] = "كلمة المرور",
            ["Confirm password"] = "تأكيد كلمة المرور", ["At least 6 characters."] = "6 أحرف على الأقل.",
            ["Not you? Use another account"] = "لست أنت؟ استخدم حساباً آخر", ["First time here?"] = "أول مرة هنا؟", ["Create an account"] = "إنشاء حساب",
            ["Connecting to the database"] = "جارٍ الاتصال بقاعدة البيانات", ["Starting the database"] = "جارٍ تشغيل قاعدة البيانات",
            ["Getting things ready"] = "جارٍ التحضير", ["Ready"] = "جاهز", ["Can't reach the database"] = "تعذر الوصول إلى قاعدة البيانات",
            ["Try again"] = "إعادة المحاولة", ["Open anyway"] = "فتح على أي حال",
            ["No accounts exist yet. Create the first administrator to get started."] = "لا توجد حسابات بعد. أنشئ أول مسؤول للبدء.",
            ["Welcome back"] = "مرحباً بعودتك", ["Enter your password to continue."] = "أدخل كلمة المرور للمتابعة.",
            ["Enter your username."] = "أدخل اسم المستخدم.", ["Enter your password."] = "أدخل كلمة المرور.",
            ["Use at least 6 characters for the password."] = "استخدم 6 أحرف على الأقل لكلمة المرور.",
            ["The two passwords don't match."] = "كلمتا المرور غير متطابقتين.",
            ["That username and password don't match. Check them and try again."] = "اسم المستخدم وكلمة المرور غير متطابقين. تحقق منهما وحاول مرة أخرى.",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "تعذر الوصول إلى قاعدة البيانات. تأكد من تشغيل MySQL ثم حاول مرة أخرى.",
            ["EduTrack is already running."] = "EduTrack قيد التشغيل بالفعل.",
            ["Unexpected error"] = "خطأ غير متوقع",
            ["Open EduTrack"] = "فتح EduTrack", ["Exit EduTrack"] = "إغلاق EduTrack",

            // ---- home
            ["Good morning"] = "صباح الخير", ["Good afternoon"] = "نهارك سعيد", ["Good evening"] = "مساء الخير",
            ["Here is what the register holds right now."] = "هذا ما يحتويه السجل الآن.",
            ["Welcome. Let's set up your school."] = "مرحباً. لنبدأ بإعداد مدرستك.",
            ["New class"] = "قسم جديد", ["New teacher"] = "أستاذ جديد", ["New student"] = "طالب جديد", ["New employee"] = "موظف جديد",
            ["Your register is empty. Start here."] = "سجلك فارغ. ابدأ من هنا.",
            ["Set things up in this order and everything else falls into place."] = "أعدّ الأمور بهذا الترتيب وسيتضح كل شيء.",
            ["Create a class"] = "أنشئ قسماً", ["A class is the group students and teachers belong to."] = "القسم هو المجموعة التي ينتمي إليها الطلاب والأساتذة.",
            ["Add your teachers"] = "أضف أساتذتك", ["Contracts, modules and documents live on each teacher's page."] = "العقود والمواد والوثائق موجودة في صفحة كل أستاذ.",
            ["Enrol your students"] = "سجّل طلابك", ["Pick a class for each student while you add them."] = "اختر قسماً لكل طالب أثناء إضافته.",
            ["Recently added students"] = "الطلاب المضافون حديثاً", ["See all"] = "عرض الكل",
            ["Students you add will be listed here."] = "سيظهر هنا الطلاب الذين تضيفهم.",
            ["Class sizes"] = "عدد الطلاب في الأقسام", ["Classes you create will show how many students they hold."] = "ستعرض الأقسام التي تنشئها عدد طلابها.",
            ["Students by major"] = "الطلاب حسب التخصص", ["Majors appear here once students have one."] = "تظهر التخصصات هنا عندما يكون للطلاب تخصص.",
            ["Contracts ending soon"] = "عقود تنتهي قريباً", ["Teachers and employees, next 90 days"] = "الأساتذة والموظفون، خلال 90 يوماً القادمة",
            ["No contract ends in the next 90 days."] = "لا ينتهي أي عقد خلال 90 يوماً القادمة.",
            ["Records to complete"] = "سجلات تحتاج إلى استكمال", ["Missing a photo or an ID card scan"] = "صورة أو مسح لبطاقة الهوية مفقود",
            ["Every record has a photo and both ID scans."] = "لكل سجل صورة ومسحان لبطاقة الهوية.",
            ["today"] = "اليوم",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "الموظفون الإداريون وموظفو الدعم، مع العقود والرواتب.",
            ["All employees"] = "كل الموظفين", ["Everyone enrolled, with their contact details and documents."] = "جميع المسجلين مع بيانات الاتصال والوثائق.",
            ["All students"] = "كل الطلاب", ["The teaching staff, their modules and contracts."] = "الهيئة التدريسية ومواد تدريسها وعقودها.",
            ["All teachers"] = "كل الأساتذة", ["All classes"] = "كل الأقسام",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "مجموعات الطلاب: من يدرّسهم ومتى يلتقون وكيف يُقيَّمون.",
            ["Schedule"] = "الجدول", ["Grades"] = "النقط",
            ["Reload the list"] = "إعادة تحميل القائمة", ["Open details"] = "فتح التفاصيل",
            ["Joined"] = "تاريخ الالتحاق", ["Contract ends"] = "نهاية العقد",
            ["Add the first employee"] = "أضف أول موظف", ["Add the first student"] = "أضف أول طالب", ["Add the first teacher"] = "أضف أول أستاذ",
            ["Create the first class"] = "أنشئ أول قسم", ["Choose a class"] = "اختر قسماً",
            ["Pick a class above to see its teachers and students."] = "اختر قسماً أعلاه لعرض أساتذته وطلابه.",
            ["No employees yet"] = "لا يوجد موظفون بعد", ["Employees you add will show up here, ready to search and open."] = "سيظهر هنا الموظفون الذين تضيفهم، جاهزين للبحث والفتح.",
            ["No students yet"] = "لا يوجد طلاب بعد", ["Students you add will show up here, ready to search and open."] = "سيظهر هنا الطلاب الذين تضيفهم، جاهزين للبحث والفتح.",
            ["No teachers yet"] = "لا يوجد أساتذة بعد", ["Teachers you add will show up here, ready to search and open."] = "سيظهر هنا الأساتذة الذين تضيفهم، جاهزين للبحث والفتح.",
            ["Check the spelling, or try just a first or last name."] = "تحقق من الإملاء أو جرّب الاسم الأول أو اسم العائلة فقط.",
            ["Add to this class"] = "إضافة إلى هذا القسم", ["A student"] = "طالب", ["A teacher"] = "أستاذ", ["Add to class"] = "إضافة إلى القسم",
            ["Remove from this class"] = "إزالة من هذا القسم",
            ["No teacher has been assigned to this class yet."] = "لم يتم تعيين أي أستاذ لهذا القسم بعد.",
            ["No student is in this class yet. Use the Add to class bar above."] = "لا يوجد طلاب في هذا القسم بعد. استخدم شريط «إضافة إلى القسم» أعلاه.",
            ["Delete class"] = "حذف القسم", ["Class details"] = "تفاصيل القسم",
            ["A class groups students, teachers, a weekly schedule and grades."] = "يجمع القسم الطلاب والأساتذة وجدولاً أسبوعياً والنقط.",
            ["Class name *"] = "اسم القسم *", ["Start date"] = "تاريخ البدء", ["Description"] = "الوصف", ["Save class"] = "حفظ القسم",
            ["Last class added"] = "آخر قسم أُضيف",
            ["No class has been added yet. The one you save will appear here."] = "لم يُضف أي قسم بعد. القسم الذي تحفظه سيظهر هنا.",
            ["The last class could not be loaded."] = "تعذر تحميل آخر قسم.", ["no date"] = "بدون تاريخ",

            // ---- grades / schedule
            ["Module"] = "المادة", ["Add a module to this class"] = "أضف مادة إلى هذا القسم", ["Add module"] = "إضافة مادة",
            ["Grade"] = "النقطة", ["No grades to enter yet"] = "لا توجد نقط لإدخالها بعد",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "اختر قسماً ومادة أعلاه. سيظهر هنا طلاب ذلك القسم جاهزين لإدخال نقطهم.",
            ["Save grades"] = "حفظ النقط",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "انقر على خلية واكتب المادة أو القاعة. ينتقل Tab إلى اليوم التالي.",
            ["Unsaved changes"] = "تغييرات غير محفوظة", ["Save schedule"] = "حفظ الجدول", ["Time"] = "الوقت",
            ["Monday"] = "الاثنين", ["Tuesday"] = "الثلاثاء", ["Wednesday"] = "الأربعاء", ["Thursday"] = "الخميس", ["Friday"] = "الجمعة", ["Saturday"] = "السبت", ["Sunday"] = "الأحد",
            ["No classes to schedule yet"] = "لا توجد أقسام لجدولتها بعد", ["Create a class first, then plan its week here."] = "أنشئ قسماً أولاً ثم خطّط لأسبوعه هنا.",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "استخدم رقماً من 0 إلى 20 أو كلمة قصيرة مثل ABS.",
            ["Check the grades"] = "تحقق من النقط",

            // ---- forms / details
            ["Back to the list"] = "العودة إلى القائمة", ["Back to employees"] = "العودة إلى الموظفين", ["Back to students"] = "العودة إلى الطلاب", ["Back to teachers"] = "العودة إلى الأساتذة",
            ["Employee details"] = "تفاصيل الموظف", ["Student details"] = "تفاصيل الطالب", ["Teacher details"] = "تفاصيل الأستاذ",
            ["Edit anything below, then choose Save changes."] = "عدّل ما تريد أدناه ثم اختر حفظ التغييرات.",
            ["Choose photo"] = "اختيار صورة", ["JPG or PNG"] = "JPG أو PNG",
            ["Personal information"] = "المعلومات الشخصية", ["Fields marked with * are required."] = "الحقول المعلَّمة بـ * إلزامية.",
            ["First name *"] = "الاسم الأول *", ["Middle name"] = "الاسم الأوسط", ["Last name *"] = "اسم العائلة *", ["First name"] = "الاسم الأول", ["Last name"] = "اسم العائلة",
            ["Gender"] = "الجنس", ["Email"] = "البريد الإلكتروني", ["Phone"] = "الهاتف", ["Date of birth"] = "تاريخ الميلاد", ["Nationality"] = "الجنسية",
            ["ID card (CIN)"] = "بطاقة الهوية (CIN)", ["Address"] = "العنوان",
            ["Professional details"] = "المعلومات المهنية", ["Organization"] = "المؤسسة", ["Role"] = "الوظيفة",
            ["Date of appointment"] = "تاريخ التعيين", ["Experience"] = "الخبرة", ["Contract"] = "العقد", ["Contract start"] = "بداية العقد",
            ["Contract end"] = "نهاية العقد", ["Duration"] = "المدة", ["Salary (MAD per month)"] = "الراتب (درهم في الشهر)",
            ["Education"] = "التعليم", ["Highest degree"] = "أعلى شهادة", ["Graduation year"] = "سنة التخرج", ["University"] = "الجامعة",
            ["Specialization"] = "التخصص", ["Module taught"] = "المادة المدرَّسة",
            ["Documents"] = "الوثائق", ["Scans or photos. Open any of them from here later."] = "مسوحات أو صور. يمكنك فتحها من هنا لاحقاً.",
            ["ID card, front"] = "بطاقة الهوية، الوجه", ["ID card, back"] = "بطاقة الهوية، الظهر", ["Open the file"] = "فتح الملف",
            ["Resume"] = "السيرة الذاتية", ["Diploma"] = "الشهادة", ["Choose file"] = "اختيار ملف", ["No file chosen"] = "لم يتم اختيار ملف",
            ["Delete employee"] = "حذف الموظف", ["Delete student"] = "حذف الطالب", ["Delete teacher"] = "حذف الأستاذ",
            ["Save changes"] = "حفظ التغييرات", ["Save employee"] = "حفظ الموظف", ["Save student"] = "حفظ الطالب", ["Save teacher"] = "حفظ الأستاذ",
            ["Enrolment"] = "التسجيل", ["Where the student comes from and which class they join."] = "من أين جاء الطالب وإلى أي قسم ينضم.",
            ["School or university"] = "المدرسة أو الجامعة", ["Major"] = "التخصص", ["Joining date"] = "تاريخ الالتحاق", ["Previous studies."] = "الدراسات السابقة.",
            ["High school or university"] = "الثانوية أو الجامعة", ["Emergency contact"] = "جهة الاتصال في الطوارئ",
            ["Who to call first, usually a parent or guardian."] = "من يُتَّصل به أولاً، عادةً أحد الوالدين أو الوصي.",
            ["Relation"] = "صلة القرابة", ["Emergency contact ID card, front"] = "بطاقة هوية جهة الطوارئ، الوجه",
            ["Emergency contact ID card, back"] = "بطاقة هوية جهة الطوارئ، الظهر", ["Male"] = "ذكر", ["Female"] = "أنثى", ["Select"] = "اختر",
            ["Search by first, middle or last name"] = "ابحث بالاسم الأول أو الأوسط أو اسم العائلة",
            ["Choose who to add"] = "اختر من تريد إضافته", ["Anything worth knowing about this class"] = "أي شيء يستحق المعرفة عن هذا القسم",
            ["Calculated from the appointment date"] = "يُحسب من تاريخ التعيين", ["Calculated"] = "محسوب", ["Choose a module"] = "اختر مادة",
            ["Parent, guardian..."] = "أحد الوالدين، الوصي...", ["e.g. 2A Computer Science"] = "مثال: 2A معلوميات", ["e.g. Algebra"] = "مثال: الجبر", ["e.g. Computer Science"] = "مثال: معلوميات",
            ["Search this class"] = "ابحث في هذا القسم", ["Every teacher is already in this class"] = "جميع الأساتذة موجودون في هذا القسم بالفعل",
            ["Every student is already in this class"] = "جميع الطلاب موجودون في هذا القسم بالفعل", ["No major"] = "بدون تخصص", ["started"] = "بدأ في",

            // ---- document viewer
            ["Previous page"] = "الصفحة السابقة", ["Next page"] = "الصفحة التالية", ["Zoom out  (-)"] = "تصغير  (-)", ["Zoom in  (+)"] = "تكبير  (+)",
            ["Fit to screen  (0 or double-click)"] = "ملاءمة الشاشة  (0 أو نقر مزدوج)", ["Close  (Esc)"] = "إغلاق  (Esc)", ["Document"] = "وثيقة",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "مظهر EduTrack وكيفية الاحتفاظ بنسخة من بياناتك.",
            ["Appearance"] = "المظهر", ["Backup and restore"] = "النسخ الاحتياطي والاستعادة", ["Theme"] = "السمة", ["Language"] = "اللغة",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "الكلاسيكي هو المظهر الأصلي لـ EduTrack. الورقي والأردوازي بديلان بالأخضر. يتذكر EduTrack اختيارك.",
            ["Classic (original)"] = "كلاسيكي (الأصلي)", ["Paper (light green)"] = "ورقي (أخضر فاتح)", ["Slate (dark green)"] = "أردوازي (أخضر داكن)",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "اختر لغة التطبيق بأكمله. يتذكر EduTrack اختيارك.",
            ["Save a copy"] = "حفظ نسخة", ["Export one list to an XML file you can keep safe or move to another computer."] = "صدّر قائمة إلى ملف XML يمكنك حفظه بأمان أو نقله إلى حاسوب آخر.",
            ["What to save"] = "ما الذي سيُحفظ", ["Save to file"] = "حفظ في ملف", ["Show in folder"] = "إظهار في المجلد",
            ["Restore from a copy"] = "الاستعادة من نسخة", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "اختر ملفاً محفوظاً أعلاه. تُستبدل السجلات ذات المعرّف نفسه بالسجلات الموجودة في الملف.",
            ["Restore data"] = "استعادة البيانات",
            ["Choose what to save first."] = "اختر ما تريد حفظه أولاً.", ["Choose a saved copy"] = "اختر نسخة محفوظة",
            ["Choose a file to restore first."] = "اختر ملفاً للاستعادة أولاً.", ["That file has no records to restore."] = "لا توجد سجلات في هذا الملف لاستعادتها.",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "يمكن لـ EduTrack استعادة الطلاب أو الأساتذة أو الموظفين المحفوظين بزر «حفظ نسخة» فقط.",
            ["Couldn't save"] = "تعذر الحفظ", ["Can't restore"] = "تعذرت الاستعادة", ["Couldn't restore"] = "تعذرت الاستعادة",
            ["Can't read the file"] = "تعذرت قراءة الملف", ["Nothing chosen"] = "لم يتم اختيار شيء", ["Unknown file"] = "ملف غير معروف", ["Nothing to restore"] = "لا شيء للاستعادة",

            // ---- about
            ["About EduTrack"] = "حول EduTrack", ["A desktop register for schools and training centres."] = "سجل مكتبي للمدارس ومراكز التكوين.",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "يجمع EduTrack الطلاب والأساتذة والموظفين والأقسام في مكان واحد. سجّل من هم وما الوثائق التي لديك، وأنشئ الجدول الأسبوعي لكل قسم، وأدخل النقط، واحتفظ بنسخة XML من بياناتك.",
            ["Contact"] = "التواصل", ["Scan to visit the website"] = "امسح لزيارة الموقع",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "وجّه كاميرا هاتفك إلى هنا لفتح adamrohal.tech. هل لديك أسئلة أو أفكار؟ راسلنا على البريد الإلكتروني.",
            ["Support the developer"] = "ادعم المطوّر",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "يقوم شخص واحد ببناء EduTrack وتحديثه. إذا وفّر عليك وقتك، فإن فنجان قهوة عبر Ko-fi يساعد على إبقائه مجانياً وتطويره.",
            ["Support on Ko-fi"] = "ادعم عبر Ko-fi",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "هل أنت متأكد أنك تريد حذف هذا الموظف؟",
            ["Are you sure you want to delete this student?"] = "هل أنت متأكد أنك تريد حذف هذا الطالب؟",
            ["Are you sure you want to delete this Teacher?"] = "هل أنت متأكد أنك تريد حذف هذا الأستاذ؟",
            ["Employee has been deleted successfully."] = "تم حذف الموظف.", ["Student has been deleted successfully."] = "تم حذف الطالب.", ["Teacher has been deleted successfully."] = "تم حذف الأستاذ.",
            ["Employee information updated successfully!"] = "تم تحديث معلومات الموظف!", ["Student information updated successfully!"] = "تم تحديث معلومات الطالب!", ["Teacher information updated successfully!"] = "تم تحديث معلومات الأستاذ!",
            ["Failed to update employee information!"] = "تعذر تحديث معلومات الموظف!", ["Failed to update student information!"] = "تعذر تحديث معلومات الطالب!", ["Failed to update teacher information!"] = "تعذر تحديث معلومات الأستاذ!",
            ["Employee information saved successfully!"] = "تم حفظ معلومات الموظف!", ["Student information saved successfully!"] = "تم حفظ معلومات الطالب!", ["Teacher information saved successfully!"] = "تم حفظ معلومات الأستاذ!",
            ["Failed to save employee information!"] = "تعذر حفظ معلومات الموظف!", ["Failed to save student information!"] = "تعذر حفظ معلومات الطالب!", ["Failed to save teacher information!"] = "تعذر حفظ معلومات الأستاذ!",
            ["Please select a class."] = "يرجى اختيار قسم.", ["Give the class a name."] = "أعطِ القسم اسماً.",
            ["Class saved successfully."] = "تم حفظ القسم.", ["The class could not be saved."] = "تعذر حفظ القسم.",
            ["The class was deleted."] = "تم حذف القسم.", ["Module added successfully."] = "تمت إضافة المادة.", ["Grades saved successfully."] = "تم حفظ النقط.",
            ["Choose a class and type the name of the module first."] = "اختر قسماً واكتب اسم المادة أولاً.",
            ["Choose a module first."] = "اختر مادة أولاً.", ["Choose a class first."] = "اختر قسماً أولاً.", ["Choose who to add first."] = "اختر من تريد إضافته أولاً.",
            ["Choose a file first."] = "اختر ملفاً أولاً.",
            ["The classes could not be loaded. Check that the database is running."] = "تعذر تحميل الأقسام. تأكد من أن قاعدة البيانات تعمل.",
            ["The modules could not be loaded."] = "تعذر تحميل المواد.", ["The students could not be loaded."] = "تعذر تحميل الطلاب.",
            ["The module could not be added."] = "تعذرت إضافة المادة.", ["The grades could not be saved."] = "تعذر حفظ النقط.",
            ["The schedule could not be loaded."] = "تعذر تحميل الجدول.", ["The schedule could not be saved."] = "تعذر حفظ الجدول.",
            ["The teachers of this class could not be loaded."] = "تعذر تحميل أساتذة هذا القسم.", ["The students of this class could not be loaded."] = "تعذر تحميل طلاب هذا القسم.",
            ["The list of people could not be loaded."] = "تعذر تحميل قائمة الأشخاص.", ["They could not be added to the class."] = "تعذرت إضافتهم إلى القسم.",
            ["That change could not be saved."] = "تعذر حفظ هذا التغيير.", ["The class could not be deleted."] = "تعذر حذف القسم.",
            ["The employee list could not be loaded. Check that the database is running."] = "تعذر تحميل قائمة الموظفين. تأكد من أن قاعدة البيانات تعمل.",
            ["The student list could not be loaded. Check that the database is running."] = "تعذر تحميل قائمة الطلاب. تأكد من أن قاعدة البيانات تعمل.",
            ["The teacher list could not be loaded. Check that the database is running."] = "تعذر تحميل قائمة الأساتذة. تأكد من أن قاعدة البيانات تعمل.",
            ["The file could not be saved."] = "تعذر حفظ الملف.", ["That file isn't a readable EduTrack copy."] = "هذا الملف ليس نسخة EduTrack قابلة للقراءة.",
            ["Nothing was changed. The restore stopped because:"] = "لم يتم تغيير أي شيء. توقفت الاستعادة بسبب:",
            ["This file could not be opened."] = "تعذر فتح هذا الملف.", ["This page could not be shown."] = "تعذر عرض هذه الصفحة.",
            ["EduTrack hit a problem it could not recover from:"] = "واجه EduTrack مشكلة لم يستطع التعافي منها:",
            ["An error occurred: "] = "حدث خطأ: ",
            ["Can't load classes"] = "تعذر تحميل الأقسام", ["Can't load employees"] = "تعذر تحميل الموظفين", ["Can't load modules"] = "تعذر تحميل المواد",
            ["Can't load people"] = "تعذر تحميل الأشخاص", ["Can't load schedule"] = "تعذر تحميل الجدول", ["Can't load students"] = "تعذر تحميل الطلاب",
            ["Can't load teachers"] = "تعذر تحميل الأساتذة", ["Confirmation"] = "تأكيد", ["Couldn't add"] = "تعذرت الإضافة", ["Couldn't delete"] = "تعذر الحذف",
            ["Error"] = "خطأ", ["Information"] = "معلومة", ["Module not added"] = "لم تُضف المادة", ["Move student"] = "نقل الطالب", ["No class chosen"] = "لم يتم اختيار قسم",
            ["No module chosen"] = "لم يتم اختيار مادة", ["Remove from class"] = "إزالة من القسم",
        };

        private static readonly List<Pat> ArPats = new List<Pat>();

        private static string NounAr(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "الطلاب";
                case "teacher": case "teachers": return "الأساتذة";
                case "employee": case "employees": return "الموظفين";
                default: return english;
            }
        }

        private static void InitAr()
        {
            Dictionaries[Arabic] = Ar;
            Patterns[Arabic] = ArPats;
            P(ArPats, @"^(\d+) (student|teacher|employee)s?$", m => m.Groups[1].Value + " " + (m.Groups[2].Value == "student" ? "طالب" : m.Groups[2].Value == "teacher" ? "أستاذ" : "موظف"));
            P(ArPats, @"^(\d+) matches?$", m => m.Groups[1].Value + " نتيجة");
            P(ArPats, @"^Version (.*)$", m => "الإصدار " + m.Groups[1].Value);
            P(ArPats, @"^Save (students|teachers|employees)$", m => "حفظ " + NounAr(m.Groups[1].Value));
            P(ArPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => "استعادة " + m.Groups[1].Value + " سجلاً إلى " + NounAr(m.Groups[2].Value) + "؟ ستُستبدل السجلات ذات المعرّف نفسه.");
            P(ArPats, @"^None of the columns in that file match the (.*) table\.$", m => "لا يتطابق أي عمود في هذا الملف مع جدول " + NounAr(m.Groups[1].Value) + ".");
            P(ArPats, @"^The grade for (.*) isn't valid\. (.*)$", m => "نقطة " + m.Groups[1].Value + " غير صالحة. " + T(m.Groups[2].Value));
            P(ArPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => "لديك تغييرات غير محفوظة في " + m.Groups[1].Value + ". هل تريد تغيير القسم وفقدانها؟");
            P(ArPats, @"^Schedule saved for (.*)\.$", m => "تم حفظ الجدول لـ " + m.Groups[1].Value + ".");
            P(ArPats, @"^Teachers  \((\d+)\)$", m => "الأساتذة  (" + m.Groups[1].Value + ")");
            P(ArPats, @"^Students  \((\d+) found\)$", m => "الطلاب  (" + m.Groups[1].Value + " نتيجة)");
            P(ArPats, @"^Students  \((\d+)\)$", m => "الطلاب  (" + m.Groups[1].Value + ")");
            P(ArPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => "هل تريد إزالة " + m.Groups[1].Value + " من " + m.Groups[2].Value + "؟ " + (m.Groups[3].Value == "student" ? "يتم الاحتفاظ بسجله كطالب." : "يتم الاحتفاظ بسجله كأستاذ."));
            P(ArPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "هل تريد حذف القسم «" + m.Groups[1].Value + "»؟ سيتم أيضاً حذف جدوله ومواده ونقطه. " + T(m.Groups[2].Value));
            P(ArPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "لا يوجد " + (m.Groups[1].Value == "employee" ? "موظف" : m.Groups[1].Value == "student" ? "طالب" : "أستاذ") + " يطابق «" + m.Groups[2].Value + "»");
            P(ArPats, @"^created (.*)$", m => "أُنشئ في " + m.Groups[1].Value);
            P(ArPats, @"^ended (\d+) days? ago$", m => "انتهى منذ " + m.Groups[1].Value + (m.Groups[1].Value == "1" ? " يوم" : " يوماً"));
            P(ArPats, @"^in (\d+) days?$", m => "خلال " + m.Groups[1].Value + (m.Groups[1].Value == "1" ? " يوم" : " يوماً"));
            P(ArPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + "، " + m.Groups[2].Value);
            P(ArPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(ArPats, @"^(Error [a-z ]+|Error): (.*)$", m => "خطأ: " + m.Groups[2].Value);
        }
    }
}
