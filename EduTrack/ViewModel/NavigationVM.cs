using EduTrack.Properties;
using EduTrack.Utilities;
using EduTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EduTrack.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        private string connectedUsername;

        // Constructor
        public NavigationVM()
        {

            // Initialize commands
            HomeCommand = new RelayCommand(Home);
            MembersCommand = new RelayCommand(Members);
            ClassCommand = new RelayCommand(Class);
            TeachersCommand = new RelayCommand(Teachers);
            StudentsCommand = new RelayCommand(Students);
            AboutCommand = new RelayCommand(About);
            SettingsCommand = new RelayCommand(Setting);
            NewEmployeeCommand = new RelayCommand(NewEmployee);
            ShowEmployeesCommand = new RelayCommand(ShowEmployees);
            NewClassCommand = new RelayCommand(NewClass);
            ShowClassesCommand = new RelayCommand(ShowClasses);
            NewStudentCommand = new RelayCommand(NewStudent);
            ShowStudentsCommand = new RelayCommand(ShowStudents);
            BackupCommand = new RelayCommand(Backup);
            ClassesScheduleCommand = new RelayCommand(ClassesSchedule);
            ClassesGradesCommand = new RelayCommand(Grades);

            // Startup Page
            CurrentView = new HomeVM();
        }

        public string ConnectedUsername
        {
            get { return connectedUsername; }
            set
            {
                if (connectedUsername != value)
                {
                    connectedUsername = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetConnectedUsername(string username)
        {
            ConnectedUsername = username;
        }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand MembersCommand { get; set; }
        public ICommand ClassCommand { get; set; }
        public ICommand TeachersCommand { get; set; }
        public ICommand StudentsCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand NewEmployeeCommand { get; set; }
        public ICommand ShowEmployeesCommand { get; set; }
        public ICommand NewClassCommand { get; set; }
        public ICommand ShowClassesCommand { get; set; }
        public ICommand NewStudentCommand { get; set; }
        public ICommand ShowStudentsCommand { get; set; }
        public ICommand BackupCommand { get; set; }
        public ICommand ClassesScheduleCommand { get; set; }
        public ICommand ClassesGradesCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Class(object obj) => CurrentView = new ClassVM();
        private void Members(object obj) => CurrentView = new MembersVM();
        private void Teachers(object obj) => CurrentView = new TeachersVM();
        private void Students(object obj) => CurrentView = new StudentsVM();
        private void About(object obj) => CurrentView = new AboutVM();
        private void Setting(object obj) => CurrentView = new SettingsVM();
        private void NewEmployee(object obj) => CurrentView = new NewEmployeeVM();
        private void ShowEmployees(object obj) => CurrentView = new ShowEmployeesVM();
        private void NewClass(object obj) => CurrentView = new NewClassVM();
        private void ShowClasses(object obj) => CurrentView = new ShowClassesVM();
        private void NewStudent(object obj) => CurrentView = new NewStudentVM();
        private void ShowStudents(object obj) => CurrentView = new ShowStudentsVM();
        private void Backup(object obj) => CurrentView = new BackupVM();
        private void ClassesSchedule(object obj) => CurrentView = new ScheduleClassesVM();
        private void Grades(object obj) => CurrentView = new GradesVM();

    }
}
