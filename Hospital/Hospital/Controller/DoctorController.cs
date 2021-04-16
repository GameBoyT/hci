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