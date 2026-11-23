using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> Korean.</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Ko = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "홈", ["Students"] = "학생", ["Teachers"] = "교사", ["Employees"] = "직원",
            ["Classes"] = "학급", ["Settings"] = "설정", ["About"] = "정보",
            ["Home  (Ctrl+1)"] = "홈  (Ctrl+1)", ["Students  (Ctrl+2)"] = "학생  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "교사  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "직원  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "학급  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "설정  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "정보  (Ctrl+7)",
            ["Administrator"] = "관리자", ["Change theme"] = "테마 변경", ["Sign out"] = "로그아웃",
            ["Minimize"] = "최소화", ["Maximize"] = "최대화", ["Restore"] = "복원", ["Close"] = "닫기",
            ["Student"] = "학생", ["Teacher"] = "교사", ["Employee"] = "직원", ["Class"] = "학급",
            ["OK"] = "확인", ["Yes"] = "예", ["No"] = "아니요", ["Cancel"] = "취소", ["Delete"] = "삭제", ["Keep it"] = "유지",
            ["Something went wrong"] = "문제가 발생했습니다", ["Check this first"] = "먼저 확인해 주세요", ["Please confirm"] = "확인해 주세요",
            ["year"] = "년", ["years"] = "년", ["month"] = "개월", ["months"] = "개월",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - 로그인",
            ["One register for your whole school."] = "학교 전체를 하나의 명부로.",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "학생, 교사, 직원, 학급을 한곳에 모아 쉽게 찾을 수 있습니다.",
            ["Sign in"] = "로그인", ["Use your administrator account to open the register."] = "관리자 계정으로 명부를 엽니다.",
            ["Signed in last time"] = "지난번에 로그인한 계정", ["Username"] = "사용자 이름", ["Password"] = "비밀번호",
            ["Confirm password"] = "비밀번호 확인", ["At least 6 characters."] = "6자 이상.",
            ["Not you? Use another account"] = "본인이 아닌가요? 다른 계정 사용", ["First time here?"] = "처음이신가요?", ["Create an account"] = "계정 만들기",
            ["Connecting to the database"] = "데이터베이스에 연결하는 중", ["Starting the database"] = "데이터베이스를 시작하는 중",
            ["Getting things ready"] = "준비하는 중", ["Ready"] = "준비 완료", ["Can't reach the database"] = "데이터베이스에 연결할 수 없습니다",
            ["Try again"] = "다시 시도", ["Open anyway"] = "그래도 열기",
            ["No accounts exist yet. Create the first administrator to get started."] = "아직 계정이 없습니다. 첫 번째 관리자를 만들어 시작하세요.",
            ["Welcome back"] = "다시 오신 것을 환영합니다", ["Enter your password to continue."] = "계속하려면 비밀번호를 입력하세요.",
            ["Enter your username."] = "사용자 이름을 입력하세요.", ["Enter your password."] = "비밀번호를 입력하세요.",
            ["Use at least 6 characters for the password."] = "비밀번호는 6자 이상이어야 합니다.",
            ["The two passwords don't match."] = "두 비밀번호가 일치하지 않습니다.",
            ["That username and password don't match. Check them and try again."] = "사용자 이름과 비밀번호가 일치하지 않습니다. 확인 후 다시 시도하세요.",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "데이터베이스에 연결할 수 없습니다. MySQL이 실행 중인지 확인한 후 다시 시도하세요.",
            ["EduTrack is already running."] = "EduTrack이 이미 실행 중입니다.",
            ["Unexpected error"] = "예기치 않은 오류",
            ["Open EduTrack"] = "EduTrack 열기", ["Exit EduTrack"] = "EduTrack 종료",

            // ---- home
            ["Good morning"] = "좋은 아침입니다", ["Good afternoon"] = "안녕하세요", ["Good evening"] = "좋은 저녁입니다",
            ["Here is what the register holds right now."] = "현재 명부에 담긴 내용입니다.",
            ["Welcome. Let's set up your school."] = "환영합니다. 학교를 설정해 봅시다.",
            ["New class"] = "새 학급", ["New teacher"] = "새 교사", ["New student"] = "새 학생", ["New employee"] = "새 직원",
            ["Your register is empty. Start here."] = "명부가 비어 있습니다. 여기서 시작하세요.",
            ["Set things up in this order and everything else falls into place."] = "이 순서대로 설정하면 나머지는 자연스럽게 정리됩니다.",
            ["Create a class"] = "학급 만들기", ["A class is the group students and teachers belong to."] = "학급은 학생과 교사가 속하는 그룹입니다.",
            ["Add your teachers"] = "교사 추가", ["Contracts, modules and documents live on each teacher's page."] = "계약, 과목, 문서는 각 교사의 페이지에 있습니다.",
            ["Enrol your students"] = "학생 등록", ["Pick a class for each student while you add them."] = "학생을 추가할 때 학급을 선택하세요.",
            ["Recently added students"] = "최근 추가된 학생", ["See all"] = "모두 보기",
            ["Students you add will be listed here."] = "추가한 학생이 여기에 표시됩니다.",
            ["Class sizes"] = "학급별 인원", ["Classes you create will show how many students they hold."] = "만든 학급의 학생 수가 표시됩니다.",
            ["Students by major"] = "전공별 학생", ["Majors appear here once students have one."] = "학생에게 전공이 생기면 여기에 표시됩니다.",
            ["Contracts ending soon"] = "곧 종료되는 계약", ["Teachers and employees, next 90 days"] = "교사와 직원, 향후 90일",
            ["No contract ends in the next 90 days."] = "향후 90일 내에 종료되는 계약이 없습니다.",
            ["Records to complete"] = "완성할 기록", ["Missing a photo or an ID card scan"] = "사진 또는 신분증 스캔 누락",
            ["Every record has a photo and both ID scans."] = "모든 기록에 사진과 신분증 양면 스캔이 있습니다.",
            ["today"] = "오늘",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "행정 및 지원 직원, 계약 및 급여 포함.",
            ["All employees"] = "모든 직원", ["Everyone enrolled, with their contact details and documents."] = "등록된 모든 사람의 연락처와 문서.",
            ["All students"] = "모든 학생", ["The teaching staff, their modules and contracts."] = "교직원, 담당 과목 및 계약.",
            ["All teachers"] = "모든 교사", ["All classes"] = "모든 학급",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "학생 그룹: 담당 교사, 수업 시간, 평가 방식.",
            ["Schedule"] = "시간표", ["Grades"] = "성적",
            ["Reload the list"] = "목록 새로 고침", ["Open details"] = "자세히 보기",
            ["Joined"] = "입학일", ["Contract ends"] = "계약 종료",
            ["Add the first employee"] = "첫 직원 추가", ["Add the first student"] = "첫 학생 추가", ["Add the first teacher"] = "첫 교사 추가",
            ["Create the first class"] = "첫 학급 만들기", ["Choose a class"] = "학급 선택",
            ["Pick a class above to see its teachers and students."] = "위에서 학급을 선택하면 교사와 학생이 표시됩니다.",
            ["No employees yet"] = "아직 직원이 없습니다", ["Employees you add will show up here, ready to search and open."] = "추가한 직원이 여기에 표시되며 검색하고 열 수 있습니다.",
            ["No students yet"] = "아직 학생이 없습니다", ["Students you add will show up here, ready to search and open."] = "추가한 학생이 여기에 표시되며 검색하고 열 수 있습니다.",
            ["No teachers yet"] = "아직 교사가 없습니다", ["Teachers you add will show up here, ready to search and open."] = "추가한 교사가 여기에 표시되며 검색하고 열 수 있습니다.",
            ["Check the spelling, or try just a first or last name."] = "철자를 확인하거나 이름 또는 성만 입력해 보세요.",
            ["Add to this class"] = "이 학급에 추가", ["A student"] = "학생", ["A teacher"] = "교사", ["Add to class"] = "학급에 추가",
            ["Remove from this class"] = "이 학급에서 제외",
            ["No teacher has been assigned to this class yet."] = "이 학급에 배정된 교사가 아직 없습니다.",
            ["No student is in this class yet. Use the Add to class bar above."] = "이 학급에 학생이 아직 없습니다. 위의 '학급에 추가' 막대를 사용하세요.",
            ["Delete class"] = "학급 삭제", ["Class details"] = "학급 상세",
            ["A class groups students, teachers, a weekly schedule and grades."] = "학급은 학생, 교사, 주간 시간표, 성적을 묶습니다.",
            ["Class name *"] = "학급 이름 *", ["Start date"] = "시작일", ["Description"] = "설명", ["Save class"] = "학급 저장",
            ["Last class added"] = "마지막으로 추가한 학급",
            ["No class has been added yet. The one you save will appear here."] = "아직 추가된 학급이 없습니다. 저장한 학급이 여기에 표시됩니다.",
            ["The last class could not be loaded."] = "마지막 학급을 불러올 수 없습니다.", ["no date"] = "날짜 없음",

            // ---- grades / schedule
            ["Module"] = "과목", ["Add a module to this class"] = "이 학급에 과목 추가", ["Add module"] = "과목 추가",
            ["Grade"] = "성적", ["No grades to enter yet"] = "아직 입력할 성적이 없습니다",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "위에서 학급과 과목을 선택하세요. 해당 학급의 학생이 표시되어 성적을 입력할 수 있습니다.",
            ["Save grades"] = "성적 저장",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "셀을 클릭하고 과목이나 교실을 입력하세요. Tab 키로 다음 요일로 이동합니다.",
            ["Unsaved changes"] = "저장하지 않은 변경 사항", ["Save schedule"] = "시간표 저장", ["Time"] = "시간",
            ["Monday"] = "월요일", ["Tuesday"] = "화요일", ["Wednesday"] = "수요일", ["Thursday"] = "목요일", ["Friday"] = "금요일", ["Saturday"] = "토요일", ["Sunday"] = "일요일",
            ["No classes to schedule yet"] = "시간표를 짤 학급이 아직 없습니다", ["Create a class first, then plan its week here."] = "먼저 학급을 만든 다음 여기에서 한 주의 계획을 세우세요.",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "0~20 사이의 숫자 또는 ABS 같은 짧은 단어를 사용하세요.",
            ["Check the grades"] = "성적을 확인하세요",

            // ---- forms / details
            ["Back to the list"] = "목록으로 돌아가기", ["Back to employees"] = "직원으로 돌아가기", ["Back to students"] = "학생으로 돌아가기", ["Back to teachers"] = "교사로 돌아가기",
            ["Employee details"] = "직원 상세", ["Student details"] = "학생 상세", ["Teacher details"] = "교사 상세",
            ["Edit anything below, then choose Save changes."] = "아래 내용을 수정한 후 '변경 사항 저장'을 선택하세요.",
            ["Choose photo"] = "사진 선택", ["JPG or PNG"] = "JPG 또는 PNG",
            ["Personal information"] = "개인 정보", ["Fields marked with * are required."] = "* 표시 항목은 필수입니다.",
            ["First name *"] = "이름 *", ["Middle name"] = "중간 이름", ["Last name *"] = "성 *", ["First name"] = "이름", ["Last name"] = "성",
            ["Gender"] = "성별", ["Email"] = "이메일", ["Phone"] = "전화번호", ["Date of birth"] = "생년월일", ["Nationality"] = "국적",
            ["ID card (CIN)"] = "신분증 (CIN)", ["Address"] = "주소",
            ["Professional details"] = "직무 정보", ["Organization"] = "소속", ["Role"] = "직책",
            ["Date of appointment"] = "임용일", ["Experience"] = "경력", ["Contract"] = "계약", ["Contract start"] = "계약 시작",
            ["Contract end"] = "계약 종료", ["Duration"] = "기간", ["Salary (MAD per month)"] = "급여 (월, MAD)",
            ["Education"] = "학력", ["Highest degree"] = "최종 학위", ["Graduation year"] = "졸업 연도", ["University"] = "대학교",
            ["Specialization"] = "전문 분야", ["Module taught"] = "담당 과목",
            ["Documents"] = "문서", ["Scans or photos. Open any of them from here later."] = "스캔 또는 사진. 나중에 여기에서 열 수 있습니다.",
            ["ID card, front"] = "신분증 앞면", ["ID card, back"] = "신분증 뒷면", ["Open the file"] = "파일 열기",
            ["Resume"] = "이력서", ["Diploma"] = "졸업장", ["Choose file"] = "파일 선택", ["No file chosen"] = "선택된 파일 없음",
            ["Delete employee"] = "직원 삭제", ["Delete student"] = "학생 삭제", ["Delete teacher"] = "교사 삭제",
            ["Save changes"] = "변경 사항 저장", ["Save employee"] = "직원 저장", ["Save student"] = "학생 저장", ["Save teacher"] = "교사 저장",
            ["Enrolment"] = "등록", ["Where the student comes from and which class they join."] = "학생의 출신과 소속될 학급.",
            ["School or university"] = "학교 또는 대학교", ["Major"] = "전공", ["Joining date"] = "입학일", ["Previous studies."] = "이전 학업.",
            ["High school or university"] = "고등학교 또는 대학교", ["Emergency contact"] = "비상 연락처",
            ["Who to call first, usually a parent or guardian."] = "가장 먼저 연락할 사람, 보통 부모나 보호자입니다.",
            ["Relation"] = "관계", ["Emergency contact ID card, front"] = "비상 연락처 신분증 앞면",
            ["Emergency contact ID card, back"] = "비상 연락처 신분증 뒷면", ["Male"] = "남성", ["Female"] = "여성", ["Select"] = "선택",
            ["Search by first, middle or last name"] = "이름, 중간 이름 또는 성으로 검색",
            ["Choose who to add"] = "추가할 사람 선택", ["Anything worth knowing about this class"] = "이 학급에 대해 알아둘 만한 내용",
            ["Calculated from the appointment date"] = "임용일로 자동 계산", ["Calculated"] = "자동 계산", ["Choose a module"] = "과목 선택",
            ["Parent, guardian..."] = "부모, 보호자...", ["e.g. 2A Computer Science"] = "예: 2A 컴퓨터 과학", ["e.g. Algebra"] = "예: 대수학", ["e.g. Computer Science"] = "예: 컴퓨터 과학",
            ["Search this class"] = "이 학급에서 검색", ["Every teacher is already in this class"] = "모든 교사가 이미 이 학급에 있습니다",
            ["Every student is already in this class"] = "모든 학생이 이미 이 학급에 있습니다", ["No major"] = "전공 없음", ["started"] = "시작",

            // ---- document viewer
            ["Previous page"] = "이전 페이지", ["Next page"] = "다음 페이지", ["Zoom out  (-)"] = "축소  (-)", ["Zoom in  (+)"] = "확대  (+)",
            ["Fit to screen  (0 or double-click)"] = "화면에 맞추기  (0 또는 더블 클릭)", ["Close  (Esc)"] = "닫기  (Esc)", ["Document"] = "문서",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "EduTrack의 모양과 데이터 사본을 보관하는 방법.",
            ["Appearance"] = "모양", ["Backup and restore"] = "백업 및 복원", ["Theme"] = "테마", ["Language"] = "언어",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "클래식은 EduTrack의 기본 모양입니다. 페이퍼와 슬레이트는 녹색 대안입니다. 선택한 내용은 기억됩니다.",
            ["Classic (original)"] = "클래식 (기본)", ["Paper (light green)"] = "페이퍼 (연한 녹색)", ["Slate (dark green)"] = "슬레이트 (진한 녹색)",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "앱 전체의 언어를 선택하세요. 선택한 내용은 기억됩니다.",
            ["Save a copy"] = "사본 저장", ["Export one list to an XML file you can keep safe or move to another computer."] = "목록 하나를 XML 파일로 내보내 안전하게 보관하거나 다른 컴퓨터로 옮길 수 있습니다.",
            ["What to save"] = "저장할 항목", ["Save to file"] = "파일로 저장", ["Show in folder"] = "폴더에서 보기",
            ["Restore from a copy"] = "사본에서 복원", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "위에서 저장한 파일을 선택하세요. ID가 같은 기록은 파일의 내용으로 대체됩니다.",
            ["Restore data"] = "데이터 복원",
            ["Choose what to save first."] = "먼저 저장할 항목을 선택하세요.", ["Choose a saved copy"] = "저장한 사본 선택",
            ["Choose a file to restore first."] = "먼저 복원할 파일을 선택하세요.", ["That file has no records to restore."] = "해당 파일에는 복원할 기록이 없습니다.",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "EduTrack은 '사본 저장' 버튼으로 저장한 학생, 교사, 직원만 복원할 수 있습니다.",
            ["Couldn't save"] = "저장할 수 없음", ["Can't restore"] = "복원할 수 없음", ["Couldn't restore"] = "복원하지 못했습니다",
            ["Can't read the file"] = "파일을 읽을 수 없음", ["Nothing chosen"] = "선택된 항목 없음", ["Unknown file"] = "알 수 없는 파일", ["Nothing to restore"] = "복원할 항목 없음",

            // ---- about
            ["About EduTrack"] = "EduTrack 정보", ["A desktop register for schools and training centres."] = "학교와 교육 센터를 위한 데스크톱 명부.",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "EduTrack은 학생, 교사, 직원, 학급을 한곳에서 관리합니다. 누구인지, 어떤 문서를 보관하는지 기록하고, 학급별 주간 시간표를 만들고, 성적을 입력하며, 데이터의 XML 사본을 보관할 수 있습니다.",
            ["Contact"] = "연락처", ["Scan to visit the website"] = "스캔하여 웹사이트 방문",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "휴대폰 카메라를 여기에 대면 adamrohal.tech가 열립니다. 질문이나 아이디어는 왼쪽 이메일로 보내 주세요.",
            ["Support the developer"] = "개발자 후원",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "EduTrack은 한 사람이 만들고 업데이트합니다. 시간을 절약해 주었다면 Ko-fi에서 커피 한 잔으로 후원해 주세요. 무료로 유지하고 계속 개선하는 데 도움이 됩니다.",
            ["Support on Ko-fi"] = "Ko-fi에서 후원",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "이 직원을 삭제하시겠습니까?",
            ["Are you sure you want to delete this student?"] = "이 학생을 삭제하시겠습니까?",
            ["Are you sure you want to delete this Teacher?"] = "이 교사를 삭제하시겠습니까?",
            ["Employee has been deleted successfully."] = "직원을 삭제했습니다.", ["Student has been deleted successfully."] = "학생을 삭제했습니다.", ["Teacher has been deleted successfully."] = "교사를 삭제했습니다.",
            ["Employee information updated successfully!"] = "직원 정보를 업데이트했습니다.", ["Student information updated successfully!"] = "학생 정보를 업데이트했습니다.", ["Teacher information updated successfully!"] = "교사 정보를 업데이트했습니다.",
            ["Failed to update employee information!"] = "직원 정보를 업데이트하지 못했습니다.", ["Failed to update student information!"] = "학생 정보를 업데이트하지 못했습니다.", ["Failed to update teacher information!"] = "교사 정보를 업데이트하지 못했습니다.",
            ["Employee information saved successfully!"] = "직원 정보를 저장했습니다.", ["Student information saved successfully!"] = "학생 정보를 저장했습니다.", ["Teacher information saved successfully!"] = "교사 정보를 저장했습니다.",
            ["Failed to save employee information!"] = "직원 정보를 저장하지 못했습니다.", ["Failed to save student information!"] = "학생 정보를 저장하지 못했습니다.", ["Failed to save teacher information!"] = "교사 정보를 저장하지 못했습니다.",
            ["Please select a class."] = "학급을 선택하세요.", ["Give the class a name."] = "학급 이름을 입력하세요.",
            ["Class saved successfully."] = "학급을 저장했습니다.", ["The class could not be saved."] = "학급을 저장할 수 없습니다.",
            ["The class was deleted."] = "학급을 삭제했습니다.", ["Module added successfully."] = "과목을 추가했습니다.", ["Grades saved successfully."] = "성적을 저장했습니다.",
            ["Choose a class and type the name of the module first."] = "먼저 학급을 선택하고 과목 이름을 입력하세요.",
            ["Choose a module first."] = "먼저 과목을 선택하세요.", ["Choose a class first."] = "먼저 학급을 선택하세요.", ["Choose who to add first."] = "먼저 추가할 사람을 선택하세요.",
            ["Choose a file first."] = "먼저 파일을 선택하세요.",
            ["The classes could not be loaded. Check that the database is running."] = "학급을 불러올 수 없습니다. 데이터베이스가 실행 중인지 확인하세요.",
            ["The modules could not be loaded."] = "과목을 불러올 수 없습니다.", ["The students could not be loaded."] = "학생을 불러올 수 없습니다.",
            ["The module could not be added."] = "과목을 추가할 수 없습니다.", ["The grades could not be saved."] = "성적을 저장할 수 없습니다.",
            ["The schedule could not be loaded."] = "시간표를 불러올 수 없습니다.", ["The schedule could not be saved."] = "시간표를 저장할 수 없습니다.",
            ["The teachers of this class could not be loaded."] = "이 학급의 교사를 불러올 수 없습니다.", ["The students of this class could not be loaded."] = "이 학급의 학생을 불러올 수 없습니다.",
            ["The list of people could not be loaded."] = "사람 목록을 불러올 수 없습니다.", ["They could not be added to the class."] = "학급에 추가할 수 없습니다.",
            ["That change could not be saved."] = "해당 변경 사항을 저장할 수 없습니다.", ["The class could not be deleted."] = "학급을 삭제할 수 없습니다.",
            ["The employee list could not be loaded. Check that the database is running."] = "직원 목록을 불러올 수 없습니다. 데이터베이스가 실행 중인지 확인하세요.",
            ["The student list could not be loaded. Check that the database is running."] = "학생 목록을 불러올 수 없습니다. 데이터베이스가 실행 중인지 확인하세요.",
            ["The teacher list could not be loaded. Check that the database is running."] = "교사 목록을 불러올 수 없습니다. 데이터베이스가 실행 중인지 확인하세요.",
            ["The file could not be saved."] = "파일을 저장할 수 없습니다.", ["That file isn't a readable EduTrack copy."] = "해당 파일은 읽을 수 있는 EduTrack 사본이 아닙니다.",
            ["Nothing was changed. The restore stopped because:"] = "변경된 내용이 없습니다. 복원이 다음 이유로 중지되었습니다:",
            ["This file could not be opened."] = "이 파일을 열 수 없습니다.", ["This page could not be shown."] = "이 페이지를 표시할 수 없습니다.",
            ["EduTrack hit a problem it could not recover from:"] = "EduTrack이 복구할 수 없는 문제를 만났습니다:",
            ["An error occurred: "] = "오류가 발생했습니다: ",
            ["Can't load classes"] = "학급을 불러올 수 없음", ["Can't load employees"] = "직원을 불러올 수 없음", ["Can't load modules"] = "과목을 불러올 수 없음",
            ["Can't load people"] = "사람 목록을 불러올 수 없음", ["Can't load schedule"] = "시간표를 불러올 수 없음", ["Can't load students"] = "학생을 불러올 수 없음",
            ["Can't load teachers"] = "교사를 불러올 수 없음", ["Confirmation"] = "확인", ["Couldn't add"] = "추가할 수 없음", ["Couldn't delete"] = "삭제할 수 없음",
            ["Error"] = "오류", ["Information"] = "정보", ["Module not added"] = "과목이 추가되지 않음", ["Move student"] = "학생 이동", ["No class chosen"] = "선택된 학급 없음",
            ["No module chosen"] = "선택된 과목 없음", ["Remove from class"] = "학급에서 제외",
        };

        private static readonly List<Pat> KoPats = new List<Pat>();

        private static string NounKo(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "학생";
                case "teacher": case "teachers": return "교사";
                case "employee": case "employees": return "직원";
                default: return english;
            }
        }

        private static void InitKo()
        {
            Dictionaries[Korean] = Ko;
            Patterns[Korean] = KoPats;
            P(KoPats, @"^(\d+) (student|teacher|employee)s?$", m => NounKo(m.Groups[2].Value) + " " + m.Groups[1].Value + "명");
            P(KoPats, @"^(\d+) matches?$", m => m.Groups[1].Value + "건");
            P(KoPats, @"^Version (.*)$", m => "버전 " + m.Groups[1].Value);
            P(KoPats, @"^Save (students|teachers|employees)$", m => NounKo(m.Groups[1].Value) + " 저장");
            P(KoPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => m.Groups[1].Value + "개의 기록을 " + NounKo(m.Groups[2].Value) + "에 복원하시겠습니까? ID가 같은 기록은 대체됩니다.");
            P(KoPats, @"^None of the columns in that file match the (.*) table\.$", m => "해당 파일의 열 중 " + NounKo(m.Groups[1].Value) + " 테이블과 일치하는 열이 없습니다.");
            P(KoPats, @"^The grade for (.*) isn't valid\. (.*)$", m => m.Groups[1].Value + "의 성적이 올바르지 않습니다. " + T(m.Groups[2].Value));
            P(KoPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => m.Groups[1].Value + "에 저장하지 않은 변경 사항이 있습니다. 학급을 전환하고 변경 사항을 버리시겠습니까?");
            P(KoPats, @"^Schedule saved for (.*)\.$", m => m.Groups[1].Value + "의 시간표를 저장했습니다.");
            P(KoPats, @"^Teachers  \((\d+)\)$", m => "교사  (" + m.Groups[1].Value + ")");
            P(KoPats, @"^Students  \((\d+) found\)$", m => "학생  (" + m.Groups[1].Value + "명 검색됨)");
            P(KoPats, @"^Students  \((\d+)\)$", m => "학생  (" + m.Groups[1].Value + ")");
            P(KoPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => m.Groups[1].Value + "을(를) " + m.Groups[2].Value + "에서 제외하시겠습니까? " + (m.Groups[3].Value == "student" ? "학생 기록은 유지됩니다." : "교사 기록은 유지됩니다."));
            P(KoPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "'" + m.Groups[1].Value + "' 학급을 삭제하시겠습니까? 시간표, 과목, 성적도 함께 삭제됩니다. " + T(m.Groups[2].Value));
            P(KoPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "'" + m.Groups[2].Value + "'와(과) 일치하는 " + NounKo(m.Groups[1].Value) + "이(가) 없습니다");
            P(KoPats, @"^created (.*)$", m => m.Groups[1].Value + "에 생성됨");
            P(KoPats, @"^ended (\d+) days? ago$", m => m.Groups[1].Value + "일 전에 종료");
            P(KoPats, @"^in (\d+) days?$", m => m.Groups[1].Value + "일 후");
            P(KoPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + ", " + m.Groups[2].Value + "님");
            P(KoPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(KoPats, @"^(Error [a-z ]+|Error): (.*)$", m => "오류: " + m.Groups[2].Value);
        }
    }
}
