using Hospital.Model;
using Model;
using System.Collections.Generic;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for Pacijent.xaml
    /// </summary>
    public partial class Pacijent : Window
    {

        Appointment app = new Appointment("nemanja", "nemanja", "emsad");
        private AppointmentStorage storage = new AppointmentStorage();
        List<Appointment> appoinemnts = new List<Appointment>();
        public Pacijent()
        {
            InitializeComponent();
            appoinemnts = storage.GetAll();
            lvDataBinding.Items.Clear();
            lvDataBinding.ItemsSource = appoinemnts;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment app = (Appointment)lvDataBinding.SelectedItems[0];
            storage.DeleteAppointment(app);
            lvDataBinding.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var pacijent_win = new Pacijent();
            var add_new = new Add_new_appointment();
            add_new.Show();
            pacijent_win.Hide();

        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        public void createAppointment(Appointment app)
        {
            storage.CreateAppointment(app);
            lvDataBinding.Items.Refresh();
        }



    }
}
