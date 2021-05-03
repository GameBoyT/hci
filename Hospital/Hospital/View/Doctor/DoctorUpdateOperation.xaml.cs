using DTO;
using Model;
using System;
using System.Windows;

namespace Hospital.View.Doctor
{
    public partial class DoctorUpdateOperation : Window
    {
        App app;
        public DoctorWindow ParentWindow { get; set; }
        public AppointmentDTO Appointment { get; set; }
        public Patient Patient { get; set; }

        public DoctorUpdateOperation(DoctorWindow parentWindow, AppointmentDTO appointment)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;

            ParentWindow = parentWindow;
            Appointment = appointment;
            Patient = app.patientController.GetByJmbg(appointment.PatientJmbg);
            new_appointment_date.SelectedDate = appointment.StartTime;
            startTimeTextBox.Text = appointment.StartTime.ToString("HH:mm");
            durationTextBox.Text = appointment.DurationInMinutes.ToString();
            roomsDataGrid.ItemsSource = app.roomController.GetOperationRooms();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            roomsDataGrid.ItemsSource = app.roomController.GetRoomsWithEquipmentName(equipmentName.Text);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appointment = ParseUpdatedAppointment();
                if (IsDoctorOrRoomScheduled(appointment))
                    return;
                app.appointmentController.Update(appointment);
                ParentWindow.WindowUpdate();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Fill all the fields!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private AppointmentDTO ParseUpdatedAppointment()
        {
            Room room = (Room)roomsDataGrid.SelectedItems[0];
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);


            return new AppointmentDTO(Appointment.Id, AppointmentType.operation, appointmentDateTime, duration, Appointment.PatientJmbg, Appointment.DoctorJmbg, room.Id);
        }

        private bool IsDoctorOrRoomScheduled(AppointmentDTO appointment)
        {
            if (!app.appointmentController.IsDoctorAvailable(appointment, ParentWindow.Doctor.User.Jmbg))
            {
                MessageBox.Show("You already have an appointment at that time!");
                return true;
            }
            if (!app.appointmentController.IsRoomAvailable(appointment, appointment.RoomId))
            {
                MessageBox.Show("The room already has an appointment at that time!");
                return true;
            }
            return false;
        }
    }
}
