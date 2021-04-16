using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class DoctorService
    {
        private Repository.DoctorRepository doctorRepository = new Repository.DoctorRepository();
        public List<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        public Model.Doctor GetByJmbg(String jmbg)
        {
            return doctorRepository.GetByJmbg(jmbg);
        }

        public void Save(Model.Doctor doctor)
        {
            doctorRepository.Save(doctor);
        }

        public void Delete(String jmbg)
        {
            doctorRepository.Delete(jmbg);
        }

        public void Update(Model.Doctor doctor)
        {
            doctorRepository.Update(doctor);
        }


    }
}