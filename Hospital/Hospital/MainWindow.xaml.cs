using Hospital.View.Doctor;
using Hospital.ViewModels;
using System.Windows;

namespace Hospital
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorMainWindow doctorMainView = new DoctorMainWindow();
            doctorMainView.Show();
            this.Close();
        }
    }
}
