using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service
{
    public class PatientService
    {
     
        public Repository.PatientRepository patientRepository = new Repository.PatientRepository();
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
        }



    }
}