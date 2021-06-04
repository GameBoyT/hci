using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Model;
using System.Windows;
using Hospital.Commands;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class ExaminationViewModel : ViewModel
    {
        #region Polja

        public DateTime ExaminationDate { get; set; }

        public string StartTime { get; set; }

        public Injector Inject { get; set; }

        public ObservableCollection<PatientViewModel> Patients { get; set; }

        public PatientViewModel SelectedPatient { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public NavigationService NavigationService { get; }


        #endregion

        public void Executed_AddCommand(object obj)
        {
            Inject.AppointmentService.Save(ParseNewAppointment());
        }

        public void Executed_CancelCommand(object obj)
        {
            NavigationService.GoBack();
        }

        private MedicalAppointment ParseNewAppointment()
        {
            PatientViewModel patient = SelectedPatient;
            Employee doctor = Inject.EmployeeService.GetByJmbg("1");
            DateTime pickedDate = ExaminationDate;
            int hours = Int32.Parse(StartTime.Split(':')[0]);
            int minutes = Int32.Parse(StartTime.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);

            return new MedicalAppointment(MedicalAppointmentType.examination, appointmentDateTime, 15.0, patient.Jmbg, doctor.User.Jmbg, doctor.RoomId);
        }



        #region Konstruktori
        public ExaminationViewModel()
        {
            Inject = new Injector();
            Patients = new ObservableCollection<PatientViewModel>(Inject.PatientConverter.ConvertCollectionToViewModel(Inject.PatientService.GetAll()));
            
            AddCommand = new RelayCommand(Executed_AddCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);

            ExaminationDate = DateTime.Now;
            StartTime = "12:00";
        }

        public ExaminationViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;
            Inject = new Injector();
            Patients = new ObservableCollection<PatientViewModel>(Inject.PatientConverter.ConvertCollectionToViewModel(Inject.PatientService.GetAll()));

            AddCommand = new RelayCommand(Executed_AddCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);

            ExaminationDate = DateTime.Now;
            StartTime = "12:00";
        }

        #endregion

    }
}
