using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        private List<String> alergies;

        public System.Collections.Generic.List<Prescription> prescription;

        public System.Collections.Generic.List<Prescription> Prescription
        {
            get
            {
                if (prescription == null)
                    prescription = new System.Collections.Generic.List<Prescription>();
                return prescription;
            }
            set
            {
                RemoveAllPrescription();
                if (value != null)
                {
                    foreach (Prescription oPrescription in value)
                        AddPrescription(oPrescription);
                }
            }
        }

        public void AddPrescription(Prescription newPrescription)
        {
            if (newPrescription == null)
                return;
            if (this.prescription == null)
                this.prescription = new System.Collections.Generic.List<Prescription>();
            if (!this.prescription.Contains(newPrescription))
                this.prescription.Add(newPrescription);
        }

        public void RemovePrescription(Prescription oldPrescription)
        {
            if (oldPrescription == null)
                return;
            if (this.prescription != null)
                if (this.prescription.Contains(oldPrescription))
                    this.prescription.Remove(oldPrescription);
        }

        public void RemoveAllPrescription()
        {
            if (prescription != null)
                prescription.Clear();
        }
        public System.Collections.Generic.List<Anamnesis> anamnesis;

        public System.Collections.Generic.List<Anamnesis> Anamnesis
        {
            get
            {
                if (anamnesis == null)
                    anamnesis = new System.Collections.Generic.List<Anamnesis>();
                return anamnesis;
            }
            set
            {
                RemoveAllAnamnesis();
                if (value != null)
                {
                    foreach (Anamnesis oAnamnesis in value)
                        AddAnamnesis(oAnamnesis);
                }
            }
        }

        public void AddAnamnesis(Anamnesis newAnamnesis)
        {
            if (newAnamnesis == null)
                return;
            if (this.anamnesis == null)
                this.anamnesis = new System.Collections.Generic.List<Anamnesis>();
            if (!this.anamnesis.Contains(newAnamnesis))
                this.anamnesis.Add(newAnamnesis);
        }

        public void RemoveAnamnesis(Anamnesis oldAnamnesis)
        {
            if (oldAnamnesis == null)
                return;
            if (this.anamnesis != null)
                if (this.anamnesis.Contains(oldAnamnesis))
                    this.anamnesis.Remove(oldAnamnesis);
        }

        public void RemoveAllAnamnesis()
        {
            if (anamnesis != null)
                anamnesis.Clear();
        }

    }
}