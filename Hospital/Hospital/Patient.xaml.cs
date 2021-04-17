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
using System.Diagnostics;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        DoctorController doctorController = new DoctorController();
        AppointmentController appointmentController = new AppointmentController();
        RoomController roomController = new RoomController();
        List<Doctor> doctors = new List<Doctor>();
        private Patient Patient;
        PatientController patientController = new PatientController();
        public PatientWindow()
        {
            InitializeComponent();
            timeDataGrid.Opacity = 0;
            Patient = patientController.GetByJmbg("2");
            doctorsDataGrid.AutoGenerateColumns = false;
            doctors = doctorController.GetAll();
            doctorsDataGrid.ItemsSource = doctors; 

        }
        private Appointment CreateAppointmentFromData()
        {
            //int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            Doctor doctor = (Doctor)doctorsDataGrid.SelectedItems[0];
            return new Appointment(appointmentController.GenerateNewId() , AppointmentType.examination, appointmentDateTime, duration, Patient, doctor, roomController.GetById(1));
        }



        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = (Doctor)doctorsDataGrid.SelectedItems[0];
            Appointment newAppointment = CreateAppointmentFromData();
            List<Appointment> doctorsAppointments = appointmentController.GetAppointmentsForDoctor(doctor.User.Jmbg);
            bool error = false;
            foreach (Appointment app in doctorsAppointments)
            {
                DateTime newAppEnd = newAppointment.StartTime.AddMinutes(newAppointment.DurationInMinutes);
                DateTime newAppStart = newAppointment.StartTime;
                DateTime doctorsAppEndTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                DateTime doctorAppStartTime = app.StartTime;
                if((newAppStart < doctorsAppEndTime && newAppStart > doctorAppStartTime)||
                    (newAppEnd > doctorAppStartTime && newAppStart < doctorsAppEndTime))
                {
                    error = true;
                }
            }
            
            if (error == true)
            {
                if (doctorRadioButton.IsChecked == true)
                {
                    MessageBox.Show("Doktor je zauzet, izaberi drugi termin", "greska");
                    timeDataGrid.Opacity = 100;
                    List<Appointment> appointments = new List<Appointment>();
                    timeDataGrid.ItemsSource = doctorsAppointments;

                }
                else
                {
                    MessageBox.Show("Termin je zauzet, izaberi drugog doktora", "greska");
                    List<Appointment> appointments = new List<Appointment>();
                    
                   
                    timeDataGrid.ItemsSource = doctorsAppointments;
                    
                }
            } else if (appointmentController.AppointmentTimeIsInvalid(newAppointment))
            {
                MessageBox.Show("Provjeri ulazne podatke", "greska");

            }
            else
            {
                appointmentController.Save(newAppointment);
            }


        }
        

     

        private void timeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            timeDataGrid.Opacity = 0;
        }
    }
}
