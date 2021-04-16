using System;

namespace Model
{
    public class Doctor : Employee
    {
        public String Specialization { get; set; }

        public User User { get; set; }

        public System.Collections.Generic.List<Appointment> Appointment { get; set; }

        public System.Collections.Generic.List<Notification> Notification { get; set; }
    }
}