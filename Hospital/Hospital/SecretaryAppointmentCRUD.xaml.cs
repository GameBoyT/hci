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
        private NotificationController notificationController = new NotificationController();
        private PatientController patientController = new PatientController();
        


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

        private Notification CreateNotificationDelete()
        {
            AppointmentDTO selectAppintment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];

            string patientSurname = selectAppintment.PatientLastName.ToString();
            string doctorSurname = selectAppintment.DoctorLastName.ToString();
            string date = selectAppintment.StartTime.ToString();
            string duration = selectAppintment.DurationInMinutes.ToString();
            string text = "Imate otkazan pregled, Pacijent: " + patientSurname + " , Lekar: " + doctorSurname + " na dan " + date + " sa duzinom " + duration;
            int id = notificationController.GenerateNewId();
            int receiver = 1;
            Notification notifi = new Notification(id, text, receiver);
            notificationController.Save(notifi);
            return notifi;
        }

        private Notification CreateNotificationForUpdate()
        {
            AppointmentDTO selectAppintment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
            //string duzina = selectAppintment.DurationInMinutes.ToString();
            string duzina = durationTB.Text;
            string date = datePickerAppointment.SelectedDate.ToString();
            string name = selectAppintment.PatientFirstName.ToString();
            string surname = selectAppintment.PatientLastName.ToString();
            string doctorSurname = selectAppintment.DoctorLastName.ToString();
            int id = notificationController.GenerateNewId();
            string text = "Izmenjen pregled za pacijenta " + name + " " + surname + " koji ima zakazan pregled kod lekara " + doctorSurname +  " trajanje pregleda je " + duzina + " a datum je " + date;
            int receiver =2 ;
            Notification notification1 = new Notification(id, text, receiver);
            notificationController.Save(notification1);
            return notification1;
        }

        private void SendNotification()
        {
            //int id = notificationController.GenerateNewId();
            //notification = new Notification(id, "Pomeren je termin pregleda od strane sekretara", 15);
            notification = CreateNotificationForUpdate();
            AppointmentDTO selectAppintment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
            Employee doctor = employeeController.GetByJmbg(selectAppintment.DoctorJmbg);
            doctor.Notifications.Add(notification);
            employeeController.Update(doctor);
            Patient patient = patientController.GetByJmbg(selectAppintment.PatientJmbg);
            patient.Notifications.Add(notification);
            patientController.Update(patient);
        }

        private void SendNotificationDelete()
        {
            //int id = notificationController.GenerateNewId();
            //notification = new Notification(id, "Pomeren je termin pregleda od strane sekretara", 15);
            notification = CreateNotificationDelete();
            AppointmentDTO selectAppintment = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
            Employee doctor = employeeController.GetByJmbg(selectAppintment.DoctorJmbg);
            doctor.Notifications.Add(notification);
            employeeController.Update(doctor);
            Patient patient = patientController.GetByJmbg(selectAppintment.PatientJmbg);
            patient.Notifications.Add(notification);
            patientController.Update(patient);
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
                AppointmentDTO app = (AppointmentDTO)secretaryAppointmentDataGrid.SelectedItems[0];
                appointmentController.Delete(app.Id);
                SendNotificationDelete();
                WindowUpdate();
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
