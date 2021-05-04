using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for SecretaryFun.xaml
    /// </summary>
    public partial class SecretaryFun : Window
    {
        public SecretaryFun()
        {
            InitializeComponent();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_PatientCRUD(object sender, RoutedEventArgs e)
        {
            SecretaryPatientCRUD secretaryPatientCRUD = new SecretaryPatientCRUD();
            secretaryPatientCRUD.Show();
            this.Close();
        }

        private void CRUD_Appointment_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAppointmentCRUD secretaryAppointmentCRUD = new SecretaryAppointmentCRUD();
            secretaryAppointmentCRUD.Show();
            this.Close();
        }

        private void Allergens_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAllergens secretaryAllergens = new SecretaryAllergens();
            secretaryAllergens.Show();
            this.Close();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            SecretaryGuest secretaryGuest = new SecretaryGuest();
            secretaryGuest.Show();
            this.Close();
        }

        private void Noticeboard_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNoticeboard secretaryNoticeboard = new SecretaryNoticeboard();
            secretaryNoticeboard.Show();
            this.Close();
        }

        private void Urgent_Click(object sender, RoutedEventArgs e)
        {
            View.Secretary.SecretaryUrgentAppointment secretaryUrgentAppointment = new View.Secretary.SecretaryUrgentAppointment();
            secretaryUrgentAppointment.Show();
            this.Close();
        }
    }
}
