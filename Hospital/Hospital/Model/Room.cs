using System;

namespace Model
{
    public class Room
    {
        public int id
        {
            get
            ;
            set
            ;
        }

        public String name
        {
            get
            ;
            set
            ;
        }

        public RoomType roomType
        {
            get
            ;
            set
            ;
        }

        public int floor
        {
            get
            ;
            set
            ;
        }

        public String detail
        {
            get
            ;
            set
            ;
        }

        public DynamicEquipment[] dynamicEquipment;
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
                newAppointment.Room = this;
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
                    oldAppointment.Room = null;
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
                    oldAppointment.Room = null;
                tmpAppointment.Clear();
            }
        }
        public StaticEquipment[] staticEquipment;

    }
}