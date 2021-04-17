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
        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private DoctorController doctorController = new DoctorController();
        private RoomController roomController = new RoomController();
        List<Appointment> appointments = new List<Appointment>();
        List<Appointment> appointmentsToShow = new List<Appointment>();
        private Doctor Doctor;
        private AppointmentType appointmentType;

        public DoctorWindow()
        {
            InitializeComponent();
            Doctor = doctorController.GetByJmbg("1");
            //roomController.Save("336", RoomType.exam, 3, "detalji");
            appointment_date.SelectedDate = DateTime.Today;
            new_appointment_date.SelectedDate = DateTime.Today;
            appointmentType = AppointmentType.examination;
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

                new_appointment_date.SelectedDate = appointment.StartTime.Date;
                durationTextBox.Text = appointment.DurationInMinutes.ToString();
                startTimeTextBox.Text = appointment.StartTime.ToString("HH:mm");
                patientJmbg.Text = appointment.Patient.User.Jmbg;

                patientJmbg.IsReadOnly = true;
                patientJmbg.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to update!");
            }

        }

        private void WindowUpdate()
        {
            appointments = appointmentController.GetAppointmentsForDoctor(Doctor.User.Jmbg);
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
        }

        private Appointment CreateAppointmentFromData()
        {
            int id = appointmentController.GenerateNewId();
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            Patient patient = patientController.GetByJmbg(patientJmbg.Text);
            Room room = roomController.GetByName("336");
            return new Appointment(id, appointmentType, appointmentDateTime, duration, patient, Doctor, room);
        }

        private void Update_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = CreateAppointmentFromData();
                if (appointmentController.AppointmentTimeIsInvalid(appointment))
                    return;
                appointmentController.Update(appointment);
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
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment app = (Appointment)appointmentsDataGrid.SelectedItems[0];
                appointmentController.Delete(app.Id);
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
                if (appointmentController.AppointmentTimeIsInvalid(appointment))
                {
                    MessageBox.Show("Appointment time is invalid!");
                    return;

                }

                appointmentController.Save(appointment);
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
                appointmentType = AppointmentType.examination;
            else
                appointmentType = AppointmentType.operation;
        }
    }
}
