using Model;
using Repository.Interfaces;
using System.Collections.Generic;

namespace Service
{
    public class MedicineService
    {
        private IMedicineRepository _medicineRepository;

        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public List<Medicine> GetAll()
        {
            return _medicineRepository.GetAll();
        }

        public List<Medicine> GetByVerification(VerificationType verification)
        {
            return _medicineRepository.GetByVerification( verification);
        }
        public List<Medicine> GetNotRejected()
        {
            return _medicineRepository.GetNotRejected();
        }

        public Medicine GetById(int id)
        {
            return _medicineRepository.GetById(id);
        }

        public Medicine GetByName(string name)
        {
            return _medicineRepository.GetByName(name);
        }

        public void Save(string name, string description)
        {
            int id = _medicineRepository.GenerateNewId();
            Medicine medicine = new Medicine(id, name, description, "")
            {
                Verification = VerificationType.needsVerification
            };
            _medicineRepository.Save(medicine);
        }

        public void Delete(int id)
        {
            _medicineRepository.Delete(id);
        }

        public void Update(int id, string name, VerificationType verification, string description)
        {
            Medicine medicine = GetById(id);
            medicine.Name = name;
            medicine.Verification = verification;
            medicine.Description = description;
            _medicineRepository.Update(medicine);
        }
        public void RejectMedicine(int id, string doctorComment)
        {
            Medicine medicine = GetById(id);
            medicine.Verification = VerificationType.rejected;
            medicine.DoctorComment = doctorComment;
            _medicineRepository.Update(medicine);
        }
        public int GenerateNewId()
        {
            return _medicineRepository.GenerateNewId();
        }
    }
}