using DTO;
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

namespace Hospital.View.Doctor
{
    public partial class DoctorUpdateExamination : Window
    {
        App app;
        public DoctorWindow ParentWindow { get; set; }
        public AppointmentDTO Appointment { get; set; }
        public Patient Patient { get; set; }

        public DoctorUpdateExamination(DoctorWindow parentWindow, AppointmentDTO appointment)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;

            ParentWindow = parentWindow;
            Appointment = appointment;
            Patient = app.patientController.GetByJmbg(appointment.PatientJmbg);
            new_appointment_date.SelectedDate = appointment.StartTime;
            startTimeTextBox.Text = appointment.StartTime.ToString("HH:mm");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appointment = ParseUpdatedAppointment();
                app.appointmentController.Update(appointment);
                ParentWindow.WindowUpdate();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Fill all the fields!");
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private AppointmentDTO ParseUpdatedAppointment()
        {
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);

            return new AppointmentDTO(Appointment.Id, AppointmentType.examination, appointmentDateTime, 15.0, Appointment.PatientJmbg, Appointment.DoctorJmbg, Appointment.RoomId);
        }
    }
}
