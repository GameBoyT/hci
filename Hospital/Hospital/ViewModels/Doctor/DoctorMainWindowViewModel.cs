using Hospital.Commands;
using Hospital.View;
using Hospital.View.Doctor;
using Hospital.ViewModels.Doctor;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class DoctorMainWindowViewModel : ViewModel
    {
        #region Polja

        public NavigationService NavService { get; set; }
        #endregion

        #region Komande

        public RelayCommand NavigateBack { get; set; }

        public RelayCommand NavigateToMainView { get; set; }

        public RelayCommand NavigateToNewExamination { get; set; }

        public RelayCommand NavigateToNewOperation { get; set; }

        public RelayCommand NavigateToMedicine { get; set; }

        public RelayCommand NavigateToShortcuts { get; set; }

        public RelayCommand LogoutCommand { get; set; }
        #endregion

        #region Akcije
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private bool CanExecute_NavigateBack(object obj)
        {
            return NavService.CanGoBack;
        }


        private void Execute_NavigateBack(object obj)
        {
            if (NavService.CanGoBack)
                NavService.GoBack();
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
            MedicinePageViewModel vm = new MedicinePageViewModel(NavService);
            MedicineView view = new MedicineView(vm);
            NavService.Navigate(view);
        }

        private void Execute_NavigateToShortcuts(object obj)
        {
            ShortcutsView view = new ShortcutsView();
            NavService.Navigate(view);
        }

        private void Execute_LogoutCommand(object obj)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

        #endregion

    public DoctorMainWindowViewModel(NavigationService navService)
        {
            NavService = navService;
            NavigateBack = new RelayCommand(Execute_NavigateBack, CanExecute_NavigateBack);
            NavigateToMainView = new RelayCommand(Execute_NavigateToMainView, CanExecute_NavigateCommand);
            NavigateToNewExamination = new RelayCommand(Execute_NavigateToNewExamination, CanExecute_NavigateCommand);
            NavigateToNewOperation = new RelayCommand(Execute_NavigateToNewOperation, CanExecute_NavigateCommand);
            NavigateToMedicine = new RelayCommand(Execute_NavigateToMedicine, CanExecute_NavigateCommand);
            NavigateToShortcuts = new RelayCommand(Execute_NavigateToShortcuts, CanExecute_NavigateCommand);
            LogoutCommand = new RelayCommand(Execute_LogoutCommand, CanExecute_NavigateCommand);
        }
    }
}
