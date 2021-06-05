using Hospital.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class DoctorMainView : Page
    {
        public DoctorMainView(DoctorWindowViewModel doctorWindowViewModel)
        {
            InitializeComponent();
            DataContext = doctorWindowViewModel;
        }

        //private void EditAppointmentBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    AppointmentViewModel avm = (AppointmentViewModel)appointmentsDataGrid.SelectedItem;
        //    UpdateExaminationViewModel vm = new UpdateExaminationViewModel(NavigationService, avm);
        //    UpdateExaminationView updateExaminationView = new UpdateExaminationView(vm);
        //    NavigationService.Navigate(updateExaminationView);
        //}
    }
}
