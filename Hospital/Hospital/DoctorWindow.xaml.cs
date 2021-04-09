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
using System.Windows.Shapes;
using Hospital.Model;
using Model;

namespace Hospital
{
    public partial class DoctorWindow : Window
    {
        private AppointmentStorage storage = new AppointmentStorage();
        List<Appointment> appointments = new List<Appointment>();
        public DoctorWindow()
        {
            InitializeComponent();
            start_date.SelectedDate = DateTime.Today;
            end_date.SelectedDate = DateTime.Today;

            appointments = storage.GetAll();
            lv_appointments.Items.Clear();
            lv_appointments.ItemsSource = appointments;

        }
    }
}
