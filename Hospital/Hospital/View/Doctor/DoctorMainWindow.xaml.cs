using Hospital.ViewModels;
using System.Windows;

namespace Hospital.View.Doctor
{
    public partial class DoctorMainWindow : Window
    {
        public DoctorMainWindow()
        {
            InitializeComponent();
            this.DataContext = new DoctorMainWindowViewModel(this.frame.NavigationService);
        }
    }
}
