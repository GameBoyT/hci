using Hospital.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hospital.View.Doctor
{
    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
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
            DoctorViewPatient doctorViewPatient = new DoctorViewPatient(avm);
            doctorViewPatient.Show();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V)
            {
                ViewAppointmentBtn_Click(sender, e);
            }
            if (e.Key == Key.U)
            {
                UpdateAppointmentBtn_Click(sender, e);
            }
            if (e.Key == Key.D)
            {
                Keyboard.Focus(appointment_date);
            }
        }
    }
}
