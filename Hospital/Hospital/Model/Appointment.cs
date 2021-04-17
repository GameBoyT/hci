using System;

namespace Model
{
    public class Appointment
    {
        public Appointment(int id, DateTime startTime, Double durationInMinutes, Patient patient, Doctor doctor)
        {
            this.Id = id;
            this.StartTime = startTime;
            this.DurationInMinutes = durationInMinutes;
            this.Patient = patient;
            this.Doctor = doctor;
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

        public Doctor Doctor { get; set; }


        public Patient Patient { get; set; }


        public Room Room { get; set; }

        public AppointmentType AppointmentType { get; set; }

    }
}