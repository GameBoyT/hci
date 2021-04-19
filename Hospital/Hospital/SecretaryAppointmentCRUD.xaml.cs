using Controller;
using Model;
using System.Collections.Generic;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for SecretaryAppointmentCRUD.xaml
    /// </summary>
    public partial class SecretaryAppointmentCRUD : Window
    {
        List<Appointment> appointments = new List<Appointment>();
        private AppointmentController appointmentController = new AppointmentController();

        public SecretaryAppointmentCRUD()
        {
            InitializeComponent();
            appointments = appointmentController.GetAll();
            secretaryAppointmentDataGrid.ItemsSource = appointments;
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment app = (Appointment)secretaryAppointmentDataGrid.SelectedItems[0];
                appointmentController.Delete(app.Id);
                //WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to delete!");
            }
        }


    }
}
