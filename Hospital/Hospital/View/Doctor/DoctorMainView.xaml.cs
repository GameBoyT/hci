using Hospital.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.View.Doctor
{
    public partial class DoctorMainView : Page
    {
        public DoctorMainView()
        {
            InitializeComponent();
            DataContext = new DoctorWindowViewModel();
        }

        private void UpdateAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentViewModel avm = (AppointmentViewModel)appointmentsDataGrid.SelectedItem;
            UpdateExaminationViewModel vm = new UpdateExaminationViewModel(NavigationService, avm);
            UpdateExaminationView updateExaminationView = new UpdateExaminationView(vm);
            NavigationService.Navigate(updateExaminationView);
        }

        private void ViewAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentViewModel avm = (AppointmentViewModel)appointmentsDataGrid.SelectedItem;
            UpdateExaminationViewModel vm = new UpdateExaminationViewModel(NavigationService, avm);
            UpdateExaminationView updateExaminationView = new UpdateExaminationView(vm);
            NavigationService.Navigate(updateExaminationView);
        }
    }
}
