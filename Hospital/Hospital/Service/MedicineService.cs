using Model;
using System.Collections.Generic;

namespace Service
{
    public class MedicineService
    {
        private Repository.MedicineRepository medicineRepository = new Repository.MedicineRepository();

        public List<Medicine> GetAll()
        {
            return medicineRepository.GetAll();
        }

        public Medicine GetById(int id)
        {
            return medicineRepository.GetById(id);
        }

        public void Save(Medicine medicine)
        {
            medicineRepository.Save(medicine);
        }

        public void Delete(int id)
        {
            medicineRepository.Delete(id);
        }

        public void Update(Medicine medicine)
        {
            medicineRepository.Update(medicine);
        }
    }
}