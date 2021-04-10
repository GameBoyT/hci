using System;

namespace Model
{
    public class Appointment
    {
        //public Appointment(int id, DateTime startTime, Double durationInMinutes)
        //{
        //    this.Id = id;
        //    this.StartTime = startTime;
        //    this.DurationInMinutes = durationInMinutes;
        //}

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

        public Patient patient;

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (this.patient == null || !this.patient.Equals(value))
                {
                    if (this.patient != null)
                    {
                        Patient oldPatient = this.patient;
                        this.patient = null;
                        oldPatient.RemoveAppointment(this);
                    }
                    if (value != null)
                    {
                        this.patient = value;
                        this.patient.AddAppointment(this);
                    }
                }
            }
        }
        public Doctor doctor;

        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (this.doctor == null || !this.doctor.Equals(value))
                {
                    if (this.doctor != null)
                    {
                        Doctor oldDoctor = this.doctor;
                        this.doctor = null;
                        oldDoctor.RemoveAppointment(this);
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                        this.doctor.AddAppointment(this);
                    }
                }
            }
        }

    }
}