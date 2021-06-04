using Hospital.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class DoctorMainView : Page
    {
        public DoctorMainView()
        {
            InitializeComponent();
            this.DataContext = new DoctorWindowViewModel();

        }

        private void AddStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            ExaminationViewModel vm = new ExaminationViewModel(this.NavigationService);
            DoctorExaminationView addStudentPage = new DoctorExaminationView(vm);
            this.NavigationService.Navigate(addStudentPage);
        }
    }
}
