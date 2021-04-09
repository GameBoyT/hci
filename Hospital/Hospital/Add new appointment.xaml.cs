using Model;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for Add_new_appointment.xaml
    /// </summary>
    public partial class Add_new_appointment : Window
    {
        public Add_new_appointment()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment appoinetment = new Appointment(idTextBox.Text, timeStartTextBox.Text, durationTextBox.Text);
            var pac = new Pacijent();
            pac.createAppointment(appoinetment);

            MessageBox.Show("Termin dodaje u fajl samo iz nekog razloga nece da refreshuje kao kod brisanja", "Uspjesno");



        }
    }
}
