using System;

namespace Model
{
    public class Appointment : Entity
    {
        public Appointment(int id, DateTime startTime, Double durationInMinutes, int roomId)
        {
            this.Id = id;
            this.StartTime = startTime;
            this.DurationInMinutes = durationInMinutes;
            this.RoomId = roomId;
        }

        public Appointment(DateTime startTime, Double durationInMinutes, int roomId)
        {
            this.StartTime = startTime;
            this.DurationInMinutes = durationInMinutes;
            this.RoomId = roomId;
        }

        public Appointment()
        {

        }

        public DateTime StartTime { get; set; }

        public Double DurationInMinutes { get; set; }

        public int RoomId { get; set; }

    }
}