using Controller;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for SecretaryAppointmentCRUD.xaml
    /// </summary>
    public partial class SecretaryAppointmentCRUD : Window
    {
        List<AppointmentDTO> appointments = new List<AppointmentDTO>();
        private AppointmentController appointmentController = new AppointmentController();
        private EmployeeController employeeController = new EmployeeController();
        List<Employee> employees = new List<Employee>();
        Notification notification;
        


        public SecretaryAppointmentCRUD()
        {
            InitializeComponent();
            appointments = appointmentController.GetAll();
            secretaryAppointmentDataGrid.ItemsSource = appointments;
            employees = employeeController.GetDoctors();
            doctorsDataGrid.ItemsSource = employees;
            cancelButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
        }

        private void ClearFileds()
        {
            durationTB.Clear();
            startTimeTB.Clear();

        }

        private void WindowUpdate()
        {
            appointments = appointmentController.GetAll();
            secretaryAppointmentDataGrid.ItemsSource = appointments;
        }

        private void SendNotification()
        {
            
            notification = new Notification(1, "fsdfdsfgdsfsgsdg", 15);
            AppointmentDTO selectAppintment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
            Employee doctor = employeeController.GetByJmbg(selectAppintment.DoctorJmbg);
            doctor.Notifications.Add(notification);
            employeeController.Update(doctor);
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment app = (Appointment)secretaryAppointmentDataGrid.SelectedItems[0];
                appointmentController.Delete(app.Id);
                //WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to delete!");
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appoinment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
                datePickerAppointment.SelectedDate = appoinment.StartTime.Date;
                durationTB.Text = appoinment.DurationInMinutes.ToString();
                startTimeTB.Text = appoinment.StartTime.ToString(("HH:mm"));
                saveButton.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("Izaberi pregled", "greska");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            saveButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            ClearFileds();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            AppointmentDTO oldAppointment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
            DateTime pickedDate = datePickerAppointment.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTB.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTB.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTB.Text);

            AppointmentDTO newAppointment = new AppointmentDTO(oldAppointment.Id,
                                                        oldAppointment.AppointmentType,
                                                        appointmentDateTime,
                                                        duration,
                                                        oldAppointment.PatientJmbg,
                                                        oldAppointment.DoctorJmbg,
                                                        oldAppointment.RoomId);

            bool error = appointmentController.AppointmentTimeIsInvalid(newAppointment);
            if (error)
            {
                MessageBox.Show("Izmjena nije moguca", "greska");
            }
            else
            {
                appointmentController.Update(newAppointment);
                SendNotification();
                WindowUpdate();
                saveButton.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Collapsed;
                ClearFileds();

            }
        }
    }
}
