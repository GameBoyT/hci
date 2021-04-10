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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Appointment app = (Appointment)appointmentsDataGrid.SelectedItems[0];
            appointmentStorage.Delete(app.Id);
            appointments = appointmentStorage.GetAppointmentsForDoctor("12345678910");
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var newAppointmentWindow = new NewAppointmentDoctor();
            newAppointmentWindow.Show();
            //App.Current.MainWindow.Hide();
        }
    }
}
