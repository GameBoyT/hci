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
        List<Appointment> appointments = new List<Appointment>();
        List<Appointment> appointmentsToShow = new List<Appointment>();
        public DoctorWindow()
        {
            InitializeComponent();
            User doctorUser = new User("12345678910", "Djordje", "Tovilovc", "djole", "sifra", "email", "adresa", DateTime.Now);
            Doctor doctor = new Doctor(doctorUser);
            Room room = new Room(1, "336", 0, 3, "detalji");
            doctor.Room = room;
            appointment_date.SelectedDate = DateTime.Today;
            appointments = appointmentStorage.GetAppointmentsForDoctor(doctor.User.Jmbg);

            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;



            //User user = new User("111111111111", "Nemanja", "Markovic", "nemanja", "sifra", "email", "adresa", DateTime.Now);
            //User user2 = new User("222222222222", "Zarko", "Zarkovic", "zarko", "sifra", "email", "adresa", DateTime.Now);
            //User user3 = new User("333333333333", "Pero", "Peric", "pero", "sifra", "email", "adresa", DateTime.Now);
            //Patient patient = new Patient(user);
            //Patient patient2 = new Patient(user2);
            //Patient patient3 = new Patient(user3);
            //patientStorage.Save(patient);
            //patientStorage.Save(patient2);
            //patientStorage.Save(patient3);



            //Appointment newAppointment = new Appointment(1, DateTime.Now, 30, patient, doctor);
            //Appointment newAppointment2 = new Appointment(2, DateTime.Now, 30, patient2, doctor);
            //Appointment newAppointment3 = new Appointment(3, DateTime.Now, 30, patient3, doctor);
            //appointmentStorage.Save(newAppointment);
            //appointmentStorage.Save(newAppointment2);
            //appointmentStorage.Save(newAppointment3);


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


        }

        private void Update_Appointment_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = new DateTime();
            pickedDate = new_appointment_date.SelectedDate.Value;
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
            appointmentStorage.Update(appointment);


            appointments = appointmentStorage.GetAppointmentsForDoctor(doctor.User.Jmbg);

            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;

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
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Appointment app = (Appointment)appointmentsDataGrid.SelectedItems[0];
            appointmentStorage.Delete(app.Id);
            appointments = appointmentStorage.GetAppointmentsForDoctor("12345678910");
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;

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


            appointments = appointmentStorage.GetAppointmentsForDoctor(doctor.User.Jmbg);

            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
        }
    }
}
