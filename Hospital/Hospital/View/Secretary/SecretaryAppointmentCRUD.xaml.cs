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
        List<Patient> patients = new List<Patient>();
        Patient patient;
        Employee doctor;
        List<AppointmentDTO> doctorsAppointments;



        public SecretaryAppointmentCRUD()
        {
            InitializeComponent();
            appointments = appointmentController.GetAll();
            secretaryAppointmentDataGrid.ItemsSource = appointments;
            employees = employeeController.GetDoctors();
            doctorsDataGrid.ItemsSource = employees;
            patients = patientController.GetAll();
            patientsDataGrid.ItemsSource = patients;
           cancelButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
            timeDataGrid.Visibility = Visibility.Collapsed;
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
                appointmentController.Delete(app.Id, employeeController.GetSecretary().User.Jmbg);
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
                                                        oldAppointment.MedicalAppointmentType,
                                                        appointmentDateTime,
                                                        duration,
                                                        oldAppointment.PatientJmbg,
                                                        oldAppointment.DoctorJmbg,
                                                        oldAppointment.RoomId,
                                                        employeeController.GetSecretary().User.Jmbg);

            bool error = appointmentController.AppointmentTimeIsInvalid(newAppointment);
            if (error)
            {
                MessageBox.Show("Izmjena nije moguca", "greska");
            }
            else
            {
                appointmentController.Update(newAppointment);
                WindowUpdate();
                saveButton.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Collapsed;
                ClearFileds();

            }
        }

        private AppointmentDTO CreateAppointmentFromData()
        {
            //int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = datePickerAppointment.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTB.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTB.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTB.Text);

            return new AppointmentDTO(MedicalAppointmentType.examination, appointmentDateTime, duration, patient.User.Jmbg, doctor.User.Jmbg, doctor.RoomId, employeeController.GetSecretary().User.Jmbg);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                doctor = (Employee)doctorsDataGrid.SelectedItems[0];
                patient = (Patient)patientsDataGrid.SelectedItems[0];
                AppointmentDTO newAppointment = CreateAppointmentFromData();
                doctorsAppointments = appointmentController.GetAppointmentsForDoctor(doctor.User.Jmbg);
                bool error = false;

                if (appointmentController.AppointmentIsTaken(newAppointment, doctor.User.Jmbg))
                {
                    error = true;
                }

                if (error == true)
                {
                    if (doctorRadioButton.IsChecked == true)
                    {
                        MessageBox.Show("Doktor je zauzet, izaberi drugi termin", "greska");
                        timeDataGrid.Visibility = Visibility.Visible;
                        timeDataGrid.ItemsSource = doctorsAppointments;
                    }
                    else
                    {
                        MessageBox.Show("Termin je zauzet, izaberi drugog doktora", "greska");
                        timeDataGrid.ItemsSource = doctorsAppointments;
                    }
                }
                else if (appointmentController.AppointmentValidationWithoutOverlaping(newAppointment))
                {
                    MessageBox.Show("Termin nije moguce dodati, ponovi unos", "greska");
                }
                else
                {
                    appointmentController.Save(newAppointment);
                    ClearFileds();
                    MessageBox.Show("Novi termin uspjeno dodat", "Uspjesno");
                    WindowUpdate();
                }

            }
            catch
            {
                MessageBox.Show("Unesi podatke u sva polja", "Greska");
            }

        }

        private void timeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            timeDataGrid.Visibility = Visibility.Collapsed;
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            SecretaryGuest secretaryGuest = new SecretaryGuest();
            secretaryGuest.Show();
            this.Close();
        }
    }
}
