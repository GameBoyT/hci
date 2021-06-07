using Hospital.Commands;
using Model;
using System;
using System.Windows;
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
            ParseTime();
            Appointment.Validate();
            if (Appointment.IsValid)
            {
                Inject.AppointmentService.Update(ParseAppointment());
                MessageBox.Show("Appointment successfully updated!");
            }
        }

        public void Executed_CancelCommand(object obj)
        {
            NavService.GoBack();
        }

        private void ParseTime()
        {
            try
            {
                int hours = int.Parse(StartTime.Split(':')[0]);
                int minutes = int.Parse(StartTime.Split(':')[1]);
                DateTime appointmentDateTime = new DateTime(ExaminationDate.Year, ExaminationDate.Month, ExaminationDate.Day, hours, minutes, 00);
                Appointment.StartTime = appointmentDateTime;
            }
            catch { }
        }

        private MedicalAppointment ParseAppointment()
        {
            return new MedicalAppointment(Appointment.Id, Appointment.MedicalAppointmentType, Appointment.StartTime, Appointment.DurationInMinutes, Appointment.Patient.Jmbg, Appointment.Doctor.Jmbg, Appointment.Room.Id);
        }


        public UpdateExaminationViewModel(NavigationService navigationService, AppointmentViewModel appointment)
        {
            Inject = new Injector();
            NavService = navigationService;

            UpdateCommand = new RelayCommand(Executed_UpdateCommand, CanExecute_UpdateCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);

            Appointment = appointment;
            ExaminationDate = Appointment.StartTime.Date;
            StartTime = Appointment.StartTime.Hour + ":" + Appointment.StartTime.Minute;
        }

    }
}
