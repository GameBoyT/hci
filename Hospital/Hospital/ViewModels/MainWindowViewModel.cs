using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using System.Windows;
using Hospital.Commands;

namespace Hospital.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Polja

        private NavigationService navService;

        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }
        #endregion

        #region Komande
        private RelayCommand navigateToMainPageCommand;

        private RelayCommand navigateToStudentPageCommand;

        private RelayCommand openMenuCommand;

        private RelayCommand navigateToTeacherCommand;

        private RelayCommand navigateToSubjectCommand;

        private RelayCommand switchLanguageCommand;

        public RelayCommand NavigateToMainPageCommand
        {
            get { return navigateToMainPageCommand; }
            set
            {
                navigateToMainPageCommand = value;
            }
        }

        public RelayCommand NavigateToStudentPageCommand
        {
            get { return navigateToStudentPageCommand; }
            set
            {
                navigateToStudentPageCommand = value;
            }
        }

        public RelayCommand NavigateToTeacherPageCommand
        {
            get { return navigateToTeacherCommand; }
            set
            {
                navigateToTeacherCommand = value;
            }
        }

        public RelayCommand NavigateToSubjectPageCommand
        {
            get { return navigateToSubjectCommand; }
            set
            {
                navigateToSubjectCommand = value;
            }
        }

        public RelayCommand OpenMenuCommand
        {
            get { return openMenuCommand; }
            set
            {
                openMenuCommand = value;
            }
        }

        public RelayCommand SwitchLanguageCommand
        {
            get { return switchLanguageCommand; }
            set
            {
                switchLanguageCommand = value;
            }
        }
        #endregion

        #region Akcije
        private void Execute_NavigateToMainPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("Views/Pocetna.xaml", UriKind.Relative));
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToExamination(object obj)
        {
            this.NavService.Navigate(
                new Uri("View/DoctorExaminationView.xaml", UriKind.Relative));
        }

        #endregion

        public MainWindowViewModel(NavigationService navService)
        {
            this.NavigateToMainPageCommand = new RelayCommand(Execute_NavigateToExamination, CanExecute_NavigateCommand);
            
            this.navService = navService;
        }
    }
}
