using System;

namespace Model
{
    public class RenovationAppointment : Appointment
    {
        public RenovationAppointment(int id, DateTime startTime, Double durationInMinutes, int roomId, string description) : base (id, startTime, durationInMinutes, roomId)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}