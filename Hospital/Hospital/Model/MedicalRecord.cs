using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        public List<String> Alergies { get; set; }

        public System.Collections.Generic.List<Prescription> Prescription { get; set; }

        public System.Collections.Generic.List<Anamnesis> Anamnesis { get; set; }

        public MedicalRecord()
        {
            this.Alergies = new List<string>();
            this.Prescription = new List<Prescription>();
            this.Anamnesis = new List<Anamnesis>();
        }
    }
}