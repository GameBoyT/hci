using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {

        
        public List<String> Alergies { get; set; }

        public System.Collections.Generic.List<Prescription> Prescription { get; set; }

        public System.Collections.Generic.List<Anamnesis> Anamnesis { get; set; }
    }
}