﻿using Hospital.Commands;
using Hospital.View.Doctor;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class DoctorMainWindowViewModel : ViewModel
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
            ExaminationViewModel vm = new ExaminationViewModel(NavService);
            DoctorExaminationView addStudentPage = new DoctorExaminationView(vm);
            NavService.Navigate(addStudentPage);
        }
        #endregion

        public DoctorMainWindowViewModel(NavigationService navService)
        {
            NavigateToExamination = new RelayCommand(Execute_NavigateToExamination, CanExecute_NavigateCommand);
            NavService = navService;
        }
    }
}
