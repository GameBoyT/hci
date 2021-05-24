using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        public HospitalStay HospitalStay { get; set; }

        public List<String> Alergies { get; set; }

        public List<Prescription> Prescription { get; set; }

        public List<Anamnesis> Anamnesis { get; set; }

        public List<Referral> Referrals { get; set; }

        public MedicalRecord()
        {
            this.HospitalStay = new HospitalStay();
            this.Alergies = new List<string>();
            this.Prescription = new List<Prescription>();
            this.Anamnesis = new List<Anamnesis>();
            this.Referrals = new List<Referral>();
        }
    }
}