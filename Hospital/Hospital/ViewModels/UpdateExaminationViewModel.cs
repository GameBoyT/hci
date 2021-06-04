using Hospital.Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class UpdateExaminationViewModel : ViewModel
    {
        public Injector Inject { get; set; }
        
        public NavigationService NavService { get; set; }

        public AppointmentViewModel Appointment { get; set; }

        public DateTime ExaminationDate { get; set; }

        public string StartTime { get; set; }

        public Patient Patient { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        private bool CanExecute_UpdateCommand(object obj)
        {
            if (StartTime != "") return true;
            return false;
        }


        public void Executed_UpdateCommand(object obj)
        {
            Inject.AppointmentService.Update(ParseAppointment());
        }

        public void Executed_CancelCommand(object obj)
        {
            NavService.GoBack();
        }

        private MedicalAppointment ParseAppointment()
        {
            DateTime pickedDate = ExaminationDate;
            int hours = int.Parse(StartTime.Split(':')[0]);
            int minutes = int.Parse(StartTime.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);

            return new MedicalAppointment(Appointment.Id, MedicalAppointmentType.examination, appointmentDateTime, 15.0, Appointment.Patient.Jmbg, Appointment.Doctor.Jmbg, Appointment.Room.Id);
        }


        public UpdateExaminationViewModel(NavigationService navigationService, AppointmentViewModel appointment)
        {
            Inject = new Injector();
            NavService = navigationService;

            UpdateCommand = new RelayCommand(Executed_UpdateCommand, CanExecute_UpdateCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);

            Appointment = appointment;
            ExaminationDate = appointment.StartTime.Date;
            StartTime = appointment.StartTime.Hour + ":" + appointment.StartTime.Minute;
        }

    }
}
