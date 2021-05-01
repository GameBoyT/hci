using DTO;
using Model;
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

namespace Hospital.View.Doctor
{
    public partial class DoctorExamination : Window
    {
        App app;
        public DoctorWindow ParentWindow { get; set; }
        //private List<Patient> _patientsToShow { get; set; }
        public DoctorExamination(DoctorWindow parentWindow)
        {
            InitializeComponent();
            app = Application.Current as App;

            ParentWindow = parentWindow;
            patientsDataGrid.ItemsSource = app.patientController.GetAll();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appointment = ParseNewAppointment();
                app.appointmentController.SaveDTO(appointment);
                ParentWindow.WindowUpdate();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Chose a patient and fill all the fields!");
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private AppointmentDTO ParseNewAppointment()
        {
            Patient patient = (Patient)patientsDataGrid.SelectedItems[0];
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);

            return new AppointmentDTO(AppointmentType.examination, appointmentDateTime, 15.0, patient.User.Jmbg, ParentWindow.Doctor.User.Jmbg, ParentWindow.Doctor.RoomId);
        }
    }
}
