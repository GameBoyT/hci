using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : Entity
    {
        public Patient(User user)
        {
            this.User = user;
            this.MedicalRecord = new MedicalRecord();
            this.Appointments = new List<MedicalAppointment>();
            this.Notifications = new List<Notification>();
            this.CancelationDates = new List<DateTime>();
            this.Blocked = false;
        }

        public List<DateTime> CancelationDates { get; set; }

        public bool Blocked { get; set; }

        public User User { get; set; }

        public MedicalRecord MedicalRecord { get; set; }

        public List<MedicalAppointment> Appointments { get; set; }

        public List<Notification> Notifications { get; set; }
    }
}