using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class DoctorController
    {
        private Service.DoctorService doctorService = new DoctorService();

        

        public List<Doctor> GetAll()
        {
            return doctorService.GetAll();
        }

        public Model.Doctor GetByJmbg(String jmbg)
        {
            return doctorService.GetByJmbg(jmbg);
        }

        public void Save(Model.Doctor doctor)
        {
            doctorService.Save(doctor);
        }

        public void Delete(String jmbg)
        {
            doctorService.Delete(jmbg);
        }

        public void Update(Model.Doctor doctor)
        {
            doctorService.Update(doctor);
        }


    }
}