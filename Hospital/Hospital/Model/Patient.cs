namespace Model
{
    public class Patient
    {
        public System.Collections.Generic.List<Appointment> appointment;

        public System.Collections.Generic.List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new System.Collections.Generic.List<Appointment>();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }

        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new System.Collections.Generic.List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.Patient = this;
            }
        }

        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                {
                    this.appointment.Remove(oldAppointment);
                    oldAppointment.Patient = null;
                }
        }

        public void RemoveAllAppointment()
        {
            if (appointment != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointment)
                    tmpAppointment.Add(oldAppointment);
                appointment.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.Patient = null;
                tmpAppointment.Clear();
            }
        }
        public User User;
        public MedicalRecord MedicalRecord;
        public System.Collections.Generic.List<Notification> notification;

        public System.Collections.Generic.List<Notification> Notification
        {
            get
            {
                if (notification == null)
                    notification = new System.Collections.Generic.List<Notification>();
                return notification;
            }
            set
            {
                RemoveAllNotification();
                if (value != null)
                {
                    foreach (Notification oNotification in value)
                        AddNotification(oNotification);
                }
            }
        }

        public void AddNotification(Notification newNotification)
        {
            if (newNotification == null)
                return;
            if (this.notification == null)
                this.notification = new System.Collections.Generic.List<Notification>();
            if (!this.notification.Contains(newNotification))
                this.notification.Add(newNotification);
        }

        public void RemoveNotification(Notification oldNotification)
        {
            if (oldNotification == null)
                return;
            if (this.notification != null)
                if (this.notification.Contains(oldNotification))
                    this.notification.Remove(oldNotification);
        }

        public void RemoveAllNotification()
        {
            if (notification != null)
                notification.Clear();
        }

    }
}