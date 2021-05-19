using DTO;
using Model;
using System;
using System.Windows;

namespace Hospital.View.Doctor
{
    public partial class DoctorExamination : Window
    {
        App app;
        public DoctorWindow ParentWindow { get; set; }
        public DoctorExamination(DoctorWindow parentWindow)
        {
            InitializeComponent();
            app = Application.Current as App;

            ParentWindow = parentWindow;
            patientsDataGrid.ItemsSource = app.patientController.GetAll();
            new_appointment_date.SelectedDate = DateTime.Today;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appointment = ParseNewAppointment();
                if (ParentWindow.IsAppointmentScheduled(appointment))
                    return;
                app.appointmentController.Save(appointment);
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

            return new AppointmentDTO(MedicalAppointmentType.examination, appointmentDateTime, 15.0, patient.User.Jmbg, ParentWindow.Doctor.User.Jmbg, ParentWindow.Doctor.RoomId, ParentWindow.Doctor.User.Jmbg);
        }
    }
}
