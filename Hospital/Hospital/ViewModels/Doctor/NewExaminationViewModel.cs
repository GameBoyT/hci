using Hospital.Commands;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class NewExaminationViewModel : ViewModel
    {
        #region Polja

        public DateTime ExaminationDate { get; set; }

        public string StartTime { get; set; }

        public Injector Inject { get; set; }

        public AppointmentViewModel Appointment { get; set; }

        public ObservableCollection<PatientViewModel> Patients { get; set; }

        public Employee Doctor { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public NavigationService NavService { get; set; }


        #endregion

        //private bool CanExecute_AddCommand(object obj)
        //{
        //    if (StartTime != "" || SelectedPatient != null) return true;
        //    return false;
        //}


        public void Executed_AddCommand(object obj)
        {
            ParseTime();
            Appointment.Validate();
            if (Appointment.IsValid)
            {
                Inject.AppointmentService.Save(ParseAppointment());
                MessageBox.Show("Appointment successfully created!");
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
            return new MedicalAppointment(MedicalAppointmentType.examination, Appointment.StartTime, Appointment.DurationInMinutes, Appointment.Patient.Jmbg, Doctor.User.Jmbg, Doctor.RoomId);
        }

        #region Konstruktori
        public NewExaminationViewModel(NavigationService navigationService)
        {
            NavService = navigationService;
            Inject = new Injector();
            Patients = new ObservableCollection<PatientViewModel>(Inject.PatientConverter.ConvertCollectionToViewModel(Inject.PatientService.GetAll()));
            AddCommand = new RelayCommand(Executed_AddCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);
            Doctor = Inject.EmployeeService.GetByJmbg("1");

            ExaminationDate = DateTime.Now;
            StartTime = "";

            Appointment = new AppointmentViewModel
            {
                DurationInMinutes = 15.0,
                Room = Inject.RoomConverter.ConvertModelToViewModel(Inject.RoomService.GetById(Doctor.RoomId))
            };

        }

        #endregion

    }
}
