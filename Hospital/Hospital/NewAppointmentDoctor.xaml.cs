using Model;
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

namespace Hospital
{
    public partial class NewAppointmentDoctor : Window
    {
        private PatientStorage patientStorage = new PatientStorage();
        private AppointmentStorage appointmentStorage = new AppointmentStorage();


        public NewAppointmentDoctor()
        {
            InitializeComponent();
        }

        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = new DateTime();
            pickedDate = appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            Patient patient = patientStorage.GetByJmbg(patientJmbg.Text);

            User doctorUser = new User("12345678910", "Djordje", "Tovilovc", "djole", "sifra", "email", "adresa", DateTime.Now);
            Doctor doctor = new Doctor(doctorUser);

            Room room = new Room(1, "336", 0, 3, "detalji");

            doctor.Room = room;

            Appointment appointment = new Appointment(id, appointmentDateTime, duration, patient, doctor);
            appointmentStorage.Save(appointment);

        }
    }
}
