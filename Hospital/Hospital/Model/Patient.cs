using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient
    {


        public Patient(User user)
        {
            this.User = user;
            this.MedicalRecord = new MedicalRecord();
            this.Appointments = new System.Collections.Generic.List<Appointment>();
            this.Notifications = new System.Collections.Generic.List<Notification>();
            this.CancelationDates = new List<DateTime>();
            this.Blocked = false;

        }

        
        public List<DateTime> CancelationDates { get; set; }

        public bool Blocked { get; set; }

        public User User { get; set; }

        public MedicalRecord MedicalRecord { get; set; }

        public System.Collections.Generic.List<Appointment> Appointments { get; set; }

        public System.Collections.Generic.List<Notification> Notifications { get; set; }
    }
}