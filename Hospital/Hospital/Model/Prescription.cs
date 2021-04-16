using System;

namespace Model
{
    public class Prescription
    {
        public int interval
        {
            get
            ;
            set
            ;
        }

        public DateTime startDate
        {
            get
            ;
            set
            ;
        }

        public DateTime endDate
        {
            get
            ;
            set
            ;
        }

        public String description
        {
            get
            ;
            set
            ;
        }

        public int quantity
        {
            get
            ;
            set
            ;
        }

        public Medicine medicine;

        public Medicine Medicine
        {
            get
            {
                return medicine;
            }
            set
            {
                this.medicine = value;
            }
        }

    }
}