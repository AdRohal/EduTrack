using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> Spanish.</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Es = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "Inicio", ["Students"] = "Estudiantes", ["Teachers"] = "Profesores", ["Employees"] = "Empleados",
            ["Classes"] = "Clases", ["Settings"] = "Ajustes", ["About"] = "Acerca de",
            ["Home  (Ctrl+1)"] = "Inicio  (Ctrl+1)", ["Students  (Ctrl+2)"] = "Estudiantes  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "Profesores  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "Empleados  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "Clases  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "Ajustes  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "Acerca de  (Ctrl+7)",
            ["Administrator"] = "Administrador", ["Change theme"] = "Cambiar tema", ["Sign out"] = "Cerrar sesión",
            ["Minimize"] = "Minimizar", ["Maximize"] = "Maximizar", ["Restore"] = "Restaurar", ["Close"] = "Cerrar",
            ["Student"] = "Estudiante", ["Teacher"] = "Profesor", ["Employee"] = "Empleado", ["Class"] = "Clase",
            ["OK"] = "Aceptar", ["Yes"] = "Sí", ["No"] = "No", ["Cancel"] = "Cancelar", ["Delete"] = "Eliminar", ["Keep it"] = "Conservar",
            ["Something went wrong"] = "Algo salió mal", ["Check this first"] = "Revisa esto primero", ["Please confirm"] = "Por favor, confirma",
            ["year"] = "año", ["years"] = "años", ["month"] = "mes", ["months"] = "meses",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - Iniciar sesión",
            ["One register for your whole school."] = "Un solo registro para toda tu escuela.",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "Estudiantes, profesores, personal y clases, juntos y fáciles de encontrar.",
            ["Sign in"] = "Iniciar sesión", ["Use your administrator account to open the register."] = "Usa tu cuenta de administrador para abrir el registro.",
            ["Signed in last time"] = "Sesión iniciada la última vez", ["Username"] = "Usuario", ["Password"] = "Contraseña",
            ["Confirm password"] = "Confirmar contraseña", ["At least 6 characters."] = "Al menos 6 caracteres.",
            ["Not you? Use another account"] = "¿No eres tú? Usa otra cuenta", ["First time here?"] = "¿Primera vez aquí?", ["Create an account"] = "Crear una cuenta",
            ["Connecting to the database"] = "Conectando con la base de datos", ["Starting the database"] = "Iniciando la base de datos",
            ["Getting things ready"] = "Preparando todo", ["Ready"] = "Listo", ["Can't reach the database"] = "No se puede acceder a la base de datos",
            ["Try again"] = "Reintentar", ["Open anyway"] = "Abrir de todos modos",
            ["No accounts exist yet. Create the first administrator to get started."] = "Aún no hay cuentas. Crea el primer administrador para empezar.",
            ["Welcome back"] = "Bienvenido de nuevo", ["Enter your password to continue."] = "Introduce tu contraseña para continuar.",
            ["Enter your username."] = "Introduce tu usuario.", ["Enter your password."] = "Introduce tu contraseña.",
            ["Use at least 6 characters for the password."] = "Usa al menos 6 caracteres para la contraseña.",
            ["The two passwords don't match."] = "Las dos contraseñas no coinciden.",
            ["That username and password don't match. Check them and try again."] = "Ese usuario y contraseña no coinciden. Compruébalos e inténtalo de nuevo.",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "No se puede acceder a la base de datos. Asegúrate de que MySQL esté en marcha e inténtalo de nuevo.",
            ["EduTrack is already running."] = "EduTrack ya se está ejecutando.",
            ["Unexpected error"] = "Error inesperado",
            ["Open EduTrack"] = "Abrir EduTrack", ["Exit EduTrack"] = "Salir de EduTrack",

            // ---- home
            ["Good morning"] = "Buenos días", ["Good afternoon"] = "Buenas tardes", ["Good evening"] = "Buenas noches",
            ["Here is what the register holds right now."] = "Esto es lo que contiene el registro ahora mismo.",
            ["Welcome. Let's set up your school."] = "Bienvenido. Vamos a configurar tu escuela.",
            ["New class"] = "Nueva clase", ["New teacher"] = "Nuevo profesor", ["New student"] = "Nuevo estudiante", ["New employee"] = "Nuevo empleado",
            ["Your register is empty. Start here."] = "Tu registro está vacío. Empieza aquí.",
            ["Set things up in this order and everything else falls into place."] = "Configura todo en este orden y lo demás encajará.",
            ["Create a class"] = "Crear una clase", ["A class is the group students and teachers belong to."] = "Una clase es el grupo al que pertenecen estudiantes y profesores.",
            ["Add your teachers"] = "Añade a tus profesores", ["Contracts, modules and documents live on each teacher's page."] = "Los contratos, módulos y documentos están en la página de cada profesor.",
            ["Enrol your students"] = "Matricula a tus estudiantes", ["Pick a class for each student while you add them."] = "Elige una clase para cada estudiante al añadirlo.",
            ["Recently added students"] = "Estudiantes añadidos recientemente", ["See all"] = "Ver todo",
            ["Students you add will be listed here."] = "Los estudiantes que añadas aparecerán aquí.",
            ["Class sizes"] = "Tamaño de las clases", ["Classes you create will show how many students they hold."] = "Las clases que crees mostrarán cuántos estudiantes tienen.",
            ["Students by major"] = "Estudiantes por especialidad", ["Majors appear here once students have one."] = "Las especialidades aparecen aquí cuando los estudiantes tienen una.",
            ["Contracts ending soon"] = "Contratos que terminan pronto", ["Teachers and employees, next 90 days"] = "Profesores y empleados, próximos 90 días",
            ["No contract ends in the next 90 days."] = "Ningún contrato termina en los próximos 90 días.",
            ["Records to complete"] = "Fichas por completar", ["Missing a photo or an ID card scan"] = "Falta una foto o el escaneo del documento de identidad",
            ["Every record has a photo and both ID scans."] = "Todas las fichas tienen foto y los dos escaneos del documento.",
            ["today"] = "hoy",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "Personal administrativo y de apoyo, con contratos y salarios.",
            ["All employees"] = "Todos los empleados", ["Everyone enrolled, with their contact details and documents."] = "Todas las personas matriculadas, con sus datos de contacto y documentos.",
            ["All students"] = "Todos los estudiantes", ["The teaching staff, their modules and contracts."] = "El profesorado, sus módulos y contratos.",
            ["All teachers"] = "Todos los profesores", ["All classes"] = "Todas las clases",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "Grupos de estudiantes: quién les enseña, cuándo se reúnen y cómo se califican.",
            ["Schedule"] = "Horario", ["Grades"] = "Notas",
            ["Reload the list"] = "Recargar la lista", ["Open details"] = "Abrir ficha",
            ["Joined"] = "Alta", ["Contract ends"] = "Fin del contrato",
            ["Add the first employee"] = "Añadir el primer empleado", ["Add the first student"] = "Añadir el primer estudiante", ["Add the first teacher"] = "Añadir el primer profesor",
            ["Create the first class"] = "Crear la primera clase", ["Choose a class"] = "Elegir una clase",
            ["Pick a class above to see its teachers and students."] = "Elige una clase arriba para ver sus profesores y estudiantes.",
            ["No employees yet"] = "Aún no hay empleados", ["Employees you add will show up here, ready to search and open."] = "Los empleados que añadas aparecerán aquí, listos para buscar y abrir.",
            ["No students yet"] = "Aún no hay estudiantes", ["Students you add will show up here, ready to search and open."] = "Los estudiantes que añadas aparecerán aquí, listos para buscar y abrir.",
            ["No teachers yet"] = "Aún no hay profesores", ["Teachers you add will show up here, ready to search and open."] = "Los profesores que añadas aparecerán aquí, listos para buscar y abrir.",
            ["Check the spelling, or try just a first or last name."] = "Revisa la ortografía o prueba solo con un nombre o un apellido.",
            ["Add to this class"] = "Añadir a esta clase", ["A student"] = "Un estudiante", ["A teacher"] = "Un profesor", ["Add to class"] = "Añadir a la clase",
            ["Remove from this class"] = "Quitar de esta clase",
            ["No teacher has been assigned to this class yet."] = "Aún no se ha asignado ningún profesor a esta clase.",
            ["No student is in this class yet. Use the Add to class bar above."] = "Aún no hay estudiantes en esta clase. Usa la barra «Añadir a la clase» de arriba.",
            ["Delete class"] = "Eliminar clase", ["Class details"] = "Detalles de la clase",
            ["A class groups students, teachers, a weekly schedule and grades."] = "Una clase agrupa estudiantes, profesores, un horario semanal y notas.",
            ["Class name *"] = "Nombre de la clase *", ["Start date"] = "Fecha de inicio", ["Description"] = "Descripción", ["Save class"] = "Guardar clase",
            ["Last class added"] = "Última clase añadida",
            ["No class has been added yet. The one you save will appear here."] = "Aún no se ha añadido ninguna clase. La que guardes aparecerá aquí.",
            ["The last class could not be loaded."] = "No se pudo cargar la última clase.", ["no date"] = "sin fecha",

            // ---- grades / schedule
            ["Module"] = "Módulo", ["Add a module to this class"] = "Añadir un módulo a esta clase", ["Add module"] = "Añadir módulo",
            ["Grade"] = "Nota", ["No grades to enter yet"] = "Aún no hay notas que introducir",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "Elige una clase y un módulo arriba. Los estudiantes de esa clase aparecen aquí, listos para sus notas.",
            ["Save grades"] = "Guardar notas",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "Haz clic en una celda y escribe la asignatura o el aula. Tab pasa al día siguiente.",
            ["Unsaved changes"] = "Cambios sin guardar", ["Save schedule"] = "Guardar horario", ["Time"] = "Hora",
            ["Monday"] = "Lunes", ["Tuesday"] = "Martes", ["Wednesday"] = "Miércoles", ["Thursday"] = "Jueves", ["Friday"] = "Viernes", ["Saturday"] = "Sábado", ["Sunday"] = "Domingo",
            ["No classes to schedule yet"] = "Aún no hay clases que planificar", ["Create a class first, then plan its week here."] = "Crea primero una clase y planifica aquí su semana.",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "Usa un número del 0 al 20 o una palabra corta como ABS.",
            ["Check the grades"] = "Revisa las notas",

            // ---- forms / details
            ["Back to the list"] = "Volver a la lista", ["Back to employees"] = "Volver a empleados", ["Back to students"] = "Volver a estudiantes", ["Back to teachers"] = "Volver a profesores",
            ["Employee details"] = "Detalles del empleado", ["Student details"] = "Detalles del estudiante", ["Teacher details"] = "Detalles del profesor",
            ["Edit anything below, then choose Save changes."] = "Edita lo que quieras abajo y luego elige Guardar cambios.",
            ["Choose photo"] = "Elegir foto", ["JPG or PNG"] = "JPG o PNG",
            ["Personal information"] = "Información personal", ["Fields marked with * are required."] = "Los campos marcados con * son obligatorios.",
            ["First name *"] = "Nombre *", ["Middle name"] = "Segundo nombre", ["Last name *"] = "Apellido *", ["First name"] = "Nombre", ["Last name"] = "Apellido",
            ["Gender"] = "Género", ["Email"] = "Correo electrónico", ["Phone"] = "Teléfono", ["Date of birth"] = "Fecha de nacimiento", ["Nationality"] = "Nacionalidad",
            ["ID card (CIN)"] = "Documento de identidad (CIN)", ["Address"] = "Dirección",
            ["Professional details"] = "Datos profesionales", ["Organization"] = "Organización", ["Role"] = "Cargo",
            ["Date of appointment"] = "Fecha de nombramiento", ["Experience"] = "Experiencia", ["Contract"] = "Contrato", ["Contract start"] = "Inicio del contrato",
            ["Contract end"] = "Fin del contrato", ["Duration"] = "Duración", ["Salary (MAD per month)"] = "Salario (MAD al mes)",
            ["Education"] = "Formación", ["Highest degree"] = "Título más alto", ["Graduation year"] = "Año de graduación", ["University"] = "Universidad",
            ["Specialization"] = "Especialización", ["Module taught"] = "Módulo que imparte",
            ["Documents"] = "Documentos", ["Scans or photos. Open any of them from here later."] = "Escaneos o fotos. Ábrelos desde aquí más tarde.",
            ["ID card, front"] = "Documento de identidad, anverso", ["ID card, back"] = "Documento de identidad, reverso", ["Open the file"] = "Abrir el archivo",
            ["Resume"] = "Currículum", ["Diploma"] = "Diploma", ["Choose file"] = "Elegir archivo", ["No file chosen"] = "Ningún archivo elegido",
            ["Delete employee"] = "Eliminar empleado", ["Delete student"] = "Eliminar estudiante", ["Delete teacher"] = "Eliminar profesor",
            ["Save changes"] = "Guardar cambios", ["Save employee"] = "Guardar empleado", ["Save student"] = "Guardar estudiante", ["Save teacher"] = "Guardar profesor",
            ["Enrolment"] = "Matrícula", ["Where the student comes from and which class they join."] = "De dónde viene el estudiante y a qué clase se une.",
            ["School or university"] = "Escuela o universidad", ["Major"] = "Especialidad", ["Joining date"] = "Fecha de incorporación", ["Previous studies."] = "Estudios anteriores.",
            ["High school or university"] = "Instituto o universidad", ["Emergency contact"] = "Contacto de emergencia",
            ["Who to call first, usually a parent or guardian."] = "A quién llamar primero, normalmente un padre o tutor.",
            ["Relation"] = "Parentesco", ["Emergency contact ID card, front"] = "Documento del contacto de emergencia, anverso",
            ["Emergency contact ID card, back"] = "Documento del contacto de emergencia, reverso", ["Male"] = "Hombre", ["Female"] = "Mujer", ["Select"] = "Elegir",
            ["Search by first, middle or last name"] = "Buscar por nombre, segundo nombre o apellido",
            ["Choose who to add"] = "Elegir a quién añadir", ["Anything worth knowing about this class"] = "Cualquier cosa que valga la pena saber de esta clase",
            ["Calculated from the appointment date"] = "Calculado a partir de la fecha de nombramiento", ["Calculated"] = "Calculado", ["Choose a module"] = "Elegir un módulo",
            ["Parent, guardian..."] = "Padre, tutor...", ["e.g. 2A Computer Science"] = "p. ej. 2A Informática", ["e.g. Algebra"] = "p. ej. Álgebra", ["e.g. Computer Science"] = "p. ej. Informática",
            ["Search this class"] = "Buscar en esta clase", ["Every teacher is already in this class"] = "Todos los profesores ya están en esta clase",
            ["Every student is already in this class"] = "Todos los estudiantes ya están en esta clase", ["No major"] = "Sin especialidad", ["started"] = "desde el",

            // ---- document viewer
            ["Previous page"] = "Página anterior", ["Next page"] = "Página siguiente", ["Zoom out  (-)"] = "Alejar  (-)", ["Zoom in  (+)"] = "Acercar  (+)",
            ["Fit to screen  (0 or double-click)"] = "Ajustar a la pantalla  (0 o doble clic)", ["Close  (Esc)"] = "Cerrar  (Esc)", ["Document"] = "Documento",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "Cómo se ve EduTrack y cómo guardar una copia de tus datos.",
            ["Appearance"] = "Apariencia", ["Backup and restore"] = "Copia de seguridad y restauración", ["Theme"] = "Tema", ["Language"] = "Idioma",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "Clásico es el aspecto original de EduTrack. Papel y Pizarra son las alternativas verdes. EduTrack recuerda tu elección.",
            ["Classic (original)"] = "Clásico (original)", ["Paper (light green)"] = "Papel (verde claro)", ["Slate (dark green)"] = "Pizarra (verde oscuro)",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "Elige el idioma de toda la aplicación. EduTrack recuerda tu elección.",
            ["Save a copy"] = "Guardar una copia", ["Export one list to an XML file you can keep safe or move to another computer."] = "Exporta una lista a un archivo XML que puedes guardar a salvo o llevar a otro ordenador.",
            ["What to save"] = "Qué guardar", ["Save to file"] = "Guardar en un archivo", ["Show in folder"] = "Mostrar en la carpeta",
            ["Restore from a copy"] = "Restaurar desde una copia", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "Elige un archivo guardado arriba. Las fichas con el mismo ID se reemplazan por las del archivo.",
            ["Restore data"] = "Restaurar datos",
            ["Choose what to save first."] = "Elige primero qué guardar.", ["Choose a saved copy"] = "Elegir una copia guardada",
            ["Choose a file to restore first."] = "Elige primero un archivo para restaurar.", ["That file has no records to restore."] = "Ese archivo no tiene fichas para restaurar.",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "EduTrack solo puede restaurar estudiantes, profesores o empleados guardados con el botón «Guardar una copia».",
            ["Couldn't save"] = "No se pudo guardar", ["Can't restore"] = "No se puede restaurar", ["Couldn't restore"] = "No se pudo restaurar",
            ["Can't read the file"] = "No se puede leer el archivo", ["Nothing chosen"] = "Nada elegido", ["Unknown file"] = "Archivo desconocido", ["Nothing to restore"] = "Nada que restaurar",

            // ---- about
            ["About EduTrack"] = "Acerca de EduTrack", ["A desktop register for schools and training centres."] = "Un registro de escritorio para escuelas y centros de formación.",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "EduTrack reúne estudiantes, profesores, personal y clases en un solo lugar. Registra quiénes son y qué documentos tienes, crea el horario semanal de cada clase, introduce las notas y guarda una copia XML de tus datos.",
            ["Contact"] = "Contacto", ["Scan to visit the website"] = "Escanea para visitar el sitio web",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "Apunta la cámara de tu teléfono aquí para abrir adamrohal.tech. ¿Preguntas o ideas? Escribe al correo de la izquierda.",
            ["Support the developer"] = "Apoya al desarrollador",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "EduTrack lo crea y mantiene una sola persona. Si te ahorra tiempo, un café en Ko-fi ayuda a mantenerlo gratis y a mejorarlo.",
            ["Support on Ko-fi"] = "Apoyar en Ko-fi",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "¿Seguro que quieres eliminar a este empleado?",
            ["Are you sure you want to delete this student?"] = "¿Seguro que quieres eliminar a este estudiante?",
            ["Are you sure you want to delete this Teacher?"] = "¿Seguro que quieres eliminar a este profesor?",
            ["Employee has been deleted successfully."] = "El empleado ha sido eliminado.", ["Student has been deleted successfully."] = "El estudiante ha sido eliminado.", ["Teacher has been deleted successfully."] = "El profesor ha sido eliminado.",
            ["Employee information updated successfully!"] = "¡Información del empleado actualizada!", ["Student information updated successfully!"] = "¡Información del estudiante actualizada!", ["Teacher information updated successfully!"] = "¡Información del profesor actualizada!",
            ["Failed to update employee information!"] = "¡No se pudo actualizar la información del empleado!", ["Failed to update student information!"] = "¡No se pudo actualizar la información del estudiante!", ["Failed to update teacher information!"] = "¡No se pudo actualizar la información del profesor!",
            ["Employee information saved successfully!"] = "¡Información del empleado guardada!", ["Student information saved successfully!"] = "¡Información del estudiante guardada!", ["Teacher information saved successfully!"] = "¡Información del profesor guardada!",
            ["Failed to save employee information!"] = "¡No se pudo guardar la información del empleado!", ["Failed to save student information!"] = "¡No se pudo guardar la información del estudiante!", ["Failed to save teacher information!"] = "¡No se pudo guardar la información del profesor!",
            ["Please select a class."] = "Por favor, elige una clase.", ["Give the class a name."] = "Ponle un nombre a la clase.",
            ["Class saved successfully."] = "Clase guardada.", ["The class could not be saved."] = "No se pudo guardar la clase.",
            ["The class was deleted."] = "La clase fue eliminada.", ["Module added successfully."] = "Módulo añadido.", ["Grades saved successfully."] = "Notas guardadas.",
            ["Choose a class and type the name of the module first."] = "Elige primero una clase y escribe el nombre del módulo.",
            ["Choose a module first."] = "Elige primero un módulo.", ["Choose a class first."] = "Elige primero una clase.", ["Choose who to add first."] = "Elige primero a quién añadir.",
            ["Choose a file first."] = "Elige primero un archivo.",
            ["The classes could not be loaded. Check that the database is running."] = "No se pudieron cargar las clases. Comprueba que la base de datos esté en marcha.",
            ["The modules could not be loaded."] = "No se pudieron cargar los módulos.", ["The students could not be loaded."] = "No se pudieron cargar los estudiantes.",
            ["The module could not be added."] = "No se pudo añadir el módulo.", ["The grades could not be saved."] = "No se pudieron guardar las notas.",
            ["The schedule could not be loaded."] = "No se pudo cargar el horario.", ["The schedule could not be saved."] = "No se pudo guardar el horario.",
            ["The teachers of this class could not be loaded."] = "No se pudieron cargar los profesores de esta clase.", ["The students of this class could not be loaded."] = "No se pudieron cargar los estudiantes de esta clase.",
            ["The list of people could not be loaded."] = "No se pudo cargar la lista de personas.", ["They could not be added to the class."] = "No se pudieron añadir a la clase.",
            ["That change could not be saved."] = "No se pudo guardar ese cambio.", ["The class could not be deleted."] = "No se pudo eliminar la clase.",
            ["The employee list could not be loaded. Check that the database is running."] = "No se pudo cargar la lista de empleados. Comprueba que la base de datos esté en marcha.",
            ["The student list could not be loaded. Check that the database is running."] = "No se pudo cargar la lista de estudiantes. Comprueba que la base de datos esté en marcha.",
            ["The teacher list could not be loaded. Check that the database is running."] = "No se pudo cargar la lista de profesores. Comprueba que la base de datos esté en marcha.",
            ["The file could not be saved."] = "No se pudo guardar el archivo.", ["That file isn't a readable EduTrack copy."] = "Ese archivo no es una copia de EduTrack legible.",
            ["Nothing was changed. The restore stopped because:"] = "No se cambió nada. La restauración se detuvo porque:",
            ["This file could not be opened."] = "No se pudo abrir este archivo.", ["This page could not be shown."] = "No se pudo mostrar esta página.",
            ["EduTrack hit a problem it could not recover from:"] = "EduTrack tuvo un problema del que no pudo recuperarse:",
            ["An error occurred: "] = "Se produjo un error: ",
            ["Can't load classes"] = "No se pueden cargar las clases", ["Can't load employees"] = "No se pueden cargar los empleados", ["Can't load modules"] = "No se pueden cargar los módulos",
            ["Can't load people"] = "No se pueden cargar las personas", ["Can't load schedule"] = "No se puede cargar el horario", ["Can't load students"] = "No se pueden cargar los estudiantes",
            ["Can't load teachers"] = "No se pueden cargar los profesores", ["Confirmation"] = "Confirmación", ["Couldn't add"] = "No se pudo añadir", ["Couldn't delete"] = "No se pudo eliminar",
            ["Error"] = "Error", ["Information"] = "Información", ["Module not added"] = "Módulo no añadido", ["Move student"] = "Mover estudiante", ["No class chosen"] = "Ninguna clase elegida",
            ["No module chosen"] = "Ningún módulo elegido", ["Remove from class"] = "Quitar de la clase",
        };

        private static readonly List<Pat> EsPats = new List<Pat>();

        private static string NounEs(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "estudiantes";
                case "teacher": case "teachers": return "profesores";
                case "employee": case "employees": return "empleados";
                default: return english;
            }
        }

        private static void InitEs()
        {
            Dictionaries[Spanish] = Es;
            Patterns[Spanish] = EsPats;
            P(EsPats, @"^(\d+) (student|teacher|employee)s?$", m => m.Groups[1].Value + " " + (m.Groups[2].Value == "student" ? "estudiante" : m.Groups[2].Value == "teacher" ? "profesor" : "empleado") + (m.Groups[1].Value == "1" ? "" : m.Groups[2].Value == "teacher" ? "es" : "s"));
            P(EsPats, @"^(\d+) matches?$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " resultado" : " resultados"));
            P(EsPats, @"^Version (.*)$", m => "Versión " + m.Groups[1].Value);
            P(EsPats, @"^Save (students|teachers|employees)$", m => "Guardar " + NounEs(m.Groups[1].Value));
            P(EsPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => "¿Restaurar " + m.Groups[1].Value + " fichas en " + NounEs(m.Groups[2].Value) + "? Las fichas con el mismo ID se reemplazarán.");
            P(EsPats, @"^None of the columns in that file match the (.*) table\.$", m => "Ninguna columna de ese archivo coincide con la tabla de " + NounEs(m.Groups[1].Value) + ".");
            P(EsPats, @"^The grade for (.*) isn't valid\. (.*)$", m => "La nota de " + m.Groups[1].Value + " no es válida. " + T(m.Groups[2].Value));
            P(EsPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => "Tienes cambios sin guardar en " + m.Groups[1].Value + ". ¿Cambiar de clase y perderlos?");
            P(EsPats, @"^Schedule saved for (.*)\.$", m => "Horario guardado para " + m.Groups[1].Value + ".");
            P(EsPats, @"^Teachers  \((\d+)\)$", m => "Profesores  (" + m.Groups[1].Value + ")");
            P(EsPats, @"^Students  \((\d+) found\)$", m => "Estudiantes  (" + m.Groups[1].Value + " encontrados)");
            P(EsPats, @"^Students  \((\d+)\)$", m => "Estudiantes  (" + m.Groups[1].Value + ")");
            P(EsPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => "¿Quitar a " + m.Groups[1].Value + " de " + m.Groups[2].Value + "? " + (m.Groups[3].Value == "student" ? "Su ficha de estudiante se conserva." : "Su ficha de profesor se conserva."));
            P(EsPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "¿Eliminar la clase «" + m.Groups[1].Value + "»? Su horario, módulos y notas también se eliminan. " + T(m.Groups[2].Value));
            P(EsPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "Ningún " + (m.Groups[1].Value == "employee" ? "empleado" : m.Groups[1].Value == "student" ? "estudiante" : "profesor") + " coincide con «" + m.Groups[2].Value + "»");
            P(EsPats, @"^created (.*)$", m => "creada el " + m.Groups[1].Value);
            P(EsPats, @"^ended (\d+) days? ago$", m => "terminó hace " + m.Groups[1].Value + (m.Groups[1].Value == "1" ? " día" : " días"));
            P(EsPats, @"^in (\d+) days?$", m => "en " + m.Groups[1].Value + (m.Groups[1].Value == "1" ? " día" : " días"));
            P(EsPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + ", " + m.Groups[2].Value);
            P(EsPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(EsPats, @"^(Error [a-z ]+|Error): (.*)$", m => "Error: " + m.Groups[2].Value);
        }
    }
}
