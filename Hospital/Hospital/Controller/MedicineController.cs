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

        public List<Medicine> GetNotRejected()
        {
            return medicineService.GetNotRejected();
        }

        public Medicine GetById(int id)
        {
            return medicineService.GetById(id);
        }

        public Medicine GetByName(string name)
        {
            return medicineService.GetByName(name);
        }

        public void Save(string name, string description)
        {
            medicineService.Save(name, description);
        }

        public void Delete(int id)
        {
            medicineService.Delete(id);
        }

        public void Update(int id, string name, VerificationType verification, string description)
        {
            medicineService.Update(id, name, verification, description);
        }
    }
}