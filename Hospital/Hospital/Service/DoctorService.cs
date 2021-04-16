using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class DoctorService
    {
        private Repository.DoctorRepository _doctorRepository = new Repository.DoctorRepository();

        public List<Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public Model.Doctor GetByJmbg(String jmbg)
        {
            throw new NotImplementedException();
        }

        public void Save(Model.Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void Delete(String jmbg)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Doctor doctor)
        {
            throw new NotImplementedException();
        }


    }
}