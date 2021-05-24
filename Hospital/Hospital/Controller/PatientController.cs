using Model;
using System;
using System.Collections.Generic;
using DTO;

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

        public void CheckForMedicineNotification(Patient patient)
        {
            patientService.CheckForMedicineNotification(patient);
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

        public void ClearNotification()
        {

        }

        public List<Anamnesis> GetAllAnamnesisForPatient(string patientJmbg)
        {
            return patientService.GetAllAnamnesisForPatient(patientJmbg);
        }
       
        public List<PrescriptionDTO> GetAllPrescriptionsForPatient(string patientJmbg)
        {
            return patientService.GetAllPresctiptionsForPatient(patientJmbg);
        }

        public void AddReminder(string jmbg, Reminder reminder)
        {
            patientService.AddReminder(jmbg, reminder);
        }

        public List<Reminder>GettAllReminders(string jmbg)
        {
            return patientService.GetAllReminders(jmbg);
        }

        public void DeleteRemider(string jmbg, int id)
        {
            patientService.DeleteReminder(jmbg, id);
        }

        public void UpdateReminder(string jmbg, Reminder reminder)
        {
            patientService.UpdateReminder(jmbg, reminder);
        }

        public void CheckForReminder(string jmbg)
        {
            patientService.CheckForReminder(jmbg);
        }

        public void AddHospitalStay(string jmbg, StaticEquipment bed, DateTime startDateTime, DateTime endDateTime)
        {
            patientService.AddHospitalStay(jmbg, bed, startDateTime, endDateTime);
        }

        public void ExtendHospitalStay(string jmbg, DateTime extendedDate)
        {
            patientService.ExtendHospitalStay(jmbg, extendedDate);
        }
    }
}