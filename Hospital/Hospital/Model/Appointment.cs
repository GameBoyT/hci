using System;

namespace Model
{
    public class Appointment
    {
        public int Id
        {
            get
            ;
            set
            ;
        }

        public DateTime StartTime
        {
            get
            ;
            set
            ;
        }

        public Double DurationInMinutes
        {
            get
            ;
            set
            ;
        }

        public Doctor Doctor { get; set; }


        public Patient Patient { get; set; }


        public Room Room { get; set; }



    }
}