using Model;
using System;

namespace DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public MedicalAppointmentType MedicalAppointmentType { get; set; }

        public DateTime StartTime { get; set; }

        public Double DurationInMinutes { get; set; }

        public string DoctorJmbg { get; set; }

        public string PatientJmbg { get; set; }

        public String DoctorFirstName { get; set; }

        public String DoctorLastName { get; set; }

        public String DoctorSpecialization { get; set; }

        public String PatientFirstName { get; set; }

        public String PatientLastName { get; set; }

        public int RoomId { get; set; }

        public String RoomName { get; set; }

        public String ModifiedByJmbg { get; set; }


        public AppointmentDTO(int id, MedicalAppointmentType appointmentType, DateTime startTime, Double durationInMinutes, String doctorJmbg, string doctorFirstName,
                                    string doctorLastName, string doctorSpecialization, String patientJmbg, String patientFirstName, String patientLastName, int roomId, String roomName)
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

        public AppointmentDTO(int id, MedicalAppointmentType appointmentType, DateTime startTime, double durationInMinutes, string patientJmbg, string doctorJmbg, int roomId, string modifiedByJmbg)
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

        public AppointmentDTO(MedicalAppointmentType appointmentType, DateTime startTime, double durationInMinutes, string patientJmbg, string doctorJmbg, int roomId, string modifiedByJmbg)
        {
            MedicalAppointmentType = appointmentType;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
            PatientJmbg = patientJmbg;
            DoctorJmbg = doctorJmbg;
            RoomId = roomId;
            ModifiedByJmbg = modifiedByJmbg;
        }

        // napravitit konstruktor za ronoviranje
    }
}
