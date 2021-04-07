/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Vladimir
 * Purpose: Definition of the Class Appointment
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
    public class Appointment
    {
        public string Id { get; set; }
        public string TimeStart { get; set; }
        public string Duration { get; set; }


        public Appointment(string id, string timeStart, string duration)
        {
            this.Id = id;
            this.TimeStart = timeStart;
            this.Duration = duration;
        }
        public Boolean SetAppointment(string id, string time, string duration)
        {
            throw new NotImplementedException();
        }

        public Appointment GetAppointment(int id)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllAppointments()
        {
            throw new NotImplementedException();
        }

        public Room room;

        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                this.room = value;
            }
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