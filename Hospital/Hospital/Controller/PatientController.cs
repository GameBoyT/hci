using Model;
using System;
using System.Collections.Generic;


namespace Controller
{


    public class PatientController
    {
        private Service.PatientService patientService = new Service.PatientService();

        public List<Patient> GetAll()
        {
            return patientService.GetAll();
        }

        public Model.Patient GetByJmbg(String jmbg)
        {
            return patientService.GetByJmbg(jmbg);
        }

        public void Save(Model.Patient patient)
        {
            patientService.Save(patient);
        }

        public void Delete(String jmbg)
        {
            patientService.Delete(jmbg);
        }

        public void Update(Model.Patient patient)
        {
            patientService.Update(patient);
        }
        public Anamnesis AddAnamnesis(string jmbg, string name, string type, string description)
        {
            return patientService.AddAnamnesis(jmbg, name, type, description);
        }

        public Anamnesis UpdateAnamnesisDescription(string jmbg, int id, string description)
        {
            return patientService.UpdateAnamnesisDescription(jmbg, id, description);
        }

        public string CheckForNotification(Patient patient)
        {
            return patientService.CheckForNotification(patient);
        }

        public string AntiTrollCheck(int appointmentId)
        {
            return patientService.AntiTrollCheck(appointmentId);
        }

        public Prescription AddPrescription(string jmbg, Medicine medicine, int quantity, string description)
        {
            return patientService.AddPrescription(jmbg, medicine, quantity, description);
        }

        internal Referral AddReferral(string patientJmbg, string doctorJmbg, string description)
        {
            return patientService.AddReferral(patientJmbg, doctorJmbg, description);
        }
    }
}