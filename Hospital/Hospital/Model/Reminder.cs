using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model
{
    public class Reminder
    {
        public String Title { get; set; }
        public DateTime Time { get; set; }
        public DateTime Interval { get; set; }


        public Reminder(String title, DateTime time, DateTime interval)
        {
            Title = title;
            Time = time;
            Interval = interval;
        }

    }

  
}
