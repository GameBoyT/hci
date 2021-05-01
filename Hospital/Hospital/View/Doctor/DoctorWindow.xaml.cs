using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class DoctorWindow : Window
    {
        App app;
        List<AppointmentDTO> appointments = new List<AppointmentDTO>();
        List<AppointmentDTO> appointmentsToShow = new List<AppointmentDTO>();
        List<Room> roomsToShow = new List<Room>();
        public Appointment Appointment { get; set; }
        public Employee Doctor { get; set; }
        private AppointmentType appointmentType;

        public DoctorWindow()
        {
            InitializeComponent();
            app = Application.Current as App;
            //this.DataContext = this;

            Doctor = app.employeeController.GetByJmbg("1");

            appointment_date.SelectedDate = DateTime.Today;
            new_appointment_date.SelectedDate = DateTime.Today;
            appointmentType = AppointmentType.examination;
            rooms.IsEnabled = false;
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
                Appointment = (Appointment)appointmentsDataGrid.SelectedItems[0];

                addNewAppointmentButton.Visibility = Visibility.Collapsed;
                updateAppointmentButton.Visibility = Visibility.Visible;
                cancelUpdateButton.Visibility = Visibility.Visible;
                title.Content = "Edit appointment";

                appointmentType = Appointment.AppointmentType;
                new_appointment_date.SelectedDate = Appointment.StartTime.Date;
                durationTextBox.Text = Appointment.DurationInMinutes.ToString();
                startTimeTextBox.Text = Appointment.StartTime.ToString("HH:mm");
                patientJmbg.Text = Appointment.PatientJmbg;
                roomsToShow.Clear();
                roomsToShow.Add(app.roomController.GetById(Appointment.RoomId));
                rooms.SelectedIndex = 0;


                patientJmbg.IsReadOnly = true;
                patientJmbg.IsEnabled = false;
                examinationRadioButton.IsEnabled = false;
                operationRadioButton.IsEnabled = false;

                if (appointmentType == AppointmentType.examination)
                {
                    equipmentName.IsEnabled = false;
                    rooms.IsEnabled = false;
                }
                else
                {
                    equipmentName.IsEnabled = true;
                    rooms.IsEnabled = true;
                }

            }
            catch
            {
                MessageBox.Show("You have to select an appointment to update!");
            }

        }

        private void WindowUpdate()
        {
            appointments = app.appointmentController.GetAppointmentsForDoctor(Doctor.User.Jmbg);
            appointmentsToShow = appointments.FindAll(appointment => appointment.StartTime.Date == appointment_date.SelectedDate);
            appointmentsDataGrid.ItemsSource = appointmentsToShow;
            rooms.ItemsSource = roomsToShow;
            rooms.SelectedIndex = 0;
        }

        private Appointment CreateAppointmentFromData()
        {
            int id = app.appointmentController.GenerateNewId();
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            rooms.SelectedIndex = 0;

            if (appointmentType == AppointmentType.examination)
            {
                return new Appointment(id, appointmentType, appointmentDateTime, duration, patientJmbg.Text, Doctor.User.Jmbg, Doctor.RoomId);
            }
            else
            {
                return new Appointment(id, appointmentType, appointmentDateTime, duration, patientJmbg.Text, Doctor.User.Jmbg, ((Room)rooms.SelectedItem).Id);
            }
        }

        private Appointment UpdateAppointmentFromData()
        {
            int id = Appointment.Id;
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            rooms.SelectedIndex = 0;
            if (appointmentType == AppointmentType.examination)
            {
                return new Appointment(id, appointmentType, appointmentDateTime, duration, patientJmbg.Text, Doctor.User.Jmbg, Doctor.RoomId);
            }
            else
            {
                return new Appointment(id, appointmentType, appointmentDateTime, duration, patientJmbg.Text, Doctor.User.Jmbg, ((Room)rooms.SelectedItem).Id);
            }
        }

        private void Update_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = UpdateAppointmentFromData();
                //if (app.appointmentController.AppointmentTimeIsInvalid(appointment))
                //    return;
                app.appointmentController.Update(appointment);
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
            new_appointment_date.SelectedDate = DateTime.Today;
            durationTextBox.Clear();
            startTimeTextBox.Clear();
            patientJmbg.Clear();

            addNewAppointmentButton.Visibility = Visibility.Visible;
            updateAppointmentButton.Visibility = Visibility.Collapsed;
            cancelUpdateButton.Visibility = Visibility.Collapsed;

            title.Content = "New appointment";

            patientJmbg.IsReadOnly = false;
            patientJmbg.IsEnabled = true;
            examinationRadioButton.IsEnabled = true;
            operationRadioButton.IsEnabled = true;
            roomsToShow.Clear();
            equipmentName.Clear();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appointment = (AppointmentDTO)appointmentsDataGrid.SelectedItems[0];
                app.appointmentController.Delete(appointment.Id);
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to delete!");
            }
        }


        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = CreateAppointmentFromData();
                if (appointment.PatientJmbg == null)
                {
                    MessageBox.Show("You have to enter a valid patient jmbg!");
                    return;
                }

                //TODO: treba refaktorisati AppointmentTimeIsInvalid da moze izbaciti error za svaku gresku posebno
                //if (app.appointmentController.AppointmentTimeIsInvalid(appointment))
                //{
                //    MessageBox.Show("Appointment time is invalid!");
                //    return;

                //}

                app.appointmentController.Save(appointment);
                ChangeToNewAppointment();
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void TypeButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            if (radioButton.Content.ToString() == "Examination")
            {
                appointmentType = AppointmentType.examination;
                roomsToShow.Clear();
                rooms.SelectedIndex = -1;
                roomsToShow.Add(app.roomController.GetById(Doctor.RoomId));
                rooms.IsEnabled = false;
                equipmentName.IsEnabled = false;
                findButton.IsEnabled = false;
                WindowUpdate();
            }
            else
            {
                appointmentType = AppointmentType.operation;
                roomsToShow.Clear();
                rooms.SelectedIndex = -1;
                rooms.IsEnabled = true;
                equipmentName.IsEnabled = true;
                findButton.IsEnabled = true;
                WindowUpdate();
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            //roomsToShow = app.staticEquipmentController.GetAllRoomsWithEquipmentName(equipmentName.Text);
            WindowUpdate();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentDTO appointment = (AppointmentDTO)appointmentsDataGrid.SelectedItems[0];
                DoctorViewPatient doctorViewPatientWindow = new DoctorViewPatient(appointment);
                doctorViewPatientWindow.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to view!");
            }
        }

        private void NewExaminationButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorExamination doctorViewPatientWindow = new DoctorExamination();

        }
    }
}
