using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        private List<String> Alergies { get; set; }

        public System.Collections.Generic.List<Prescription> Prescription { get; set; }

        public System.Collections.Generic.List<Anamnesis> Anamnesis { get; set; }
    }
}