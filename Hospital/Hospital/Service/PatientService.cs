using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service
{
    public class PatientService
    {
     
        public Repository.PatientRepository patientRepository = new Repository.PatientRepository();
        public Repository.AppointmentRepository appointmentRepository = new Repository.AppointmentRepository();
        public List<Patient> GetAll()
        {
            return patientRepository.GetAll();
        }

        public Model.Patient GetByJmbg(String jmbg)
        {
            return patientRepository.GetByJmbg(jmbg);
        }

        public void Save(Model.Patient patient)
        {
            List<Patient> patients = patientRepository.GetAll();
            foreach(Patient patient1 in patients)
            {
                if(patient1.User.Jmbg == patient.User.Jmbg)
                    MessageBox.Show("Isti jmbg", "greska");
            }
            patientRepository.Save(patient);
        }

        public void Delete(String jmbg)
        {
            patientRepository.Delete(jmbg);
        }

        public void Update(Model.Patient patient)
        {
            patientRepository.Update(patient);
            List<Appointment> appointments = appointmentRepository.GetAppointmentsForPatient(patient.User.Jmbg);
            foreach (Appointment appointment in appointments)
            {
                appointment.Patient = patient;
                appointmentRepository.Update(appointment);
            }
        }

        public void AddAnamnesis(string jmbg, string name, string type, string description)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            int id = patientRepository.GenerateNewAnamnesisId();
            Anamnesis anamnesis = new Anamnesis(id, type, name, description);
            patient.MedicalRecord.Anamnesis.Add(anamnesis);
            Update(patient);
        }

        public void UpdateAnamnesisDescription(string jmbg, int id, string description)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            Anamnesis anamnesis = patient.MedicalRecord.Anamnesis.Find(obj => obj.Id == id);
            anamnesis.Description = description;
            Update(patient);
        }

        //public void AddPrescription(string jmbg, Prescription prescription)
        //{
        //    Patient patient = patientRepository.GetByJmbg(jmbg);
        //    int id = patientRepository.GenerateNewAnamnesisId();
        //    Anamnesis anamnesis = new Anamnesis(id, type, name, description);
        //    patient.MedicalRecord.Anamnesis.Add(anamnesis);
        //    Update(patient);
        //}
    }
}