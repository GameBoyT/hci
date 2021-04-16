using System;

namespace Model
{
    public class Prescription
    {
        public int Interval
        {
            get
            ;
            set
            ;
        }

        public DateTime StartDate
        {
            get
            ;
            set
            ;
        }

        public DateTime EndDate
        {
            get
            ;
            set
            ;
        }

        public String Description
        {
            get
            ;
            set
            ;
        }

        public int Quantity
        {
            get
            ;
            set
            ;
        }

        public Medicine Medicine { get; set; }

    }
}