using System;

namespace Model
{
    public class Prescription
    {
        public int Interval { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }
        public int Quantity { get; set; }
        public Medicine Medicine { get; set; }


        //public Prescription(int interval, DateTime startDate, DateTime endDate, String description, int quantity, Medicine medicine)
        //{
        //    this.Interval = interval;
        //    this.StartDate = startDate;
        //    this.EndDate = endDate;
        //    this.Description = description;
        //    this.Quantity = quantity;
        //    this.Medicine = medicine;

        //}

        public Prescription(Medicine medicine, int quantity, string description)
        {
            Medicine = medicine;
            Quantity = quantity;
            Description = description;
        }
    }
}