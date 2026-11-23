using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> Chinese (Simplified).</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Zh = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "首页", ["Students"] = "学生", ["Teachers"] = "教师", ["Employees"] = "员工",
            ["Classes"] = "班级", ["Settings"] = "设置", ["About"] = "关于",
            ["Home  (Ctrl+1)"] = "首页  (Ctrl+1)", ["Students  (Ctrl+2)"] = "学生  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "教师  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "员工  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "班级  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "设置  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "关于  (Ctrl+7)",
            ["Administrator"] = "管理员", ["Change theme"] = "切换主题", ["Sign out"] = "退出登录",
            ["Minimize"] = "最小化", ["Maximize"] = "最大化", ["Restore"] = "还原", ["Close"] = "关闭",
            ["Student"] = "学生", ["Teacher"] = "教师", ["Employee"] = "员工", ["Class"] = "班级",
            ["OK"] = "确定", ["Yes"] = "是", ["No"] = "否", ["Cancel"] = "取消", ["Delete"] = "删除", ["Keep it"] = "保留",
            ["Something went wrong"] = "出错了", ["Check this first"] = "请先检查", ["Please confirm"] = "请确认",
            ["year"] = "年", ["years"] = "年", ["month"] = "个月", ["months"] = "个月",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - 登录",
            ["One register for your whole school."] = "全校共用一本登记册。",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "学生、教师、员工和班级集中管理，查找方便。",
            ["Sign in"] = "登录", ["Use your administrator account to open the register."] = "使用管理员账户打开登记册。",
            ["Signed in last time"] = "上次登录", ["Username"] = "用户名", ["Password"] = "密码",
            ["Confirm password"] = "确认密码", ["At least 6 characters."] = "至少 6 个字符。",
            ["Not you? Use another account"] = "不是您？使用其他账户", ["First time here?"] = "第一次使用？", ["Create an account"] = "创建账户",
            ["Connecting to the database"] = "正在连接数据库", ["Starting the database"] = "正在启动数据库",
            ["Getting things ready"] = "正在准备", ["Ready"] = "就绪", ["Can't reach the database"] = "无法连接数据库",
            ["Try again"] = "重试", ["Open anyway"] = "仍然打开",
            ["No accounts exist yet. Create the first administrator to get started."] = "还没有账户。请创建第一个管理员以开始使用。",
            ["Welcome back"] = "欢迎回来", ["Enter your password to continue."] = "输入密码以继续。",
            ["Enter your username."] = "请输入用户名。", ["Enter your password."] = "请输入密码。",
            ["Use at least 6 characters for the password."] = "密码至少需要 6 个字符。",
            ["The two passwords don't match."] = "两次输入的密码不一致。",
            ["That username and password don't match. Check them and try again."] = "用户名和密码不匹配。请检查后重试。",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "无法连接数据库。请确认 MySQL 正在运行，然后重试。",
            ["EduTrack is already running."] = "EduTrack 已在运行。",
            ["Unexpected error"] = "意外错误",
            ["Open EduTrack"] = "打开 EduTrack", ["Exit EduTrack"] = "退出 EduTrack",

            // ---- home
            ["Good morning"] = "早上好", ["Good afternoon"] = "下午好", ["Good evening"] = "晚上好",
            ["Here is what the register holds right now."] = "这是登记册目前的内容。",
            ["Welcome. Let's set up your school."] = "欢迎。让我们来设置您的学校。",
            ["New class"] = "新建班级", ["New teacher"] = "新建教师", ["New student"] = "新建学生", ["New employee"] = "新建员工",
            ["Your register is empty. Start here."] = "登记册还是空的。从这里开始。",
            ["Set things up in this order and everything else falls into place."] = "按此顺序设置，其余都会水到渠成。",
            ["Create a class"] = "创建班级", ["A class is the group students and teachers belong to."] = "班级是学生和教师所属的群组。",
            ["Add your teachers"] = "添加教师", ["Contracts, modules and documents live on each teacher's page."] = "合同、模块和文件都在每位教师的页面上。",
            ["Enrol your students"] = "登记学生", ["Pick a class for each student while you add them."] = "添加学生时为每位学生选择班级。",
            ["Recently added students"] = "最近添加的学生", ["See all"] = "查看全部",
            ["Students you add will be listed here."] = "您添加的学生将显示在这里。",
            ["Class sizes"] = "班级人数", ["Classes you create will show how many students they hold."] = "您创建的班级会显示学生人数。",
            ["Students by major"] = "按专业统计学生", ["Majors appear here once students have one."] = "学生有专业后会显示在这里。",
            ["Contracts ending soon"] = "即将到期的合同", ["Teachers and employees, next 90 days"] = "教师和员工，未来 90 天",
            ["No contract ends in the next 90 days."] = "未来 90 天内没有合同到期。",
            ["Records to complete"] = "待完善的记录", ["Missing a photo or an ID card scan"] = "缺少照片或身份证扫描件",
            ["Every record has a photo and both ID scans."] = "每条记录都有照片和两面身份证扫描件。",
            ["today"] = "今天",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "行政和后勤人员，含合同和薪资。",
            ["All employees"] = "全部员工", ["Everyone enrolled, with their contact details and documents."] = "所有已登记人员及其联系方式和文件。",
            ["All students"] = "全部学生", ["The teaching staff, their modules and contracts."] = "教学人员、其授课模块和合同。",
            ["All teachers"] = "全部教师", ["All classes"] = "全部班级",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "学生群组：由谁授课、何时上课以及如何评分。",
            ["Schedule"] = "课程表", ["Grades"] = "成绩",
            ["Reload the list"] = "重新加载列表", ["Open details"] = "打开详情",
            ["Joined"] = "入学日期", ["Contract ends"] = "合同到期",
            ["Add the first employee"] = "添加第一位员工", ["Add the first student"] = "添加第一位学生", ["Add the first teacher"] = "添加第一位教师",
            ["Create the first class"] = "创建第一个班级", ["Choose a class"] = "选择班级",
            ["Pick a class above to see its teachers and students."] = "在上方选择班级以查看其教师和学生。",
            ["No employees yet"] = "暂无员工", ["Employees you add will show up here, ready to search and open."] = "您添加的员工会显示在这里，可搜索和打开。",
            ["No students yet"] = "暂无学生", ["Students you add will show up here, ready to search and open."] = "您添加的学生会显示在这里，可搜索和打开。",
            ["No teachers yet"] = "暂无教师", ["Teachers you add will show up here, ready to search and open."] = "您添加的教师会显示在这里，可搜索和打开。",
            ["Check the spelling, or try just a first or last name."] = "请检查拼写，或只输入名或姓。",
            ["Add to this class"] = "添加到此班级", ["A student"] = "一名学生", ["A teacher"] = "一名教师", ["Add to class"] = "添加到班级",
            ["Remove from this class"] = "从此班级移除",
            ["No teacher has been assigned to this class yet."] = "此班级尚未分配教师。",
            ["No student is in this class yet. Use the Add to class bar above."] = "此班级还没有学生。请使用上方的“添加到班级”栏。",
            ["Delete class"] = "删除班级", ["Class details"] = "班级详情",
            ["A class groups students, teachers, a weekly schedule and grades."] = "班级包含学生、教师、每周课程表和成绩。",
            ["Class name *"] = "班级名称 *", ["Start date"] = "开始日期", ["Description"] = "说明", ["Save class"] = "保存班级",
            ["Last class added"] = "最近添加的班级",
            ["No class has been added yet. The one you save will appear here."] = "尚未添加班级。您保存的班级会显示在这里。",
            ["The last class could not be loaded."] = "无法加载最近的班级。", ["no date"] = "无日期",

            // ---- grades / schedule
            ["Module"] = "模块", ["Add a module to this class"] = "为此班级添加模块", ["Add module"] = "添加模块",
            ["Grade"] = "成绩", ["No grades to enter yet"] = "暂无可录入的成绩",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "在上方选择班级和模块。该班级的学生会显示在这里，可录入成绩。",
            ["Save grades"] = "保存成绩",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "点击单元格并输入科目或教室。按 Tab 键跳到下一天。",
            ["Unsaved changes"] = "未保存的更改", ["Save schedule"] = "保存课程表", ["Time"] = "时间",
            ["Monday"] = "星期一", ["Tuesday"] = "星期二", ["Wednesday"] = "星期三", ["Thursday"] = "星期四", ["Friday"] = "星期五", ["Saturday"] = "星期六", ["Sunday"] = "星期日",
            ["No classes to schedule yet"] = "暂无班级可排课", ["Create a class first, then plan its week here."] = "请先创建班级，然后在这里安排每周课程。",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "请输入 0 到 20 的数字，或简短词语，如 ABS。",
            ["Check the grades"] = "请检查成绩",

            // ---- forms / details
            ["Back to the list"] = "返回列表", ["Back to employees"] = "返回员工", ["Back to students"] = "返回学生", ["Back to teachers"] = "返回教师",
            ["Employee details"] = "员工详情", ["Student details"] = "学生详情", ["Teacher details"] = "教师详情",
            ["Edit anything below, then choose Save changes."] = "在下方修改任何内容，然后选择“保存更改”。",
            ["Choose photo"] = "选择照片", ["JPG or PNG"] = "JPG 或 PNG",
            ["Personal information"] = "个人信息", ["Fields marked with * are required."] = "标有 * 的字段为必填项。",
            ["First name *"] = "名 *", ["Middle name"] = "中间名", ["Last name *"] = "姓 *", ["First name"] = "名", ["Last name"] = "姓",
            ["Gender"] = "性别", ["Email"] = "电子邮箱", ["Phone"] = "电话", ["Date of birth"] = "出生日期", ["Nationality"] = "国籍",
            ["ID card (CIN)"] = "身份证 (CIN)", ["Address"] = "地址",
            ["Professional details"] = "职业信息", ["Organization"] = "单位", ["Role"] = "职务",
            ["Date of appointment"] = "任职日期", ["Experience"] = "工作经验", ["Contract"] = "合同", ["Contract start"] = "合同开始",
            ["Contract end"] = "合同结束", ["Duration"] = "期限", ["Salary (MAD per month)"] = "薪资（迪拉姆/月）",
            ["Education"] = "教育背景", ["Highest degree"] = "最高学历", ["Graduation year"] = "毕业年份", ["University"] = "大学",
            ["Specialization"] = "专业方向", ["Module taught"] = "授课模块",
            ["Documents"] = "文件", ["Scans or photos. Open any of them from here later."] = "扫描件或照片。以后可在此打开。",
            ["ID card, front"] = "身份证正面", ["ID card, back"] = "身份证背面", ["Open the file"] = "打开文件",
            ["Resume"] = "简历", ["Diploma"] = "毕业证书", ["Choose file"] = "选择文件", ["No file chosen"] = "未选择文件",
            ["Delete employee"] = "删除员工", ["Delete student"] = "删除学生", ["Delete teacher"] = "删除教师",
            ["Save changes"] = "保存更改", ["Save employee"] = "保存员工", ["Save student"] = "保存学生", ["Save teacher"] = "保存教师",
            ["Enrolment"] = "入学登记", ["Where the student comes from and which class they join."] = "学生来自哪里以及加入哪个班级。",
            ["School or university"] = "学校或大学", ["Major"] = "专业", ["Joining date"] = "入学日期", ["Previous studies."] = "以往学业。",
            ["High school or university"] = "高中或大学", ["Emergency contact"] = "紧急联系人",
            ["Who to call first, usually a parent or guardian."] = "首先联系谁，通常是父母或监护人。",
            ["Relation"] = "关系", ["Emergency contact ID card, front"] = "紧急联系人身份证正面",
            ["Emergency contact ID card, back"] = "紧急联系人身份证背面", ["Male"] = "男", ["Female"] = "女", ["Select"] = "请选择",
            ["Search by first, middle or last name"] = "按名、中间名或姓搜索",
            ["Choose who to add"] = "选择要添加的人员", ["Anything worth knowing about this class"] = "关于此班级值得了解的内容",
            ["Calculated from the appointment date"] = "根据任职日期计算", ["Calculated"] = "自动计算", ["Choose a module"] = "选择模块",
            ["Parent, guardian..."] = "父母、监护人...", ["e.g. 2A Computer Science"] = "例如：2A 计算机科学", ["e.g. Algebra"] = "例如：代数", ["e.g. Computer Science"] = "例如：计算机科学",
            ["Search this class"] = "在此班级中搜索", ["Every teacher is already in this class"] = "所有教师都已在此班级中",
            ["Every student is already in this class"] = "所有学生都已在此班级中", ["No major"] = "无专业", ["started"] = "开始于",

            // ---- document viewer
            ["Previous page"] = "上一页", ["Next page"] = "下一页", ["Zoom out  (-)"] = "缩小  (-)", ["Zoom in  (+)"] = "放大  (+)",
            ["Fit to screen  (0 or double-click)"] = "适应屏幕  (0 或双击)", ["Close  (Esc)"] = "关闭  (Esc)", ["Document"] = "文件",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "EduTrack 的外观，以及如何备份您的数据。",
            ["Appearance"] = "外观", ["Backup and restore"] = "备份与还原", ["Theme"] = "主题", ["Language"] = "语言",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "经典是 EduTrack 的原始外观。纸张和石板是绿色的备选主题。EduTrack 会记住您的选择。",
            ["Classic (original)"] = "经典（原始）", ["Paper (light green)"] = "纸张（浅绿）", ["Slate (dark green)"] = "石板（深绿）",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "选择整个应用的语言。EduTrack 会记住您的选择。",
            ["Save a copy"] = "保存副本", ["Export one list to an XML file you can keep safe or move to another computer."] = "将一个列表导出为 XML 文件，可妥善保存或转移到另一台电脑。",
            ["What to save"] = "保存内容", ["Save to file"] = "保存到文件", ["Show in folder"] = "在文件夹中显示",
            ["Restore from a copy"] = "从副本还原", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "选择上面保存的文件。ID 相同的记录将被文件中的记录替换。",
            ["Restore data"] = "还原数据",
            ["Choose what to save first."] = "请先选择要保存的内容。", ["Choose a saved copy"] = "选择已保存的副本",
            ["Choose a file to restore first."] = "请先选择要还原的文件。", ["That file has no records to restore."] = "该文件没有可还原的记录。",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "EduTrack 只能还原使用“保存副本”按钮保存的学生、教师或员工。",
            ["Couldn't save"] = "无法保存", ["Can't restore"] = "无法还原", ["Couldn't restore"] = "还原失败",
            ["Can't read the file"] = "无法读取文件", ["Nothing chosen"] = "未选择任何内容", ["Unknown file"] = "未知文件", ["Nothing to restore"] = "没有可还原的内容",

            // ---- about
            ["About EduTrack"] = "关于 EduTrack", ["A desktop register for schools and training centres."] = "适用于学校和培训中心的桌面登记册。",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "EduTrack 将学生、教师、员工和班级集中在一处。记录他们的身份和您持有的文件，制定每个班级的每周课程表，录入成绩，并保留数据的 XML 副本。",
            ["Contact"] = "联系方式", ["Scan to visit the website"] = "扫码访问网站",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "用手机摄像头对准此处即可打开 adamrohal.tech。有问题或想法？请发邮件至左侧邮箱。",
            ["Support the developer"] = "支持开发者",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "EduTrack 由一个人开发并持续更新。如果它为您节省了时间，请在 Ko-fi 上请他喝杯咖啡，帮助它保持免费并不断改进。",
            ["Support on Ko-fi"] = "在 Ko-fi 上支持",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "确定要删除这位员工吗？",
            ["Are you sure you want to delete this student?"] = "确定要删除这位学生吗？",
            ["Are you sure you want to delete this Teacher?"] = "确定要删除这位教师吗？",
            ["Employee has been deleted successfully."] = "员工已删除。", ["Student has been deleted successfully."] = "学生已删除。", ["Teacher has been deleted successfully."] = "教师已删除。",
            ["Employee information updated successfully!"] = "员工信息已更新！", ["Student information updated successfully!"] = "学生信息已更新！", ["Teacher information updated successfully!"] = "教师信息已更新！",
            ["Failed to update employee information!"] = "更新员工信息失败！", ["Failed to update student information!"] = "更新学生信息失败！", ["Failed to update teacher information!"] = "更新教师信息失败！",
            ["Employee information saved successfully!"] = "员工信息已保存！", ["Student information saved successfully!"] = "学生信息已保存！", ["Teacher information saved successfully!"] = "教师信息已保存！",
            ["Failed to save employee information!"] = "保存员工信息失败！", ["Failed to save student information!"] = "保存学生信息失败！", ["Failed to save teacher information!"] = "保存教师信息失败！",
            ["Please select a class."] = "请选择班级。", ["Give the class a name."] = "请为班级命名。",
            ["Class saved successfully."] = "班级已保存。", ["The class could not be saved."] = "无法保存班级。",
            ["The class was deleted."] = "班级已删除。", ["Module added successfully."] = "模块已添加。", ["Grades saved successfully."] = "成绩已保存。",
            ["Choose a class and type the name of the module first."] = "请先选择班级并输入模块名称。",
            ["Choose a module first."] = "请先选择模块。", ["Choose a class first."] = "请先选择班级。", ["Choose who to add first."] = "请先选择要添加的人员。",
            ["Choose a file first."] = "请先选择文件。",
            ["The classes could not be loaded. Check that the database is running."] = "无法加载班级。请确认数据库正在运行。",
            ["The modules could not be loaded."] = "无法加载模块。", ["The students could not be loaded."] = "无法加载学生。",
            ["The module could not be added."] = "无法添加模块。", ["The grades could not be saved."] = "无法保存成绩。",
            ["The schedule could not be loaded."] = "无法加载课程表。", ["The schedule could not be saved."] = "无法保存课程表。",
            ["The teachers of this class could not be loaded."] = "无法加载此班级的教师。", ["The students of this class could not be loaded."] = "无法加载此班级的学生。",
            ["The list of people could not be loaded."] = "无法加载人员列表。", ["They could not be added to the class."] = "无法将他们添加到班级。",
            ["That change could not be saved."] = "无法保存该更改。", ["The class could not be deleted."] = "无法删除班级。",
            ["The employee list could not be loaded. Check that the database is running."] = "无法加载员工列表。请确认数据库正在运行。",
            ["The student list could not be loaded. Check that the database is running."] = "无法加载学生列表。请确认数据库正在运行。",
            ["The teacher list could not be loaded. Check that the database is running."] = "无法加载教师列表。请确认数据库正在运行。",
            ["The file could not be saved."] = "无法保存文件。", ["That file isn't a readable EduTrack copy."] = "该文件不是可读取的 EduTrack 副本。",
            ["Nothing was changed. The restore stopped because:"] = "未做任何更改。还原已停止，原因：",
            ["This file could not be opened."] = "无法打开此文件。", ["This page could not be shown."] = "无法显示此页面。",
            ["EduTrack hit a problem it could not recover from:"] = "EduTrack 遇到无法恢复的问题：",
            ["An error occurred: "] = "发生错误：",
            ["Can't load classes"] = "无法加载班级", ["Can't load employees"] = "无法加载员工", ["Can't load modules"] = "无法加载模块",
            ["Can't load people"] = "无法加载人员", ["Can't load schedule"] = "无法加载课程表", ["Can't load students"] = "无法加载学生",
            ["Can't load teachers"] = "无法加载教师", ["Confirmation"] = "确认", ["Couldn't add"] = "无法添加", ["Couldn't delete"] = "无法删除",
            ["Error"] = "错误", ["Information"] = "信息", ["Module not added"] = "模块未添加", ["Move student"] = "转移学生", ["No class chosen"] = "未选择班级",
            ["No module chosen"] = "未选择模块", ["Remove from class"] = "从班级移除",
        };

        private static readonly List<Pat> ZhPats = new List<Pat>();

        private static string NounZh(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "学生";
                case "teacher": case "teachers": return "教师";
                case "employee": case "employees": return "员工";
                default: return english;
            }
        }

        private static void InitZh()
        {
            Dictionaries[Chinese] = Zh;
            Patterns[Chinese] = ZhPats;
            P(ZhPats, @"^(\d+) (student|teacher|employee)s?$", m => m.Groups[1].Value + " 名" + NounZh(m.Groups[2].Value));
            P(ZhPats, @"^(\d+) matches?$", m => m.Groups[1].Value + " 条结果");
            P(ZhPats, @"^Version (.*)$", m => "版本 " + m.Groups[1].Value);
            P(ZhPats, @"^Save (students|teachers|employees)$", m => "保存" + NounZh(m.Groups[1].Value));
            P(ZhPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => "将 " + m.Groups[1].Value + " 条记录还原到" + NounZh(m.Groups[2].Value) + "？ID 相同的记录将被替换。");
            P(ZhPats, @"^None of the columns in that file match the (.*) table\.$", m => "该文件中没有任何列与" + NounZh(m.Groups[1].Value) + "表匹配。");
            P(ZhPats, @"^The grade for (.*) isn't valid\. (.*)$", m => m.Groups[1].Value + " 的成绩无效。" + T(m.Groups[2].Value));
            P(ZhPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => m.Groups[1].Value + " 有未保存的更改。切换班级并丢弃这些更改吗？");
            P(ZhPats, @"^Schedule saved for (.*)\.$", m => "已保存 " + m.Groups[1].Value + " 的课程表。");
            P(ZhPats, @"^Teachers  \((\d+)\)$", m => "教师  (" + m.Groups[1].Value + ")");
            P(ZhPats, @"^Students  \((\d+) found\)$", m => "学生  (找到 " + m.Groups[1].Value + " 名)");
            P(ZhPats, @"^Students  \((\d+)\)$", m => "学生  (" + m.Groups[1].Value + ")");
            P(ZhPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => "将 " + m.Groups[1].Value + " 从 " + m.Groups[2].Value + " 中移除？" + (m.Groups[3].Value == "student" ? "其学生记录将保留。" : "其教师记录将保留。"));
            P(ZhPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "删除班级“" + m.Groups[1].Value + "”？其课程表、模块和成绩也将一并删除。" + T(m.Groups[2].Value));
            P(ZhPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "没有与“" + m.Groups[2].Value + "”匹配的" + NounZh(m.Groups[1].Value));
            P(ZhPats, @"^created (.*)$", m => "创建于 " + m.Groups[1].Value);
            P(ZhPats, @"^ended (\d+) days? ago$", m => m.Groups[1].Value + " 天前已到期");
            P(ZhPats, @"^in (\d+) days?$", m => m.Groups[1].Value + " 天后");
            P(ZhPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + "，" + m.Groups[2].Value);
            P(ZhPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(ZhPats, @"^(Error [a-z ]+|Error): (.*)$", m => "错误：" + m.Groups[2].Value);
        }
    }
}
