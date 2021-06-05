using Hospital.Commands;
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
        public RelayCommand NavigateToMainView { get; set; }

        public RelayCommand NavigateToNewExamination { get; set; }

        public RelayCommand NavigateToNewOperation { get; set; }

        public RelayCommand NavigateToMedicine { get; set; }
        #endregion

        #region Akcije
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToMainView(object obj)
        {
            while (NavService.CanGoBack)
            {
                NavService.GoBack();
            }
        }

        private void Execute_NavigateToNewExamination(object obj)
        {
            NewExaminationViewModel vm = new NewExaminationViewModel(NavService);
            NewExaminationView view = new NewExaminationView(vm);
            NavService.Navigate(view);
        }

        private void Execute_NavigateToNewOperation(object obj)
        {
            NewOperationViewModel vm = new NewOperationViewModel(NavService);
            NewOperationView view = new NewOperationView(vm);
            NavService.Navigate(view);
        }

        private void Execute_NavigateToMedicine(object obj)
        {
            DoctorMedicine doctorMedicine = new DoctorMedicine();
            doctorMedicine.Show();
        }
        #endregion

        public DoctorMainWindowViewModel(NavigationService navService)
        {
            NavService = navService;
            NavigateToMainView = new RelayCommand(Execute_NavigateToMainView, CanExecute_NavigateCommand);
            NavigateToNewExamination = new RelayCommand(Execute_NavigateToNewExamination, CanExecute_NavigateCommand);
            NavigateToNewOperation = new RelayCommand(Execute_NavigateToNewOperation, CanExecute_NavigateCommand);
            NavigateToMedicine = new RelayCommand(Execute_NavigateToMedicine, CanExecute_NavigateCommand);
        }
    }
}
