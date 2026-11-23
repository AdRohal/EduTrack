using System.Collections.Generic;

namespace EduTrack.Utilities
{
    /// <summary>English -> French. The English text is the key, so a missing entry simply stays in English.</summary>
    public static partial class Loc
    {
        private static readonly Dictionary<string, string> Fr = new Dictionary<string, string>
        {
            // ---- general
            ["EduTrack"] = "EduTrack",
            ["Home"] = "Accueil", ["Students"] = "Étudiants", ["Teachers"] = "Enseignants", ["Employees"] = "Employés",
            ["Classes"] = "Classes", ["Settings"] = "Paramètres", ["About"] = "À propos",
            ["Home  (Ctrl+1)"] = "Accueil  (Ctrl+1)", ["Students  (Ctrl+2)"] = "Étudiants  (Ctrl+2)", ["Teachers  (Ctrl+3)"] = "Enseignants  (Ctrl+3)",
            ["Employees  (Ctrl+4)"] = "Employés  (Ctrl+4)", ["Classes  (Ctrl+5)"] = "Classes  (Ctrl+5)", ["Settings  (Ctrl+6)"] = "Paramètres  (Ctrl+6)",
            ["About  (Ctrl+7)"] = "À propos  (Ctrl+7)",
            ["Administrator"] = "Administrateur", ["Change theme"] = "Changer de thème", ["Sign out"] = "Se déconnecter",
            ["Minimize"] = "Réduire", ["Maximize"] = "Agrandir", ["Restore"] = "Restaurer", ["Close"] = "Fermer",
            ["Student"] = "Étudiant", ["Teacher"] = "Enseignant", ["Employee"] = "Employé", ["Class"] = "Classe",
            ["OK"] = "OK", ["Yes"] = "Oui", ["No"] = "Non", ["Cancel"] = "Annuler", ["Delete"] = "Supprimer", ["Keep it"] = "Conserver",
            ["Something went wrong"] = "Un problème est survenu", ["Check this first"] = "À vérifier d'abord", ["Please confirm"] = "Veuillez confirmer",
            ["year"] = "an", ["years"] = "ans", ["month"] = "mois", ["months"] = "mois",

            // ---- sign in / loading
            ["EduTrack - Sign in"] = "EduTrack - Connexion",
            ["One register for your whole school."] = "Un seul registre pour toute votre école.",
            ["Students, teachers, staff and classes, kept together and easy to find."] = "Étudiants, enseignants, personnel et classes, réunis et faciles à retrouver.",
            ["Sign in"] = "Connexion", ["Use your administrator account to open the register."] = "Utilisez votre compte administrateur pour ouvrir le registre.",
            ["Signed in last time"] = "Connecté la dernière fois", ["Username"] = "Nom d'utilisateur", ["Password"] = "Mot de passe",
            ["Confirm password"] = "Confirmer le mot de passe", ["At least 6 characters."] = "Au moins 6 caractères.",
            ["Not you? Use another account"] = "Pas vous ? Utiliser un autre compte", ["First time here?"] = "Première visite ?", ["Create an account"] = "Créer un compte",
            ["Connecting to the database"] = "Connexion à la base de données", ["Starting the database"] = "Démarrage de la base de données",
            ["Getting things ready"] = "Préparation en cours", ["Ready"] = "Prêt", ["Can't reach the database"] = "Base de données inaccessible",
            ["Try again"] = "Réessayer", ["Open anyway"] = "Ouvrir quand même",
            ["No accounts exist yet. Create the first administrator to get started."] = "Aucun compte n'existe encore. Créez le premier administrateur pour commencer.",
            ["Welcome back"] = "Bon retour", ["Enter your password to continue."] = "Saisissez votre mot de passe pour continuer.",
            ["Enter your username."] = "Saisissez votre nom d'utilisateur.", ["Enter your password."] = "Saisissez votre mot de passe.",
            ["Use at least 6 characters for the password."] = "Utilisez au moins 6 caractères pour le mot de passe.",
            ["The two passwords don't match."] = "Les deux mots de passe ne correspondent pas.",
            ["That username and password don't match. Check them and try again."] = "Ce nom d'utilisateur et ce mot de passe ne correspondent pas. Vérifiez-les et réessayez.",
            ["Can't reach the database. Make sure MySQL is running, then try again."] = "Base de données inaccessible. Vérifiez que MySQL est démarré, puis réessayez.",
            ["EduTrack is already running."] = "EduTrack est déjà en cours d'exécution.",
            ["Unexpected error"] = "Erreur inattendue",
            ["Open EduTrack"] = "Ouvrir EduTrack", ["Exit EduTrack"] = "Quitter EduTrack",

            // ---- home
            ["Good morning"] = "Bonjour", ["Good afternoon"] = "Bon après-midi", ["Good evening"] = "Bonsoir",
            ["Here is what the register holds right now."] = "Voici ce que contient le registre en ce moment.",
            ["Welcome. Let's set up your school."] = "Bienvenue. Configurons votre école.",
            ["New class"] = "Nouvelle classe", ["New teacher"] = "Nouvel enseignant", ["New student"] = "Nouvel étudiant", ["New employee"] = "Nouvel employé",
            ["Your register is empty. Start here."] = "Votre registre est vide. Commencez ici.",
            ["Set things up in this order and everything else falls into place."] = "Suivez cet ordre et le reste se mettra en place.",
            ["Create a class"] = "Créer une classe", ["A class is the group students and teachers belong to."] = "Une classe est le groupe auquel appartiennent étudiants et enseignants.",
            ["Add your teachers"] = "Ajoutez vos enseignants", ["Contracts, modules and documents live on each teacher's page."] = "Contrats, modules et documents se trouvent sur la page de chaque enseignant.",
            ["Enrol your students"] = "Inscrivez vos étudiants", ["Pick a class for each student while you add them."] = "Choisissez une classe pour chaque étudiant en l'ajoutant.",
            ["Recently added students"] = "Étudiants récemment ajoutés", ["See all"] = "Tout voir",
            ["Students you add will be listed here."] = "Les étudiants que vous ajoutez apparaîtront ici.",
            ["Class sizes"] = "Effectifs des classes", ["Classes you create will show how many students they hold."] = "Les classes que vous créez indiqueront leur nombre d'étudiants.",
            ["Students by major"] = "Étudiants par filière", ["Majors appear here once students have one."] = "Les filières apparaissent ici dès que des étudiants en ont une.",
            ["Contracts ending soon"] = "Contrats bientôt terminés", ["Teachers and employees, next 90 days"] = "Enseignants et employés, 90 prochains jours",
            ["No contract ends in the next 90 days."] = "Aucun contrat ne se termine dans les 90 prochains jours.",
            ["Records to complete"] = "Fiches à compléter", ["Missing a photo or an ID card scan"] = "Photo ou scan de carte d'identité manquant",
            ["Every record has a photo and both ID scans."] = "Chaque fiche a une photo et les deux scans d'identité.",
            ["today"] = "aujourd'hui",

            // ---- list pages
            ["Administrative and support staff, with contracts and salaries."] = "Personnel administratif et de soutien, avec contrats et salaires.",
            ["All employees"] = "Tous les employés", ["Everyone enrolled, with their contact details and documents."] = "Toutes les personnes inscrites, avec leurs coordonnées et documents.",
            ["All students"] = "Tous les étudiants", ["The teaching staff, their modules and contracts."] = "Le corps enseignant, ses modules et ses contrats.",
            ["All teachers"] = "Tous les enseignants", ["All classes"] = "Toutes les classes",
            ["Groups of students: who teaches them, when they meet and how they are graded."] = "Groupes d'étudiants : qui les enseigne, quand ils se réunissent et comment ils sont notés.",
            ["Schedule"] = "Emploi du temps", ["Grades"] = "Notes",
            ["Reload the list"] = "Recharger la liste", ["Open details"] = "Ouvrir la fiche",
            ["Joined"] = "Inscrit le", ["Contract ends"] = "Fin du contrat",
            ["Add the first employee"] = "Ajouter le premier employé", ["Add the first student"] = "Ajouter le premier étudiant", ["Add the first teacher"] = "Ajouter le premier enseignant",
            ["Create the first class"] = "Créer la première classe", ["Choose a class"] = "Choisir une classe",
            ["Pick a class above to see its teachers and students."] = "Choisissez une classe ci-dessus pour voir ses enseignants et ses étudiants.",
            ["No employees yet"] = "Aucun employé pour l'instant", ["Employees you add will show up here, ready to search and open."] = "Les employés que vous ajoutez apparaîtront ici, prêts à être recherchés et ouverts.",
            ["No students yet"] = "Aucun étudiant pour l'instant", ["Students you add will show up here, ready to search and open."] = "Les étudiants que vous ajoutez apparaîtront ici, prêts à être recherchés et ouverts.",
            ["No teachers yet"] = "Aucun enseignant pour l'instant", ["Teachers you add will show up here, ready to search and open."] = "Les enseignants que vous ajoutez apparaîtront ici, prêts à être recherchés et ouverts.",
            ["Check the spelling, or try just a first or last name."] = "Vérifiez l'orthographe, ou essayez seulement un prénom ou un nom.",
            ["Add to this class"] = "Ajouter à cette classe", ["A student"] = "Un étudiant", ["A teacher"] = "Un enseignant", ["Add to class"] = "Ajouter à la classe",
            ["Remove from this class"] = "Retirer de cette classe",
            ["No teacher has been assigned to this class yet."] = "Aucun enseignant n'est encore affecté à cette classe.",
            ["No student is in this class yet. Use the Add to class bar above."] = "Aucun étudiant dans cette classe pour l'instant. Utilisez la barre « Ajouter à la classe » ci-dessus.",
            ["Delete class"] = "Supprimer la classe", ["Class details"] = "Détails de la classe",
            ["A class groups students, teachers, a weekly schedule and grades."] = "Une classe regroupe des étudiants, des enseignants, un emploi du temps hebdomadaire et des notes.",
            ["Class name *"] = "Nom de la classe *", ["Start date"] = "Date de début", ["Description"] = "Description", ["Save class"] = "Enregistrer la classe",
            ["Last class added"] = "Dernière classe ajoutée",
            ["No class has been added yet. The one you save will appear here."] = "Aucune classe n'a encore été ajoutée. Celle que vous enregistrez apparaîtra ici.",
            ["The last class could not be loaded."] = "La dernière classe n'a pas pu être chargée.", ["no date"] = "sans date",

            // ---- grades / schedule
            ["Module"] = "Module", ["Add a module to this class"] = "Ajouter un module à cette classe", ["Add module"] = "Ajouter le module",
            ["Grade"] = "Note", ["No grades to enter yet"] = "Aucune note à saisir pour l'instant",
            ["Choose a class and a module above. The students of that class appear here, ready for their grades."] = "Choisissez une classe et un module ci-dessus. Les étudiants de cette classe apparaissent ici, prêts pour leurs notes.",
            ["Save grades"] = "Enregistrer les notes",
            ["Click a cell and type the subject or room. Tab moves to the next day."] = "Cliquez sur une case et saisissez la matière ou la salle. Tab passe au jour suivant.",
            ["Unsaved changes"] = "Modifications non enregistrées", ["Save schedule"] = "Enregistrer l'emploi du temps", ["Time"] = "Heure",
            ["Monday"] = "Lundi", ["Tuesday"] = "Mardi", ["Wednesday"] = "Mercredi", ["Thursday"] = "Jeudi", ["Friday"] = "Vendredi", ["Saturday"] = "Samedi", ["Sunday"] = "Dimanche",
            ["No classes to schedule yet"] = "Aucune classe à planifier pour l'instant", ["Create a class first, then plan its week here."] = "Créez d'abord une classe, puis planifiez sa semaine ici.",
            ["Use a number from 0 to 20, or a short word such as ABS."] = "Utilisez un nombre de 0 à 20, ou un mot court comme ABS.",
            ["Check the grades"] = "Vérifiez les notes",

            // ---- forms / details
            ["Back to the list"] = "Retour à la liste", ["Back to employees"] = "Retour aux employés", ["Back to students"] = "Retour aux étudiants", ["Back to teachers"] = "Retour aux enseignants",
            ["Employee details"] = "Détails de l'employé", ["Student details"] = "Détails de l'étudiant", ["Teacher details"] = "Détails de l'enseignant",
            ["Edit anything below, then choose Save changes."] = "Modifiez ce que vous voulez ci-dessous, puis choisissez Enregistrer les modifications.",
            ["Choose photo"] = "Choisir photo", ["JPG or PNG"] = "JPG ou PNG",
            ["Personal information"] = "Informations personnelles", ["Fields marked with * are required."] = "Les champs marqués d'un * sont obligatoires.",
            ["First name *"] = "Prénom *", ["Middle name"] = "Deuxième prénom", ["Last name *"] = "Nom *", ["First name"] = "Prénom", ["Last name"] = "Nom",
            ["Gender"] = "Genre", ["Email"] = "E-mail", ["Phone"] = "Téléphone", ["Date of birth"] = "Date de naissance", ["Nationality"] = "Nationalité",
            ["ID card (CIN)"] = "Carte d'identité (CIN)", ["Address"] = "Adresse",
            ["Professional details"] = "Informations professionnelles", ["Organization"] = "Organisation", ["Role"] = "Fonction",
            ["Date of appointment"] = "Date de nomination", ["Experience"] = "Expérience", ["Contract"] = "Contrat", ["Contract start"] = "Début du contrat",
            ["Contract end"] = "Fin du contrat", ["Duration"] = "Durée", ["Salary (MAD per month)"] = "Salaire (MAD par mois)",
            ["Education"] = "Formation", ["Highest degree"] = "Diplôme le plus élevé", ["Graduation year"] = "Année d'obtention", ["University"] = "Université",
            ["Specialization"] = "Spécialisation", ["Module taught"] = "Module enseigné",
            ["Documents"] = "Documents", ["Scans or photos. Open any of them from here later."] = "Scans ou photos. Ouvrez-les d'ici plus tard.",
            ["ID card, front"] = "Carte d'identité, recto", ["ID card, back"] = "Carte d'identité, verso", ["Open the file"] = "Ouvrir le fichier",
            ["Resume"] = "CV", ["Diploma"] = "Diplôme", ["Choose file"] = "Choisir un fichier", ["No file chosen"] = "Aucun fichier choisi",
            ["Delete employee"] = "Supprimer l'employé", ["Delete student"] = "Supprimer l'étudiant", ["Delete teacher"] = "Supprimer l'enseignant",
            ["Save changes"] = "Enregistrer les modifications", ["Save employee"] = "Enregistrer l'employé", ["Save student"] = "Enregistrer l'étudiant", ["Save teacher"] = "Enregistrer l'enseignant",
            ["Enrolment"] = "Inscription", ["Where the student comes from and which class they join."] = "D'où vient l'étudiant et quelle classe il rejoint.",
            ["School or university"] = "École ou université", ["Major"] = "Filière", ["Joining date"] = "Date d'inscription", ["Previous studies."] = "Études précédentes.",
            ["High school or university"] = "Lycée ou université", ["Emergency contact"] = "Contact d'urgence",
            ["Who to call first, usually a parent or guardian."] = "Qui appeler en premier, généralement un parent ou tuteur.",
            ["Relation"] = "Lien de parenté", ["Emergency contact ID card, front"] = "Carte d'identité du contact d'urgence, recto",
            ["Emergency contact ID card, back"] = "Carte d'identité du contact d'urgence, verso", ["Male"] = "Homme", ["Female"] = "Femme", ["Select"] = "Choisir", ["Choose who to add"] = "Choisir qui ajouter", ["Anything worth knowing about this class"] = "Tout ce qui mérite d'être su sur cette classe", ["Calculated from the appointment date"] = "Calculé à partir de la date de nomination", ["Calculated"] = "Calculé", ["Choose a module"] = "Choisir un module", ["Parent, guardian..."] = "Parent, tuteur...", ["e.g. 2A Computer Science"] = "ex. 2A Informatique", ["e.g. Algebra"] = "ex. Algèbre", ["e.g. Computer Science"] = "ex. Informatique", ["Search this class"] = "Rechercher dans cette classe", ["Every teacher is already in this class"] = "Tous les enseignants sont déjà dans cette classe", ["Every student is already in this class"] = "Tous les étudiants sont déjà dans cette classe", ["No major"] = "Aucune filière", ["started"] = "début le", ["Search by first, middle or last name"] = "Rechercher par prénom, deuxième prénom ou nom",

            // ---- document viewer
            ["Previous page"] = "Page précédente", ["Next page"] = "Page suivante", ["Zoom out  (-)"] = "Zoom arrière  (-)", ["Zoom in  (+)"] = "Zoom avant  (+)",
            ["Fit to screen  (0 or double-click)"] = "Ajuster à l'écran  (0 ou double-clic)", ["Close  (Esc)"] = "Fermer  (Échap)", ["Document"] = "Document",

            // ---- settings
            ["How EduTrack looks, and how to keep a copy of your data."] = "L'apparence d'EduTrack, et comment garder une copie de vos données.",
            ["Appearance"] = "Apparence", ["Backup and restore"] = "Sauvegarde et restauration", ["Theme"] = "Thème", ["Language"] = "Langue",
            ["Classic is the original EduTrack look. Paper and Slate are the green alternatives. EduTrack remembers your choice."] = "Classique est l'apparence d'origine d'EduTrack. Papier et Ardoise sont les variantes vertes. EduTrack retient votre choix.",
            ["Classic (original)"] = "Classique (original)", ["Paper (light green)"] = "Papier (vert clair)", ["Slate (dark green)"] = "Ardoise (vert foncé)",
            ["Choose the language of the whole app. EduTrack remembers your choice."] = "Choisissez la langue de toute l'application. EduTrack retient votre choix.",
            ["English"] = "English", ["Français"] = "Français",
            ["Save a copy"] = "Enregistrer une copie", ["Export one list to an XML file you can keep safe or move to another computer."] = "Exportez une liste vers un fichier XML à conserver en lieu sûr ou à déplacer vers un autre ordinateur.",
            ["What to save"] = "Quoi enregistrer", ["Save to file"] = "Enregistrer dans un fichier", ["Show in folder"] = "Afficher dans le dossier",
            ["Restore from a copy"] = "Restaurer depuis une copie", ["Choose a file saved above. Records with the same ID are replaced by the ones in the file."] = "Choisissez un fichier enregistré ci-dessus. Les fiches de même identifiant sont remplacées par celles du fichier.",
            ["Restore data"] = "Restaurer les données",
            ["Choose what to save first."] = "Choisissez d'abord quoi enregistrer.", ["Choose a saved copy"] = "Choisir une copie enregistrée",
            ["Choose a file to restore first."] = "Choisissez d'abord un fichier à restaurer.", ["That file has no records to restore."] = "Ce fichier ne contient aucune fiche à restaurer.",
            ["EduTrack can only restore students, teachers or employees saved with the Save a copy button."] = "EduTrack ne peut restaurer que des étudiants, enseignants ou employés enregistrés avec le bouton « Enregistrer une copie ».",
            ["Couldn't save"] = "Enregistrement impossible", ["Can't restore"] = "Restauration impossible", ["Couldn't restore"] = "Restauration impossible",
            ["Can't read the file"] = "Lecture du fichier impossible", ["Nothing chosen"] = "Rien de choisi", ["Unknown file"] = "Fichier inconnu", ["Nothing to restore"] = "Rien à restaurer",

            // ---- about
            ["About EduTrack"] = "À propos d'EduTrack", ["A desktop register for schools and training centres."] = "Un registre de bureau pour les écoles et centres de formation.",
            ["EduTrack keeps students, teachers, staff and classes in one place. Record who they are and which documents you hold, build each class's weekly schedule, enter grades, and keep an XML copy of your data."] =
                "EduTrack réunit étudiants, enseignants, personnel et classes au même endroit. Enregistrez qui ils sont et quels documents vous détenez, construisez l'emploi du temps hebdomadaire de chaque classe, saisissez les notes et gardez une copie XML de vos données.",
            ["Contact"] = "Contact", ["Scan to visit the website"] = "Scannez pour visiter le site",
            ["Point your phone camera here to open adamrohal.tech. Questions or ideas? Write to the email on the left."] = "Pointez l'appareil photo de votre téléphone ici pour ouvrir adamrohal.tech. Des questions ou des idées ? Écrivez à l'adresse e-mail à gauche.",
            ["Support the developer"] = "Soutenir le développeur",
            ["EduTrack is built and kept up to date by one person. If it saves you time, a coffee on Ko-fi helps keep it free and improving."] = "EduTrack est conçu et tenu à jour par une seule personne. S'il vous fait gagner du temps, un café sur Ko-fi aide à le garder gratuit et à l'améliorer.",
            ["Support on Ko-fi"] = "Soutenir sur Ko-fi",

            // ---- messages
            ["Are you sure you want to delete this employee?"] = "Voulez-vous vraiment supprimer cet employé ?",
            ["Are you sure you want to delete this student?"] = "Voulez-vous vraiment supprimer cet étudiant ?",
            ["Are you sure you want to delete this Teacher?"] = "Voulez-vous vraiment supprimer cet enseignant ?",
            ["Employee has been deleted successfully."] = "L'employé a été supprimé.", ["Student has been deleted successfully."] = "L'étudiant a été supprimé.", ["Teacher has been deleted successfully."] = "L'enseignant a été supprimé.",
            ["Employee information updated successfully!"] = "Informations de l'employé mises à jour !", ["Student information updated successfully!"] = "Informations de l'étudiant mises à jour !", ["Teacher information updated successfully!"] = "Informations de l'enseignant mises à jour !",
            ["Failed to update employee information!"] = "Échec de la mise à jour des informations de l'employé !", ["Failed to update student information!"] = "Échec de la mise à jour des informations de l'étudiant !", ["Failed to update teacher information!"] = "Échec de la mise à jour des informations de l'enseignant !",
            ["Employee information saved successfully!"] = "Informations de l'employé enregistrées !", ["Student information saved successfully!"] = "Informations de l'étudiant enregistrées !", ["Teacher information saved successfully!"] = "Informations de l'enseignant enregistrées !",
            ["Failed to save employee information!"] = "Échec de l'enregistrement des informations de l'employé !", ["Failed to save student information!"] = "Échec de l'enregistrement des informations de l'étudiant !", ["Failed to save teacher information!"] = "Échec de l'enregistrement des informations de l'enseignant !",
            ["Please select a class."] = "Veuillez choisir une classe.", ["Give the class a name."] = "Donnez un nom à la classe.",
            ["Class saved successfully."] = "Classe enregistrée.", ["The class could not be saved."] = "La classe n'a pas pu être enregistrée.",
            ["The class was deleted."] = "La classe a été supprimée.", ["Module added successfully."] = "Module ajouté.", ["Grades saved successfully."] = "Notes enregistrées.",
            ["Choose a class and type the name of the module first."] = "Choisissez d'abord une classe et saisissez le nom du module.",
            ["Choose a module first."] = "Choisissez d'abord un module.", ["Choose a class first."] = "Choisissez d'abord une classe.", ["Choose who to add first."] = "Choisissez d'abord qui ajouter.",
            ["Choose a file first."] = "Choisissez d'abord un fichier.",
            ["The classes could not be loaded. Check that the database is running."] = "Les classes n'ont pas pu être chargées. Vérifiez que la base de données est démarrée.",
            ["The modules could not be loaded."] = "Les modules n'ont pas pu être chargés.", ["The students could not be loaded."] = "Les étudiants n'ont pas pu être chargés.",
            ["The module could not be added."] = "Le module n'a pas pu être ajouté.", ["The grades could not be saved."] = "Les notes n'ont pas pu être enregistrées.",
            ["The schedule could not be loaded."] = "L'emploi du temps n'a pas pu être chargé.", ["The schedule could not be saved."] = "L'emploi du temps n'a pas pu être enregistré.",
            ["The teachers of this class could not be loaded."] = "Les enseignants de cette classe n'ont pas pu être chargés.", ["The students of this class could not be loaded."] = "Les étudiants de cette classe n'ont pas pu être chargés.",
            ["The list of people could not be loaded."] = "La liste des personnes n'a pas pu être chargée.", ["They could not be added to the class."] = "Ils n'ont pas pu être ajoutés à la classe.",
            ["That change could not be saved."] = "Cette modification n'a pas pu être enregistrée.", ["The class could not be deleted."] = "La classe n'a pas pu être supprimée.",
            ["The employee list could not be loaded. Check that the database is running."] = "La liste des employés n'a pas pu être chargée. Vérifiez que la base de données est démarrée.",
            ["The student list could not be loaded. Check that the database is running."] = "La liste des étudiants n'a pas pu être chargée. Vérifiez que la base de données est démarrée.",
            ["The teacher list could not be loaded. Check that the database is running."] = "La liste des enseignants n'a pas pu être chargée. Vérifiez que la base de données est démarrée.",
            ["The file could not be saved."] = "Le fichier n'a pas pu être enregistré.", ["That file isn't a readable EduTrack copy."] = "Ce fichier n'est pas une copie EduTrack lisible.",
            ["Nothing was changed. The restore stopped because:"] = "Rien n'a été modifié. La restauration s'est arrêtée car :",
            ["This file could not be opened."] = "Ce fichier n'a pas pu être ouvert.", ["This page could not be shown."] = "Cette page n'a pas pu être affichée.",
            ["EduTrack hit a problem it could not recover from:"] = "EduTrack a rencontré un problème dont il n'a pas pu se remettre :",
            ["An error occurred: "] = "Une erreur s'est produite : ",
            ["Can't load classes"] = "Chargement des classes impossible", ["Can't load employees"] = "Chargement des employés impossible", ["Can't load modules"] = "Chargement des modules impossible",
            ["Can't load people"] = "Chargement des personnes impossible", ["Can't load schedule"] = "Chargement de l'emploi du temps impossible", ["Can't load students"] = "Chargement des étudiants impossible",
            ["Can't load teachers"] = "Chargement des enseignants impossible", ["Confirmation"] = "Confirmation", ["Couldn't add"] = "Ajout impossible", ["Couldn't delete"] = "Suppression impossible",
            ["Error"] = "Erreur", ["Information"] = "Information", ["Module not added"] = "Module non ajouté", ["Move student"] = "Déplacer l'étudiant", ["No class chosen"] = "Aucune classe choisie",
            ["No module chosen"] = "Aucun module choisi", ["Remove from class"] = "Retirer de la classe",
        };
        /// <summary>Messages built from pieces: the pattern is matched against the finished English text.</summary>
        private static readonly List<Pat> FrPats = new List<Pat>();

