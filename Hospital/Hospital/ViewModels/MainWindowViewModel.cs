using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using System.Windows;
using Hospital.Commands;
using Hospital.View.Doctor;

namespace Hospital.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Polja

        public NavigationService NavService { get; set; }
        #endregion

        #region Komande
        public RelayCommand NavigateToExamination { get; set; }

        #endregion

        #region Akcije
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToExamination(object obj)
        {
            ExaminationViewModel vm = new ExaminationViewModel(this.NavService);
            DoctorExaminationView addStudentPage = new DoctorExaminationView(vm);
            this.NavService.Navigate(addStudentPage);
        }

        #endregion

        public MainWindowViewModel(NavigationService navService)
        {
            NavigateToExamination = new RelayCommand(Execute_NavigateToExamination, CanExecute_NavigateCommand);
            NavService = navService;
        }
    }
}
