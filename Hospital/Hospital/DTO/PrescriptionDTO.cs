using System;

namespace DTO
{
    public class PrescriptionDTO
    {
        public String MedicineName { get; set; }
        public int Interval { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }

        public PrescriptionDTO(String medicineName, int interval, DateTime start, DateTime end)
        {
            MedicineName = medicineName;
            Interval = interval;
            EndDate = end;
            StartDate = start;
        }



    }
}
