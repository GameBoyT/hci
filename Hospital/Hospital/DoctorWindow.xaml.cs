using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hospital
{
    public partial class DoctorWindow : Window
    {
        private AppointmentStorage appointmentStorage = new AppointmentStorage();
        private PatientStorage patientStorage = new PatientStorage();
        private DoctorStorage doctorStorage = new DoctorStorage();
        List<Appointment> appointments = new List<Appointment>();
        List<Appointment> appointmentsToShow = new List<Appointment>();
        private Doctor Doctor;
        public DoctorWindow()
        {
            InitializeComponent();
            Doctor = doctorStorage.GetByJmbg("1");
            appointment_date.SelectedDate = DateTime.Today;
            new_appointment_date.SelectedDate = DateTime.Today;
            WindowUpdate();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            addNewAppointmentButton.Visibility = Visibility.Collapsed;
            updateAppointmentButton.Visibility = Visibility.Visible;
            cancelUpdateButton.Visibility = Visibility.Visible;
            title.Content = "Edit appointment";

            Appointment appointment = (Appointment)appointmentsDataGrid.SelectedItems[0];
            idTextBox.Text = appointment.Id.ToString();
            new_appointment_date.SelectedDate = appointment.StartTime.Date;
            durationTextBox.Text = appointment.DurationInMinutes.ToString();
            startTimeTextBox.Text = appointment.StartTime.ToString("HH:mm");
            patientJmbg.Text = appointment.Patient.User.Jmbg;

            idTextBox.IsReadOnly = true;
            idTextBox.IsEnabled = false;
            patientJmbg.IsReadOnly = true;
            patientJmbg.IsEnabled = false;
        }

        private void WindowUpdate()
        {
            appointments = appointmentStorage.GetAppointmentsForDoctor(Doctor.User.Jmbg);
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
        }

        private Appointment CreateAppointmentFromData()
        {
            int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            Patient patient = patientStorage.GetByJmbg(patientJmbg.Text);
            return new Appointment(id, appointmentDateTime, duration, patient, Doctor);
        }

        private void Update_Appointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = CreateAppointmentFromData();
            appointmentStorage.Update(appointment);
            WindowUpdate();
            ChangeToNewAppointment();
        }

        private void Cancel_Update_Click(object sender, RoutedEventArgs e)
        {
            ChangeToNewAppointment();
        }

        private void ChangeToNewAppointment()
        {
            idTextBox.Clear();
            new_appointment_date.SelectedDate = DateTime.Today;
            durationTextBox.Clear();
            startTimeTextBox.Clear();
            patientJmbg.Clear();

            addNewAppointmentButton.Visibility = Visibility.Visible;
            updateAppointmentButton.Visibility = Visibility.Collapsed;
            cancelUpdateButton.Visibility = Visibility.Collapsed;

            title.Content = "New appointment";

            idTextBox.IsReadOnly = false;
            idTextBox.IsEnabled = true;
            patientJmbg.IsReadOnly = false;
            patientJmbg.IsEnabled = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Appointment app = (Appointment)appointmentsDataGrid.SelectedItems[0];
            appointmentStorage.Delete(app.Id);
            WindowUpdate();
        }

        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = CreateAppointmentFromData();
            appointmentStorage.Save(appointment);
            WindowUpdate();
        }
    }
}
