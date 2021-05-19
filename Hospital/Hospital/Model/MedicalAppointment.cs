using System;

namespace Model
{
    public class MedicalAppointment : Appointment
    {
        public MedicalAppointment(int id, MedicalAppointmentType medicalAppointmentType, DateTime startTime, Double durationInMinutes, string patientJmbg, string doctorJmbg, int roomId) : base(id, startTime, durationInMinutes, roomId)
        {
            MedicalAppointmentType = medicalAppointmentType;
            PatientJmbg = patientJmbg;
            DoctorJmbg = doctorJmbg;
        }

        public MedicalAppointmentType MedicalAppointmentType { get; set; }

        public string DoctorJmbg { get; set; }

        public string PatientJmbg { get; set; }

    }
}