        private static string Noun(string english)
        {
            switch ((english ?? "").Trim().ToLowerInvariant().Replace("_registry", ""))
            {
                case "student": case "students": return "étudiants";
                case "teacher": case "teachers": return "enseignants";
                case "employee": case "employees": return "employés";
                default: return english;
            }
        }

        private static void InitFr()
        {
            Dictionaries[French] = Fr;
            Patterns[French] = FrPats;
            P(FrPats, @"^(\d+) (student|teacher|employee)s?$", m => m.Groups[1].Value + " " + (m.Groups[2].Value == "student" ? "étudiant" : m.Groups[2].Value == "teacher" ? "enseignant" : "employé") + (m.Groups[1].Value == "1" ? "" : "s"));
            P(FrPats, @"^(\d+) matches?$", m => m.Groups[1].Value + (m.Groups[1].Value == "1" ? " résultat" : " résultats"));
            P(FrPats, @"^Version (.*)$", m => "Version " + m.Groups[1].Value);
            P(FrPats, @"^Save (students|teachers|employees)$", m => "Enregistrer les " + Noun(m.Groups[1].Value));
            P(FrPats, @"^Restore (\d+) records into (\w+)s\? Records with the same ID will be replaced\.$",
                m => "Restaurer " + m.Groups[1].Value + " fiches dans les " + Noun(m.Groups[2].Value) + " ? Les fiches de même identifiant seront remplacées.");
            P(FrPats, @"^None of the columns in that file match the (.*) table\.$", m => "Aucune colonne de ce fichier ne correspond à la table " + Noun(m.Groups[1].Value) + ".");
            P(FrPats, @"^The grade for (.*) isn't valid\. (.*)$", m => "La note de " + m.Groups[1].Value + " n'est pas valide. " + T(m.Groups[2].Value));
            P(FrPats, @"^You have unsaved changes to (.*)\. Switch class and lose them\?$", m => "Vous avez des modifications non enregistrées pour " + m.Groups[1].Value + ". Changer de classe et les perdre ?");
            P(FrPats, @"^Schedule saved for (.*)\.$", m => "Emploi du temps enregistré pour " + m.Groups[1].Value + ".");
            P(FrPats, @"^Teachers  \((\d+)\)$", m => "Enseignants  (" + m.Groups[1].Value + ")");
            P(FrPats, @"^Students  \((\d+) found\)$", m => "Étudiants  (" + m.Groups[1].Value + " trouvés)");
            P(FrPats, @"^Students  \((\d+)\)$", m => "Étudiants  (" + m.Groups[1].Value + ")");
            P(FrPats, @"^Remove (.*) from (.*)\? Their (student|teacher) record is kept\.$",
                m => "Retirer " + m.Groups[1].Value + " de " + m.Groups[2].Value + " ? " + (m.Groups[3].Value == "student" ? "Sa fiche d'étudiant est conservée." : "Sa fiche d'enseignant est conservée."));
            P(FrPats, @"^Delete the class “(.*)”\? Its schedule, modules and grades are deleted too\. (.*)$",
                m => "Supprimer la classe « " + m.Groups[1].Value + " » ? Son emploi du temps, ses modules et ses notes seront aussi supprimés. " + T(m.Groups[2].Value));
            P(FrPats, @"^No (employee|student|teacher) matches “(.*)”$", m => "Aucun " + (m.Groups[1].Value == "employee" ? "employé" : m.Groups[1].Value == "student" ? "étudiant" : "enseignant") + " ne correspond à « " + m.Groups[2].Value + " »");
            P(FrPats, @"^created (.*)$", m => "créée le " + m.Groups[1].Value);
            P(FrPats, @"^ended (\d+) days? ago$", m => "terminé il y a " + m.Groups[1].Value + (m.Groups[1].Value == "1" ? " jour" : " jours"));
            P(FrPats, @"^in (\d+) days?$", m => "dans " + m.Groups[1].Value + (m.Groups[1].Value == "1" ? " jour" : " jours"));
            P(FrPats, @"^(Good morning|Good afternoon|Good evening), (.*)$", m => T(m.Groups[1].Value) + ", " + m.Groups[2].Value);
            P(FrPats, @"^(Teacher|Employee) · (.*)$", m => T(m.Groups[1].Value) + " · " + m.Groups[2].Value);
            P(FrPats, @"^(Error [a-z ]+|Error): (.*)$", m => "Erreur : " + m.Groups[2].Value);
        }
    }
}
