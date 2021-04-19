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
using Model;
using Controller;


namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientAppointments.xaml
    /// </summary>
    public partial class PatientAppointments : Window
    {
        AppointmentController appointmentController = new AppointmentController();
        List<Appointment> appointments = new List<Appointment>();
        public PatientAppointments()
        {
            InitializeComponent();

            appointments = appointmentController.GetAppointmentsForPatient("3");
            readDataGrid.ItemsSource = appointments;
            cancelButton.Visibility = Visibility.Collapsed;
            updateConfirm.Visibility = Visibility.Collapsed;
            

        }

        private void WindowUpdate()
        {
            appointments = appointmentController.GetAppointmentsForPatient("3");
            readDataGrid.ItemsSource = appointments;
        }

        private void ClearFileds()
        {
            durationTextBox.Clear();
            startTimeTextBox.Clear();

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appoinment = (Appointment)readDataGrid.SelectedItems[0];
                new_appointment_date.SelectedDate = appoinment.StartTime.Date;
                durationTextBox.Text = appoinment.DurationInMinutes.ToString();
                startTimeTextBox.Text = appoinment.StartTime.ToString(("HH:mm"));
                updateConfirm.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("Izaberi pregled", "greska");
            }
        }

        private void updateConfirm_Click(object sender, RoutedEventArgs e)
        {
            Appointment oldAppintment = (Appointment)readDataGrid.SelectedItems[0];
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);

            Appointment newAppointment = new Appointment(oldAppintment.Id,
                                                         oldAppintment.AppointmentType,
                                                         appointmentDateTime,
                                                         duration,
                                                         oldAppintment.Patient,
                                                         oldAppintment.Doctor,
                                                         oldAppintment.Room);

            bool error = appointmentController.AppointmentTimeIsInvalid(newAppointment);
            if (error)
            {
                MessageBox.Show("Greska kod unosenja podataka", "greska");
            }
            else
            {
                appointmentController.Update(newAppointment);
                WindowUpdate();
                updateConfirm.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Collapsed;
                ClearFileds();

            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            updateConfirm.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            ClearFileds();

        }

        private void deletebutton_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)readDataGrid.SelectedItems[0];
            appointmentController.Delete(appointment.Id);
            WindowUpdate();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientWindow();
            new_window.Show();
            this.Close();
        }

        
    }
}
