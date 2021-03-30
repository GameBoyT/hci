using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Hospital.Model;

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
            storage.deleteAppointment(app);
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
            storage.createAppointment(app);
            lvDataBinding.Items.Refresh();
        }

       
       
    }
}
