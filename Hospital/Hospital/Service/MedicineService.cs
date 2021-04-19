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

        public Medicine GetByName(string name)
        {
            return medicineRepository.GetByName(name);
        }

        public void Save(string name)
        {
            int id = medicineRepository.GenerateNewId();
            Medicine medicine = new Medicine(id, name);
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