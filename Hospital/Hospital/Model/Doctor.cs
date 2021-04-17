using System;

namespace Model
{
    public class Doctor : Employee
    {
        public Doctor (User user)
        {
            this.User = user;
        }

        public String Specialization { get; set; }

        public System.Collections.Generic.List<Appointment> Appointments { get; set; }

        public System.Collections.Generic.List<Notification> Notifications { get; set; }

        


    }
}