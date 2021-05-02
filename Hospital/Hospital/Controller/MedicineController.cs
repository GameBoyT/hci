using Model;
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

        public List<Medicine> GetVerified()
        {
            return medicineService.GetVerified();
        }

        public Medicine GetById(int id)
        {
            return medicineService.GetById(id);
        }

        public Medicine GetByName(string name)
        {
            return medicineService.GetByName(name);
        }

        public void Save(string name)
        {
            medicineService.Save(name);
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