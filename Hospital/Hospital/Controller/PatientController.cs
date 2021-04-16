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


    }
}