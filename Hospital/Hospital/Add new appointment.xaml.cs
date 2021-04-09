using Hospital.Model;
using Model;
using System;
using System.Collections.Generic;
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
