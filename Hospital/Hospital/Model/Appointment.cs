using System;

namespace Model
{
    public class Appointment
    {
        public Appointment(int id, AppointmentType appointmentType, DateTime startTime, Double durationInMinutes, Patient patient, Employee doctor, Room room)
        {
            this.Id = id;
            this.AppointmentType = appointmentType;
            this.StartTime = startTime;
            this.DurationInMinutes = durationInMinutes;
            this.Patient = patient;
            this.Doctor = doctor;
            this.Room = room;
        }

        public int Id
        {
            get
            ;
            set
            ;
        }

        public DateTime StartTime
        {
            get
            ;
            set
            ;
        }

        public Double DurationInMinutes
        {
            get
            ;
            set
            ;
        }

        public Employee Doctor { get; set; }


        public Patient Patient { get; set; }


        public Room Room { get; set; }

        public AppointmentType AppointmentType { get; set; }

    }
}