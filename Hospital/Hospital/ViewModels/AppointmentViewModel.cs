using Model;
using System;

namespace Hospital.ViewModels
{
    public class AppointmentViewModel : ViewModel
    {

        private int id;

        private MedicalAppointmentType medicalAppointmentType;

        private DateTime startTime;

        private double durationInMinutes;

        private EmployeeViewModel doctor;

        private PatientViewModel patient;
        
        private RoomViewModel room;

        public Appointment _Appointment { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public MedicalAppointmentType MedicalAppointmentType
        {
            get { return medicalAppointmentType; }
            set
            {
                medicalAppointmentType = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged();
            }
        }

        public double DurationInMinutes
        {
            get { return durationInMinutes; }
            set
            {
                durationInMinutes = value;
                OnPropertyChanged();
            }
        }

        public EmployeeViewModel Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }

        public PatientViewModel Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged();
            }
        }

        public RoomViewModel Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged();
            }
        }


        public AppointmentViewModel(int id, MedicalAppointmentType appointmentType, DateTime startTime, Double durationInMinutes, EmployeeViewModel doctor, PatientViewModel patient, RoomViewModel room)
        {
            Id = id;
            MedicalAppointmentType = appointmentType;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
            Doctor = doctor;
            Patient = patient;
            Room = room;
        }

        public AppointmentViewModel()
        {
            _Appointment = new Appointment();
        }
    }
}
