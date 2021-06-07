using Hospital.Commands;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class NewOperationViewModel
    {
        public Injector Inject { get; set; }

        public NavigationService NavService { get; set; }

        public AppointmentViewModel Appointment { get; set; }

        public Employee Doctor { get; set; }


        public ObservableCollection<PatientViewModel> Patients { get; set; }

        public PatientViewModel SelectedPatient { get; set; }

        public ObservableCollection<RoomViewModel> Rooms { get; set; }

        public RoomViewModel SelectedRoom { get; set; }

        public DateTime ExaminationDate { get; set; }

        public string StartTime { get; set; }

        public string Duration { get; set; }

        public string Equipment { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public RelayCommand FilterCommand { get; set; }


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

        private bool CanExecute_FilterCommand(object obj)
        {
            if (Equipment != "") return true;
            return false;
        }

        public void Executed_FilterCommand(object obj)
        {
            ObservableCollection<RoomViewModel> RoomsWithEquipment = Inject.RoomConverter.ConvertCollectionToViewModel(Inject.RoomService.GetRoomsWithEquipmentName(Equipment));
            Rooms.Clear();
            foreach (RoomViewModel room in RoomsWithEquipment)
            {
                Rooms.Add(room);
            }
        }


        private void ParseTime()
        {
            try
            {
                int hours = int.Parse(StartTime.Split(':')[0]);
                int minutes = int.Parse(StartTime.Split(':')[1]);
                DateTime appointmentDateTime = new DateTime(ExaminationDate.Year, ExaminationDate.Month, ExaminationDate.Day, hours, minutes, 00);
                Appointment.StartTime = appointmentDateTime;
                if (!string.IsNullOrWhiteSpace(Duration))
                    Appointment.DurationInMinutes = float.Parse(Duration);
            }
            catch { }
        }

        private MedicalAppointment ParseAppointment()
        {
            return new MedicalAppointment(MedicalAppointmentType.operation, Appointment.StartTime, Appointment.DurationInMinutes, Appointment.Patient.Jmbg, Doctor.User.Jmbg, Appointment.Room.Id);
        }


        public NewOperationViewModel(NavigationService navigationService)
        {
            Inject = new Injector();
            NavService = navigationService;
            Doctor = Inject.EmployeeService.GetByJmbg("1");

            AddCommand = new RelayCommand(Executed_AddCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);
            FilterCommand = new RelayCommand(Executed_FilterCommand, CanExecute_FilterCommand);

            ExaminationDate = DateTime.Now;
            StartTime = "";

            Appointment = new AppointmentViewModel();

            Patients = Inject.PatientConverter.ConvertCollectionToViewModel(Inject.PatientService.GetAll());
            Rooms = Inject.RoomConverter.ConvertCollectionToViewModel(Inject.RoomService.GetRoomsByRoomType(RoomType.operating));
        }
    }
}
