﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class NewOperationViewModel
    {
        public Injector Inject { get; set; }
        public NavigationService NavService { get; set; }
        public NewOperationViewModel(NavigationService navigationService)
        {
            Inject = new Injector();
            NavService = navigationService;

            //ParentWindow = parentWindow;
            //patientsDataGrid.ItemsSource = app.patientController.GetAll();
            //roomsDataGrid.ItemsSource = app.roomController.GetRoomsByRoomType(RoomType.operating);
            //new_appointment_date.SelectedDate = DateTime.Today;
        }

        //private void FilterButton_Click(object sender, RoutedEventArgs e)
        //{
        //    roomsDataGrid.ItemsSource = app.roomController.GetRoomsWithEquipmentName(equipmentName.Text);
        //}

        //private void AddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        AppointmentDTO appointment = ParseNewAppointment();
        //        if (ParentWindow.IsAppointmentScheduled(appointment))
        //            return;
        //        app.appointmentController.Save(appointment);
        //        ParentWindow.WindowUpdate();
        //        this.Close();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Chose a patient and fill all the fields!");
        //    }
        //}
        //private void CancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private AppointmentDTO ParseNewAppointment()
        //{
        //    Patient patient = (Patient)patientsDataGrid.SelectedItems[0];
        //    Room room = (Room)roomsDataGrid.SelectedItems[0];
        //    DateTime pickedDate = new_appointment_date.SelectedDate.Value;
        //    int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
        //    int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
        //    DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
        //    double duration = Convert.ToDouble(durationTextBox.Text);

        //    return new AppointmentDTO(MedicalAppointmentType.operation, appointmentDateTime, duration, patient.User.Jmbg, ParentWindow.Doctor.User.Jmbg, room.Id, ParentWindow.Doctor.User.Jmbg);
        //}
    }
}
