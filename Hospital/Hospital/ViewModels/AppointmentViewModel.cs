using Model;
using System;

namespace Hospital.ViewModels
{
    public class AppointmentViewModel : ViewModel
    {
        private Injector injector;

        private int id;

        private MedicalAppointmentType medicalAppointmentType;

        private DateTime startTime;

        private double durationInMinutes;

        private EmployeeViewModel doctor;

        private PatientViewModel patient;
        
        private RoomViewModel room;

        // Umjest ovih polja AppointmentViewModel trebalo bi da sadrzi EmployeeViewModel, PatientViewModel i RoomViewModel
        private string doctorJmbg;

        private string patientJmbg;

        private string doctorFirstName;

        private string doctorLastName;

        private string doctorSpecialization;

        private string patientFirstName;

        private string patientLastName;

        private int roomId;

        private string roomName;

        private string modifiedByJmbg;


        public Injector Injector
        {
            get { return injector; }
            set
            {
                injector = value;
            }
        }

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

        public string DoctorJmbg
        {
            get { return doctorJmbg; }
            set
            {
                doctorJmbg = value;
                OnPropertyChanged();
            }
        }

        public string PatientJmbg
        {
            get { return patientJmbg; }
            set
            {
                patientJmbg = value;
                OnPropertyChanged();
            }
        }

        public string DoctorFirstName
        {
            get { return doctorFirstName; }
            set
            {
                doctorFirstName = value;
                OnPropertyChanged();
            }
        }

        public string DoctorLastName
        {
            get { return doctorLastName; }
            set
            {
                doctorLastName = value;
                OnPropertyChanged();
            }
        }

        public string DoctorSpecialization
        {
            get { return doctorSpecialization; }
            set
            {
                doctorSpecialization = value;
                OnPropertyChanged();
            }
        }

        public string PatientFirstName
        {
            get { return patientFirstName; }
            set
            {
                patientFirstName = value;
                OnPropertyChanged();
            }
        }

        public string PatientLastName
        {
            get { return patientLastName; }
            set
            {
                patientLastName = value;
                OnPropertyChanged();
            }
        }

        public int RoomId
        {
            get { return roomId; }
            set
            {
                roomId = value;
                OnPropertyChanged();
            }
        }

        public string RoomName
        {
            get { return roomName; }
            set
            {
                roomName = value;
                OnPropertyChanged();
            }
        }

        public string ModifiedByJmbg
        {
            get { return modifiedByJmbg; }
            set
            {
                modifiedByJmbg = value;
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


        public AppointmentViewModel(int id, MedicalAppointmentType appointmentType, DateTime startTime, Double durationInMinutes, string doctorJmbg, string doctorFirstName,
                                    string doctorLastName, string doctorSpecialization, string patientJmbg, string patientFirstName, string patientLastName, int roomId, string roomName)
        {
            Id = id;
            MedicalAppointmentType = appointmentType;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
            DoctorJmbg = doctorJmbg;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            DoctorSpecialization = doctorSpecialization;
            PatientJmbg = patientJmbg;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            RoomId = roomId;
            RoomName = roomName;
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

        public AppointmentViewModel(int id, MedicalAppointmentType appointmentType, DateTime startTime, double durationInMinutes, string patientJmbg, string doctorJmbg, int roomId, string modifiedByJmbg)
        {
            Id = id;
            MedicalAppointmentType = appointmentType;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
            PatientJmbg = patientJmbg;
            DoctorJmbg = doctorJmbg;
            RoomId = roomId;
            ModifiedByJmbg = modifiedByJmbg;
        }

        public AppointmentViewModel(MedicalAppointmentType appointmentType, DateTime startTime, double durationInMinutes, string patientJmbg, string doctorJmbg, int roomId, string modifiedByJmbg)
        {
            MedicalAppointmentType = appointmentType;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
            PatientJmbg = patientJmbg;
            DoctorJmbg = doctorJmbg;
            RoomId = roomId;
            ModifiedByJmbg = modifiedByJmbg;
        }

        public AppointmentViewModel(DateTime startTime, double durationInMinutes, int roomId)
        {
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
            RoomId = roomId;
            PatientJmbg = null;
            DoctorJmbg = null;
            MedicalAppointmentType = 0;

        }
        public AppointmentViewModel(int id, DateTime dateTime, double duration, int roomId)
        {
            Id = id;
            MedicalAppointmentType = 0;
            StartTime = dateTime;
            DurationInMinutes = duration;
            RoomId = roomId;
        }

        public AppointmentViewModel()
        {
            Injector = new Injector();
            _Appointment = new Appointment();
        }
    }
}
