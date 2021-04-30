using System;

namespace Model
{
    public class Appointment
    {
        public Appointment(int id, AppointmentType appointmentType, DateTime startTime, Double durationInMinutes, string patientJmbg, string doctorJmbg, Room room)
        {
            this.Id = id;
            this.AppointmentType = appointmentType;
            this.StartTime = startTime;
            this.DurationInMinutes = durationInMinutes;
            this.PatientJmbg = patientJmbg;
            this.DoctorJmbg = doctorJmbg;
            this.Room = room;
        }

        public int Id { get; set; }

        public AppointmentType AppointmentType { get; set; }

        public DateTime StartTime { get; set; }

        public Double DurationInMinutes { get; set; }

        public string DoctorJmbg { get; set; }

        public string PatientJmbg { get; set; }

        public Room Room { get; set; }

    }
}