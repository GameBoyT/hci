using Hospital.Commands;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class NewOperationViewModel
    {
        public Injector Inject { get; set; }

        public NavigationService NavService { get; set; }

        public ObservableCollection<PatientViewModel> Patients { get; set; }

        public PatientViewModel SelectedPatient { get; set; }

        public ObservableCollection<RoomViewModel> Rooms { get; set; }

        public RoomViewModel SelectedRoom { get; set; }

        public DateTime ExaminationDate { get; set; }

        public string StartTime { get; set; }

        public double Duration { get; set; }

        public string Equipment { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public RelayCommand FilterCommand { get; set; }


        private bool CanExecute_AddCommand(object obj)
        {
            if (StartTime != "" || SelectedPatient != null) return true;
            return false;
        }

        public void Executed_AddCommand(object obj)
        {
            Inject.AppointmentService.Save(ParseAppointment());
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


        private MedicalAppointment ParseAppointment()
        {
            PatientViewModel patient = SelectedPatient;
            Employee doctor = Inject.EmployeeService.GetByJmbg("1");
            DateTime pickedDate = ExaminationDate;
            int hours = int.Parse(StartTime.Split(':')[0]);
            int minutes = int.Parse(StartTime.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);

            return new MedicalAppointment(MedicalAppointmentType.examination, appointmentDateTime, Duration, patient.Jmbg, doctor.User.Jmbg, SelectedRoom.Id);
        }


        public NewOperationViewModel(NavigationService navigationService)
        {
            Inject = new Injector();
            NavService = navigationService;

            AddCommand = new RelayCommand(Executed_AddCommand, CanExecute_AddCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);
            FilterCommand = new RelayCommand(Executed_FilterCommand, CanExecute_FilterCommand);

            ExaminationDate = DateTime.Now;
            StartTime = "12:00";

            Patients = Inject.PatientConverter.ConvertCollectionToViewModel(Inject.PatientService.GetAll());
            Rooms = Inject.RoomConverter.ConvertCollectionToViewModel(Inject.RoomService.GetRoomsByRoomType(RoomType.operating));
        }
    }
}
