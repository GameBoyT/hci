using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
