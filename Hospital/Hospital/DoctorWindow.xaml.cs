using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hospital
{
    public partial class DoctorWindow : Window
    {
        App app;
        List<Appointment> appointments = new List<Appointment>();
        List<Appointment> appointmentsToShow = new List<Appointment>();
        List<Room> roomsToShow = new List<Room>();
        private Doctor Doctor;
        private AppointmentType appointmentType;

        public DoctorWindow()
        {
            InitializeComponent();
            app = Application.Current as App;


            // DATA TO GENERATE

            //DateTime date = new DateTime(1985, 4, 26);
            //User doctorUser = new User("1", "Djordje", "Tovilovic", "djoleusername", "djolesifra", "djoleemail", "djoleadresa", date);
            //Doctor doctor = new Doctor(doctorUser);
            //doctorController.Save(doctor);

            //DateTime date1 = new DateTime(1985, 4, 26);
            //User user = new User("2", "Nemanja", "Markovic", "nemanja", "sifra", "email", "adresa", date1);
            //Doctor doctor2 = new Doctor(user);
            //doctorController.Save(doctor2);

            //Doctor doctor1 = doctorController.GetByJmbg("1");
            //roomController.Save("336", RoomType.exam, 3, "detalji");
            //doctor1.Room = roomController.GetByName("336");
            //doctorController.Update(doctor1);

            //Doctor doctor2 = doctorController.GetByJmbg("2");
            //doctor2.Room = roomController.GetByName("7");
            //roomController.Save("7", RoomType.exam, 0, "detaljiSoba7");
            //doctorController.Update(doctor2);


            //DateTime date2 = new DateTime(1968, 2, 5);
            //DateTime date3 = new DateTime(1977, 11, 4);
            //User user2 = new User("10", "Mile", "Milic", "mile", "sifra", "email", "adresa", date2);
            //User user3 = new User("11", "Simo", "Simic", "simo", "sifra", "email", "adresa", date3);
            //Patient patient2 = new Patient(user2);
            //Patient patient3 = new Patient(user3);
            //app.patientController.Save(patient2);
            //app.patientController.Save(patient3);

            //roomController.Save("10", RoomType.exam, 2, "Rendgen soba");

            //staticEquipmentController.Save("Rendgen", "10", 1, "Rendgen masina");
            //List<StaticEquipment> roomsWitheRendgen = staticEquipmentController.GetAllRoomsWithEquipmentName("rendgen");
            //MessageBox.Show(roomsWitheRendgen[0].Name);


            Doctor = app.doctorController.GetByJmbg("1");
            appointment_date.SelectedDate = DateTime.Today;
            new_appointment_date.SelectedDate = DateTime.Today;
            appointmentType = AppointmentType.examination;
            rooms.IsEnabled = false;
            WindowUpdate();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = (Appointment)appointmentsDataGrid.SelectedItems[0];

                addNewAppointmentButton.Visibility = Visibility.Collapsed;
                updateAppointmentButton.Visibility = Visibility.Visible;
                cancelUpdateButton.Visibility = Visibility.Visible;
                title.Content = "Edit appointment";

                appointmentType = appointment.AppointmentType;
                new_appointment_date.SelectedDate = appointment.StartTime.Date;
                durationTextBox.Text = appointment.DurationInMinutes.ToString();
                startTimeTextBox.Text = appointment.StartTime.ToString("HH:mm");
                patientJmbg.Text = appointment.Patient.User.Jmbg;

                patientJmbg.IsReadOnly = true;
                patientJmbg.IsEnabled = false;
                examinationRadioButton.IsEnabled = false;
                operationRadioButton.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to update!");
            }

        }

        private void WindowUpdate()
        {
            appointments = app.appointmentController.GetAppointmentsForDoctor(Doctor.User.Jmbg);
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
            rooms.ItemsSource = roomsToShow;
            rooms.SelectedIndex = 0;
        }

        private Appointment CreateAppointmentFromData()
        {
            int id = app.appointmentController.GenerateNewId();
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            Patient patient = app.patientController.GetByJmbg(patientJmbg.Text);
            if (appointmentType == AppointmentType.examination)
            {
                return new Appointment(id, appointmentType, appointmentDateTime, duration, patient, Doctor, Doctor.Room);
            }
            else
            {
                return new Appointment(id, appointmentType, appointmentDateTime, duration, patient, Doctor, (Room)rooms.SelectedItem);
            }
        }

        private void Update_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = CreateAppointmentFromData();
                if (app.appointmentController.AppointmentTimeIsInvalid(appointment))
                    return;
                app.appointmentController.Update(appointment);
                WindowUpdate();
                ChangeToNewAppointment();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void Cancel_Update_Click(object sender, RoutedEventArgs e)
        {
            ChangeToNewAppointment();
        }

        private void ChangeToNewAppointment()
        {
            new_appointment_date.SelectedDate = DateTime.Today;
            durationTextBox.Clear();
            startTimeTextBox.Clear();
            patientJmbg.Clear();

            addNewAppointmentButton.Visibility = Visibility.Visible;
            updateAppointmentButton.Visibility = Visibility.Collapsed;
            cancelUpdateButton.Visibility = Visibility.Collapsed;

            title.Content = "New appointment";

            patientJmbg.IsReadOnly = false;
            patientJmbg.IsEnabled = true;
            examinationRadioButton.IsEnabled = true;
            operationRadioButton.IsEnabled = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = (Appointment)appointmentsDataGrid.SelectedItems[0];
                app.appointmentController.Delete(appointment.Id);
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to delete!");
            }
        }


        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = CreateAppointmentFromData();
                if (appointment.Patient == null)
                {
                    MessageBox.Show("You have to enter a valid patient jmbg!");
                    return;
                }

                //TODO: treba refaktorisati AppointmentTimeIsInvalid da moze izbaciti error za svaku gresku posebno
                if (app.appointmentController.AppointmentTimeIsInvalid(appointment))
                {
                    MessageBox.Show("Appointment time is invalid!");
                    return;

                }

                app.appointmentController.Save(appointment);
                ChangeToNewAppointment();
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void TypeButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            if (radioButton.Content.ToString() == "Examination")
            {
                appointmentType = AppointmentType.examination;
                roomsToShow.Clear();
                rooms.SelectedIndex = -1;
                roomsToShow.Add(Doctor.Room);
                rooms.IsEnabled = false;
                equipmentName.IsEnabled = false;
                findButton.IsEnabled = false;
                WindowUpdate();
            }
            else
            {
                appointmentType = AppointmentType.operation;
                roomsToShow.Clear();
                rooms.SelectedIndex = -1;
                rooms.IsEnabled = true;
                equipmentName.IsEnabled = true;
                findButton.IsEnabled = true;
                WindowUpdate();
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            roomsToShow = app.staticEquipmentController.GetAllRoomsWithEquipmentName(equipmentName.Text);
            WindowUpdate();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)appointmentsDataGrid.SelectedItems[0];
            DoctorViewPatient doctorViewPatientWindow = new DoctorViewPatient(appointment);
            doctorViewPatientWindow.Show();
            this.Close();
        }
    }
}
