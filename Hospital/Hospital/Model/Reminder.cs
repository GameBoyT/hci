using System;

namespace Model
{
    public class Reminder
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTime Time { get; set; }
        public DateTime NotifyTime { get; set; }


        public Reminder(int id, String title, DateTime time, DateTime notifytime)
        {
            Id = id;
            Title = title;
            Time = time;
            NotifyTime = notifytime;
        }

    }


}
