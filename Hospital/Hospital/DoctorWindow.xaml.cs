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
            try
            {
                Appointment appointment = (Appointment)appointmentsDataGrid.SelectedItems[0];

                addNewAppointmentButton.Visibility = Visibility.Collapsed;
                updateAppointmentButton.Visibility = Visibility.Visible;
                cancelUpdateButton.Visibility = Visibility.Visible;
                title.Content = "Edit appointment";

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
            catch
            {
                MessageBox.Show("You have to select an appointment to update!");
            }

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
            try
            {
                Appointment appointment = CreateAppointmentFromData();
                if (AppointmentTimeIsInvalid(appointment))
                    return;
                appointmentStorage.Update(appointment);
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
            try
            {
                Appointment app = (Appointment)appointmentsDataGrid.SelectedItems[0];
                appointmentStorage.Delete(app.Id);
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to delete!");
            }
        }

        private bool AppointmentTimeIsInvalid(Appointment appointment)
        {
            if (DateTime.Now.Date > appointment.StartTime.Date)
            {
                MessageBox.Show("You can't chose a date before today!");
                return true;
            }

            foreach (Appointment app in appointments)
            {
                if (app.Id != appointment.Id)
                {
                    DateTime endTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                    DateTime appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    if ((app.StartTime.Ticks < appointment.StartTime.Ticks && endTime.Ticks > appointment.StartTime.Ticks) ||
                            (app.StartTime.Ticks < appointmentEndTime.Ticks && endTime.Ticks > appointmentEndTime.Ticks))
                    {
                        MessageBox.Show("There is an appointment at that time!");
                        return true;
                    }
                }
                else
                {
                    if (DateTime.Now.AddDays(1).Ticks > app.StartTime.Ticks)
                    {
                        MessageBox.Show("You can't update an appointment that is less then 24h away!");
                        return true;
                    }

                    if (app.StartTime.AddDays(2).Ticks < appointment.StartTime.Ticks)
                    {
                        MessageBox.Show("You can't update an appointment to a date over 2 days later!");
                        return true;
                    }
                }
            }
            return false;
        }
        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = CreateAppointmentFromData();
                if (appointmentStorage.UniqueId(appointment.Id) == false)
                {
                    MessageBox.Show("You have to enter a unique id!");
                    return;
                }
                if (appointment.Patient == null)
                {
                    MessageBox.Show("You have to enter a valid patient jmbg!");
                    return;
                }
                if (AppointmentTimeIsInvalid(appointment))
                    return;

                appointmentStorage.Save(appointment);
                ChangeToNewAppointment();
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }
    }
}
