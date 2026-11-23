using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> Japanese.</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Ja = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "ホーム", ["Students"] = "生徒", ["Teachers"] = "教師", ["Employees"] = "職員",
            ["Classes"] = "クラス", ["Settings"] = "設定", ["About"] = "情報",
            ["Home  (Ctrl+1)"] = "ホーム  (Ctrl+1)", ["Students  (Ctrl+2)"] = "生徒  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "教師  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "職員  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "クラス  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "設定  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "情報  (Ctrl+7)",
            ["Administrator"] = "管理者", ["Change theme"] = "テーマを変更", ["Sign out"] = "サインアウト",
            ["Minimize"] = "最小化", ["Maximize"] = "最大化", ["Restore"] = "元に戻す", ["Close"] = "閉じる",
            ["Student"] = "生徒", ["Teacher"] = "教師", ["Employee"] = "職員", ["Class"] = "クラス",
            ["OK"] = "OK", ["Yes"] = "はい", ["No"] = "いいえ", ["Cancel"] = "キャンセル", ["Delete"] = "削除", ["Keep it"] = "残す",
            ["Something went wrong"] = "問題が発生しました", ["Check this first"] = "まず確認してください", ["Please confirm"] = "確認してください",
            ["year"] = "年", ["years"] = "年", ["month"] = "か月", ["months"] = "か月",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - サインイン",
            ["One register for your whole school."] = "学校全体をひとつの名簿に。",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "生徒、教師、職員、クラスをまとめて管理し、すぐに見つけられます。",
            ["Sign in"] = "サインイン", ["Use your administrator account to open the register."] = "管理者アカウントで名簿を開きます。",
            ["Signed in last time"] = "前回サインインしたアカウント", ["Username"] = "ユーザー名", ["Password"] = "パスワード",
            ["Confirm password"] = "パスワードの確認", ["At least 6 characters."] = "6文字以上。",
            ["Not you? Use another account"] = "あなたではありませんか？別のアカウントを使う", ["First time here?"] = "初めてですか？", ["Create an account"] = "アカウントを作成",
            ["Connecting to the database"] = "データベースに接続しています", ["Starting the database"] = "データベースを起動しています",
            ["Getting things ready"] = "準備しています", ["Ready"] = "準備完了", ["Can't reach the database"] = "データベースに接続できません",
            ["Try again"] = "再試行", ["Open anyway"] = "このまま開く",
            ["No accounts exist yet. Create the first administrator to get started."] = "アカウントがまだありません。最初の管理者を作成してください。",
            ["Welcome back"] = "おかえりなさい", ["Enter your password to continue."] = "続けるにはパスワードを入力してください。",
            ["Enter your username."] = "ユーザー名を入力してください。", ["Enter your password."] = "パスワードを入力してください。",
            ["Use at least 6 characters for the password."] = "パスワードは6文字以上にしてください。",
            ["The two passwords don't match."] = "2つのパスワードが一致しません。",
            ["That username and password don't match. Check them and try again."] = "ユーザー名とパスワードが一致しません。確認してもう一度お試しください。",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "データベースに接続できません。MySQL が起動していることを確認して、もう一度お試しください。",
            ["EduTrack is already running."] = "EduTrack はすでに起動しています。",
            ["Unexpected error"] = "予期しないエラー",
            ["Open EduTrack"] = "EduTrack を開く", ["Exit EduTrack"] = "EduTrack を終了",

            // ---- home
            ["Good morning"] = "おはようございます", ["Good afternoon"] = "こんにちは", ["Good evening"] = "こんばんは",
            ["Here is what the register holds right now."] = "現在の名簿の内容です。",
            ["Welcome. Let's set up your school."] = "ようこそ。学校の設定を始めましょう。",
            ["New class"] = "新しいクラス", ["New teacher"] = "新しい教師", ["New student"] = "新しい生徒", ["New employee"] = "新しい職員",
            ["Your register is empty. Start here."] = "名簿はまだ空です。ここから始めましょう。",
            ["Set things up in this order and everything else falls into place."] = "この順番で設定すれば、あとは自然に整います。",
            ["Create a class"] = "クラスを作成", ["A class is the group students and teachers belong to."] = "クラスは生徒と教師が所属するグループです。",
            ["Add your teachers"] = "教師を追加", ["Contracts, modules and documents live on each teacher's page."] = "契約、科目、書類は各教師のページにあります。",
            ["Enrol your students"] = "生徒を登録", ["Pick a class for each student while you add them."] = "生徒を追加するときにクラスを選びます。",
            ["Recently added students"] = "最近追加された生徒", ["See all"] = "すべて表示",
            ["Students you add will be listed here."] = "追加した生徒がここに表示されます。",
            ["Class sizes"] = "クラスの人数", ["Classes you create will show how many students they hold."] = "作成したクラスの生徒数が表示されます。",
            ["Students by major"] = "専攻別の生徒数", ["Majors appear here once students have one."] = "生徒に専攻が設定されるとここに表示されます。",
            ["Contracts ending soon"] = "まもなく終了する契約", ["Teachers and employees, next 90 days"] = "教師と職員、今後90日間",
            ["No contract ends in the next 90 days."] = "今後90日間に終了する契約はありません。",
            ["Records to complete"] = "未完了の記録", ["Missing a photo or an ID card scan"] = "写真または身分証のスキャンがありません",
            ["Every record has a photo and both ID scans."] = "すべての記録に写真と身分証の両面スキャンがあります。",
            ["today"] = "今日",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "事務・サポート職員（契約・給与を含む）。",
            ["All employees"] = "すべての職員", ["Everyone enrolled, with their contact details and documents."] = "登録済みの全員の連絡先と書類。",
            ["All students"] = "すべての生徒", ["The teaching staff, their modules and contracts."] = "教職員、担当科目、契約。",
            ["All teachers"] = "すべての教師", ["All classes"] = "すべてのクラス",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "生徒のグループ：担当教師、授業の時間、評価方法。",
            ["Schedule"] = "時間割", ["Grades"] = "成績",
            ["Reload the list"] = "一覧を再読み込み", ["Open details"] = "詳細を開く",
            ["Joined"] = "入学日", ["Contract ends"] = "契約終了",
            ["Add the first employee"] = "最初の職員を追加", ["Add the first student"] = "最初の生徒を追加", ["Add the first teacher"] = "最初の教師を追加",
            ["Create the first class"] = "最初のクラスを作成", ["Choose a class"] = "クラスを選択",
            ["Pick a class above to see its teachers and students."] = "上でクラスを選ぶと、教師と生徒が表示されます。",
            ["No employees yet"] = "職員はまだいません", ["Employees you add will show up here, ready to search and open."] = "追加した職員がここに表示され、検索して開けます。",
            ["No students yet"] = "生徒はまだいません", ["Students you add will show up here, ready to search and open."] = "追加した生徒がここに表示され、検索して開けます。",
            ["No teachers yet"] = "教師はまだいません", ["Teachers you add will show up here, ready to search and open."] = "追加した教師がここに表示され、検索して開けます。",
            ["Check the spelling, or try just a first or last name."] = "綴りを確認するか、名または姓だけで試してください。",
            ["Add to this class"] = "このクラスに追加", ["A student"] = "生徒", ["A teacher"] = "教師", ["Add to class"] = "クラスに追加",
            ["Remove from this class"] = "このクラスから外す",
            ["No teacher has been assigned to this class yet."] = "このクラスにはまだ教師が割り当てられていません。",
            ["No student is in this class yet. Use the Add to class bar above."] = "このクラスにはまだ生徒がいません。上の「クラスに追加」バーを使ってください。",
            ["Delete class"] = "クラスを削除", ["Class details"] = "クラスの詳細",
            ["A class groups students, teachers, a weekly schedule and grades."] = "クラスは生徒、教師、週間時間割、成績をまとめます。",
            ["Class name *"] = "クラス名 *", ["Start date"] = "開始日", ["Description"] = "説明", ["Save class"] = "クラスを保存",
            ["Last class added"] = "最後に追加したクラス",
            ["No class has been added yet. The one you save will appear here."] = "クラスはまだ追加されていません。保存したクラスがここに表示されます。",
            ["The last class could not be loaded."] = "最後のクラスを読み込めませんでした。", ["no date"] = "日付なし",

            // ---- grades / schedule
            ["Module"] = "科目", ["Add a module to this class"] = "このクラスに科目を追加", ["Add module"] = "科目を追加",
            ["Grade"] = "成績", ["No grades to enter yet"] = "入力する成績はまだありません",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "上でクラスと科目を選ぶと、そのクラスの生徒が表示され、成績を入力できます。",
            ["Save grades"] = "成績を保存",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "セルをクリックして科目または教室を入力します。Tab キーで次の曜日に移動します。",
            ["Unsaved changes"] = "未保存の変更", ["Save schedule"] = "時間割を保存", ["Time"] = "時間",
            ["Monday"] = "月曜日", ["Tuesday"] = "火曜日", ["Wednesday"] = "水曜日", ["Thursday"] = "木曜日", ["Friday"] = "金曜日", ["Saturday"] = "土曜日", ["Sunday"] = "日曜日",
            ["No classes to schedule yet"] = "時間割を作成するクラスがまだありません", ["Create a class first, then plan its week here."] = "先にクラスを作成してから、ここで1週間の予定を立てます。",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "0〜20の数字、または ABS のような短い語を使ってください。",
            ["Check the grades"] = "成績を確認してください",

            // ---- forms / details
            ["Back to the list"] = "一覧に戻る", ["Back to employees"] = "職員に戻る", ["Back to students"] = "生徒に戻る", ["Back to teachers"] = "教師に戻る",
            ["Employee details"] = "職員の詳細", ["Student details"] = "生徒の詳細", ["Teacher details"] = "教師の詳細",
            ["Edit anything below, then choose Save changes."] = "下の内容を編集し、「変更を保存」を選んでください。",
            ["Choose photo"] = "写真を選択", ["JPG or PNG"] = "JPG または PNG",
            ["Personal information"] = "個人情報", ["Fields marked with * are required."] = "* の付いた項目は必須です。",
            ["First name *"] = "名 *", ["Middle name"] = "ミドルネーム", ["Last name *"] = "姓 *", ["First name"] = "名", ["Last name"] = "姓",
            ["Gender"] = "性別", ["Email"] = "メール", ["Phone"] = "電話", ["Date of birth"] = "生年月日", ["Nationality"] = "国籍",
            ["ID card (CIN)"] = "身分証 (CIN)", ["Address"] = "住所",
            ["Professional details"] = "職務情報", ["Organization"] = "所属", ["Role"] = "役職",
            ["Date of appointment"] = "任命日", ["Experience"] = "経験", ["Contract"] = "契約", ["Contract start"] = "契約開始",
            ["Contract end"] = "契約終了", ["Duration"] = "期間", ["Salary (MAD per month)"] = "給与（月額・MAD）",
            ["Education"] = "学歴", ["Highest degree"] = "最終学位", ["Graduation year"] = "卒業年", ["University"] = "大学",
            ["Specialization"] = "専門分野", ["Module taught"] = "担当科目",
            ["Documents"] = "書類", ["Scans or photos. Open any of them from here later."] = "スキャンまたは写真。あとからここで開けます。",
            ["ID card, front"] = "身分証（表）", ["ID card, back"] = "身分証（裏）", ["Open the file"] = "ファイルを開く",
            ["Resume"] = "履歴書", ["Diploma"] = "卒業証書", ["Choose file"] = "ファイルを選択", ["No file chosen"] = "ファイルが選択されていません",
            ["Delete employee"] = "職員を削除", ["Delete student"] = "生徒を削除", ["Delete teacher"] = "教師を削除",
            ["Save changes"] = "変更を保存", ["Save employee"] = "職員を保存", ["Save student"] = "生徒を保存", ["Save teacher"] = "教師を保存",
            ["Enrolment"] = "入学登録", ["Where the student comes from and which class they join."] = "生徒の出身と、所属するクラス。",
            ["School or university"] = "学校または大学", ["Major"] = "専攻", ["Joining date"] = "入学日", ["Previous studies."] = "これまでの学歴。",
            ["High school or university"] = "高校または大学", ["Emergency contact"] = "緊急連絡先",
            ["Who to call first, usually a parent or guardian."] = "最初に連絡する人。通常は保護者です。",
            ["Relation"] = "続柄", ["Emergency contact ID card, front"] = "緊急連絡先の身分証（表）",
            ["Emergency contact ID card, back"] = "緊急連絡先の身分証（裏）", ["Male"] = "男性", ["Female"] = "女性", ["Select"] = "選択",
            ["Search by first, middle or last name"] = "名・ミドルネーム・姓で検索",
            ["Choose who to add"] = "追加する人を選択", ["Anything worth knowing about this class"] = "このクラスについて知っておくべきこと",
            ["Calculated from the appointment date"] = "任命日から自動計算", ["Calculated"] = "自動計算", ["Choose a module"] = "科目を選択",
            ["Parent, guardian..."] = "親、保護者など", ["e.g. 2A Computer Science"] = "例：2A コンピュータ科学", ["e.g. Algebra"] = "例：代数", ["e.g. Computer Science"] = "例：コンピュータ科学",
            ["Search this class"] = "このクラスを検索", ["Every teacher is already in this class"] = "すべての教師がすでにこのクラスにいます",
            ["Every student is already in this class"] = "すべての生徒がすでにこのクラスにいます", ["No major"] = "専攻なし", ["started"] = "開始",

            // ---- document viewer
            ["Previous page"] = "前のページ", ["Next page"] = "次のページ", ["Zoom out  (-)"] = "縮小  (-)", ["Zoom in  (+)"] = "拡大  (+)",
            ["Fit to screen  (0 or double-click)"] = "画面に合わせる  (0 またはダブルクリック)", ["Close  (Esc)"] = "閉じる  (Esc)", ["Document"] = "書類",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "EduTrack の見た目と、データのコピーの保存方法。",
            ["Appearance"] = "外観", ["Backup and restore"] = "バックアップと復元", ["Theme"] = "テーマ", ["Language"] = "言語",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "クラシックは EduTrack 本来の外観です。ペーパーとスレートは緑のテーマです。選択は記憶されます。",
            ["Classic (original)"] = "クラシック（標準）", ["Paper (light green)"] = "ペーパー（明るい緑）", ["Slate (dark green)"] = "スレート（濃い緑）",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "アプリ全体の言語を選びます。選択は記憶されます。",
            ["Save a copy"] = "コピーを保存", ["Export one list to an XML file you can keep safe or move to another computer."] = "一覧を XML ファイルに書き出し、安全に保管したり別のコンピューターに移したりできます。",
            ["What to save"] = "保存する内容", ["Save to file"] = "ファイルに保存", ["Show in folder"] = "フォルダーで表示",
            ["Restore from a copy"] = "コピーから復元", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "上で保存したファイルを選びます。同じ ID の記録はファイルの内容に置き換えられます。",
            ["Restore data"] = "データを復元",
            ["Choose what to save first."] = "先に保存する内容を選んでください。", ["Choose a saved copy"] = "保存したコピーを選択",
            ["Choose a file to restore first."] = "先に復元するファイルを選んでください。", ["That file has no records to restore."] = "そのファイルには復元できる記録がありません。",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "EduTrack は「コピーを保存」ボタンで保存した生徒・教師・職員のみ復元できます。",
            ["Couldn't save"] = "保存できませんでした", ["Can't restore"] = "復元できません", ["Couldn't restore"] = "復元できませんでした",
            ["Can't read the file"] = "ファイルを読み取れません", ["Nothing chosen"] = "何も選択されていません", ["Unknown file"] = "不明なファイル", ["Nothing to restore"] = "復元するものがありません",

            // ---- about
            ["About EduTrack"] = "EduTrack について", ["A desktop register for schools and training centres."] = "学校や研修施設のためのデスクトップ名簿。",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "EduTrack は生徒、教師、職員、クラスを一か所で管理します。誰がいて、どの書類を保管しているかを記録し、クラスごとの週間時間割を作り、成績を入力し、データの XML コピーを保管できます。",
            ["Contact"] = "連絡先", ["Scan to visit the website"] = "スキャンしてウェブサイトへ",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "スマートフォンのカメラをここに向けると adamrohal.tech が開きます。質問やアイデアは左のメールアドレスまで。",
            ["Support the developer"] = "開発者を支援する",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "EduTrack は一人で開発・更新されています。お役に立てたら、Ko-fi でコーヒーを一杯支援していただけると、無料のまま改善を続けられます。",
            ["Support on Ko-fi"] = "Ko-fi で支援",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "この職員を削除してもよろしいですか？",
            ["Are you sure you want to delete this student?"] = "この生徒を削除してもよろしいですか？",
            ["Are you sure you want to delete this Teacher?"] = "この教師を削除してもよろしいですか？",
            ["Employee has been deleted successfully."] = "職員を削除しました。", ["Student has been deleted successfully."] = "生徒を削除しました。", ["Teacher has been deleted successfully."] = "教師を削除しました。",
            ["Employee information updated successfully!"] = "職員の情報を更新しました。", ["Student information updated successfully!"] = "生徒の情報を更新しました。", ["Teacher information updated successfully!"] = "教師の情報を更新しました。",
            ["Failed to update employee information!"] = "職員の情報を更新できませんでした。", ["Failed to update student information!"] = "生徒の情報を更新できませんでした。", ["Failed to update teacher information!"] = "教師の情報を更新できませんでした。",
            ["Employee information saved successfully!"] = "職員の情報を保存しました。", ["Student information saved successfully!"] = "生徒の情報を保存しました。", ["Teacher information saved successfully!"] = "教師の情報を保存しました。",
            ["Failed to save employee information!"] = "職員の情報を保存できませんでした。", ["Failed to save student information!"] = "生徒の情報を保存できませんでした。", ["Failed to save teacher information!"] = "教師の情報を保存できませんでした。",
            ["Please select a class."] = "クラスを選択してください。", ["Give the class a name."] = "クラスに名前を付けてください。",
            ["Class saved successfully."] = "クラスを保存しました。", ["The class could not be saved."] = "クラスを保存できませんでした。",
            ["The class was deleted."] = "クラスを削除しました。", ["Module added successfully."] = "科目を追加しました。", ["Grades saved successfully."] = "成績を保存しました。",
            ["Choose a class and type the name of the module first."] = "先にクラスを選び、科目名を入力してください。",
            ["Choose a module first."] = "先に科目を選んでください。", ["Choose a class first."] = "先にクラスを選んでください。", ["Choose who to add first."] = "先に追加する人を選んでください。",
            ["Choose a file first."] = "先にファイルを選んでください。",
            ["The classes could not be loaded. Check that the database is running."] = "クラスを読み込めませんでした。データベースが起動しているか確認してください。",
            ["The modules could not be loaded."] = "科目を読み込めませんでした。", ["The students could not be loaded."] = "生徒を読み込めませんでした。",
            ["The module could not be added."] = "科目を追加できませんでした。", ["The grades could not be saved."] = "成績を保存できませんでした。",
            ["The schedule could not be loaded."] = "時間割を読み込めませんでした。", ["The schedule could not be saved."] = "時間割を保存できませんでした。",
            ["The teachers of this class could not be loaded."] = "このクラスの教師を読み込めませんでした。", ["The students of this class could not be loaded."] = "このクラスの生徒を読み込めませんでした。",
            ["The list of people could not be loaded."] = "人の一覧を読み込めませんでした。", ["They could not be added to the class."] = "クラスに追加できませんでした。",
            ["That change could not be saved."] = "その変更を保存できませんでした。", ["The class could not be deleted."] = "クラスを削除できませんでした。",
            ["The employee list could not be loaded. Check that the database is running."] = "職員一覧を読み込めませんでした。データベースが起動しているか確認してください。",
            ["The student list could not be loaded. Check that the database is running."] = "生徒一覧を読み込めませんでした。データベースが起動しているか確認してください。",
            ["The teacher list could not be loaded. Check that the database is running."] = "教師一覧を読み込めませんでした。データベースが起動しているか確認してください。",
            ["The file could not be saved."] = "ファイルを保存できませんでした。", ["That file isn't a readable EduTrack copy."] = "そのファイルは読み取れる EduTrack のコピーではありません。",
            ["Nothing was changed. The restore stopped because:"] = "何も変更されていません。復元は次の理由で停止しました：",
            ["This file could not be opened."] = "このファイルを開けませんでした。", ["This page could not be shown."] = "このページを表示できませんでした。",
            ["EduTrack hit a problem it could not recover from:"] = "EduTrack で回復できない問題が発生しました：",
            ["An error occurred: "] = "エラーが発生しました：",
            ["Can't load classes"] = "クラスを読み込めません", ["Can't load employees"] = "職員を読み込めません", ["Can't load modules"] = "科目を読み込めません",
            ["Can't load people"] = "人の一覧を読み込めません", ["Can't load schedule"] = "時間割を読み込めません", ["Can't load students"] = "生徒を読み込めません",
            ["Can't load teachers"] = "教師を読み込めません", ["Confirmation"] = "確認", ["Couldn't add"] = "追加できませんでした", ["Couldn't delete"] = "削除できませんでした",
            ["Error"] = "エラー", ["Information"] = "情報", ["Module not added"] = "科目は追加されませんでした", ["Move student"] = "生徒を移動", ["No class chosen"] = "クラスが選択されていません",
            ["No module chosen"] = "科目が選択されていません", ["Remove from class"] = "クラスから外す",
        };

        private static readonly List<Pat> JaPats = new List<Pat>();

        private static string NounJa(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "生徒";
                case "teacher": case "teachers": return "教師";
                case "employee": case "employees": return "職員";
                default: return english;
            }
        }

        private static void InitJa()
        {
            Dictionaries[Japanese] = Ja;
            Patterns[Japanese] = JaPats;
            P(JaPats, @"^(\d+) (student|teacher|employee)s?$", m => m.Groups[1].Value + " 人の" + NounJa(m.Groups[2].Value));
            P(JaPats, @"^(\d+) matches?$", m => m.Groups[1].Value + " 件");
            P(JaPats, @"^Version (.*)$", m => "バージョン " + m.Groups[1].Value);
            P(JaPats, @"^Save (students|teachers|employees)$", m => NounJa(m.Groups[1].Value) + "を保存");
            P(JaPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => m.Groups[1].Value + " 件の記録を" + NounJa(m.Groups[2].Value) + "に復元しますか？同じ ID の記録は置き換えられます。");
            P(JaPats, @"^None of the columns in that file match the (.*) table\.$", m => "そのファイルの列は" + NounJa(m.Groups[1].Value) + "テーブルのどの列とも一致しません。");
            P(JaPats, @"^The grade for (.*) isn't valid\. (.*)$", m => m.Groups[1].Value + " の成績が正しくありません。" + T(m.Groups[2].Value));
            P(JaPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => m.Groups[1].Value + " に未保存の変更があります。クラスを切り替えて変更を破棄しますか？");
            P(JaPats, @"^Schedule saved for (.*)\.$", m => m.Groups[1].Value + " の時間割を保存しました。");
            P(JaPats, @"^Teachers  \((\d+)\)$", m => "教師  (" + m.Groups[1].Value + ")");
            P(JaPats, @"^Students  \((\d+) found\)$", m => "生徒  (" + m.Groups[1].Value + " 件見つかりました)");
            P(JaPats, @"^Students  \((\d+)\)$", m => "生徒  (" + m.Groups[1].Value + ")");
            P(JaPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => m.Groups[1].Value + " を " + m.Groups[2].Value + " から外しますか？" + (m.Groups[3].Value == "student" ? "生徒の記録は残ります。" : "教師の記録は残ります。"));
            P(JaPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "クラス「" + m.Groups[1].Value + "」を削除しますか？時間割、科目、成績も削除されます。" + T(m.Groups[2].Value));
            P(JaPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "「" + m.Groups[2].Value + "」に一致する" + NounJa(m.Groups[1].Value) + "はいません");
            P(JaPats, @"^created (.*)$", m => m.Groups[1].Value + " に作成");
            P(JaPats, @"^ended (\d+) days? ago$", m => m.Groups[1].Value + " 日前に終了");
            P(JaPats, @"^in (\d+) days?$", m => m.Groups[1].Value + " 日後");
            P(JaPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + "、" + m.Groups[2].Value + " さん");
            P(JaPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(JaPats, @"^(Error [a-z ]+|Error): (.*)$", m => "エラー：" + m.Groups[2].Value);
        }
    }
}
