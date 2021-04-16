using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class MedicineController
    {
        private Service.MedicineService medicineService = new Service.MedicineService();


        public List<Medicine> GetAll()
        {
            return medicineService.GetAll();
        }

        public Medicine GetById(int id)
        {
            return medicineService.GetById(id);
        }

        public void Save(Medicine medicine)
        {
            medicineService.Save(medicine);
        }

        public void Delete(int id)
        {
            medicineService.Delete(id);
        }

        public void Update(Medicine medicine)
        {
            medicineService.Update(medicine);
        }

        

    }
}