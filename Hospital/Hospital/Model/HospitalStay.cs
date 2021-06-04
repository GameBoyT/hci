using System;

namespace Model
{
    public class HospitalStay
    {
        public HospitalStay(StaticEquipment bed, DateTime startDateTime, DateTime endDateTme)
        {
            Bed = bed;
            StartDateTime = startDateTime;
            EndDateTime = endDateTme;
        }
        public HospitalStay() { }

        public StaticEquipment Bed { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
