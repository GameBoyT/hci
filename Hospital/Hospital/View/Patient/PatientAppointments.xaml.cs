using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using DTO;


namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientAppointments.xaml
    /// </summary>
    public partial class PatientAppointments : Window
    {
        AppointmentController appointmentController = new AppointmentController();
        PatientController patientController = new PatientController();
        List<Appointment> appointments = new List<Appointment>();
        List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
        public PatientAppointments()
        {
            InitializeComponent();

            appointmentDTOs = appointmentController.GetAppointmentsForPatient("5");
            readDataGrid.ItemsSource = appointmentDTOs;
            cancelButton.Visibility = Visibility.Collapsed;
            updateConfirm.Visibility = Visibility.Collapsed;


        }

        private void WindowUpdate()
        {
            appointmentDTOs = appointmentController.GetAppointmentsForPatient("5");
            readDataGrid.ItemsSource = appointmentDTOs;
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
                AppointmentDTO appoinmentDTO = (AppointmentDTO)readDataGrid.SelectedItems[0];
                Appointment appointment = appointmentController.ConvertToModel(appoinmentDTO);
                new_appointment_date.SelectedDate = appointment.StartTime.Date;
                durationTextBox.Text = appointment.DurationInMinutes.ToString();
                startTimeTextBox.Text = appointment.StartTime.ToString(("HH:mm"));
                updateConfirm.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("Izaberi pregled", "greska");
            }
        }


        private AppointmentDTO AppointmentFromData()
        {

            AppointmentDTO oldAppointmentDTO = (AppointmentDTO)readDataGrid.SelectedItems[0];
            Appointment oldAppointment = appointmentController.ConvertToModel(oldAppointmentDTO);
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);

            AppointmentDTO newAppointment = new AppointmentDTO(oldAppointment.Id,
                                                         oldAppointment.AppointmentType,
                                                         appointmentDateTime,
                                                         duration,
                                                         oldAppointment.PatientJmbg,
                                                         oldAppointment.DoctorJmbg,
                                                         oldAppointment.RoomId);
            return newAppointment;
        }

        private void updateConfirm_Click(object sender, RoutedEventArgs e)
        {
            

            bool error = appointmentController.AppointmentTimeIsInvalid(AppointmentFromData());
            if (error)
            {
                MessageBox.Show("Izmjena nije moguca", "greska");
            }
            else
            {
                appointmentController.Update(AppointmentFromData(), "5");
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
            AppointmentDTO appointmentDTO = (AppointmentDTO)readDataGrid.SelectedItems[0];
            string message = patientController.AntiTrollCheck(appointmentDTO.Id);
            MessageBox.Show(message, "obavjestenje");
            appointmentController.Delete(appointmentDTO.Id, "5");

            if(message == "Pacijent je blokiran")
            {
                var new_window = new MainWindow();
                new_window.Show();
                this.Close();

            }

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
