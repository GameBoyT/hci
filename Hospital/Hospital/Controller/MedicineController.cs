using Model;
using Repository;
using Repository.Interfaces;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Controller
{
    public class MedicineController
    {
        private readonly MedicineService _medicineService;

        public MedicineController(IMedicineRepository medicineRepository)
        {
            _medicineService = new MedicineService(medicineRepository);
        }

        public MedicineController()
        {
            MedicineRepository medicineRepository = new MedicineRepository();
            _medicineService = new MedicineService(medicineRepository);
        }

        public List<Medicine> GetAll()
        {
            return _medicineService.GetAll();
        }

        public List<Medicine> GetByVerification(VerificationType verification)
        {
            return _medicineService.GetByVerification(verification);
        }


        public List<Medicine> GetNotRejected()
        {
            return _medicineService.GetNotRejected();
        }

        public Medicine GetById(int id)
        {
            return _medicineService.GetById(id);
        }

        public Medicine GetByName(string name)
        {
            return _medicineService.GetByName(name);
        }

        public void Save(string name, string description)
        {
            _medicineService.Save(name, description);
        }

        public void RejectMedicine(int id, string doctorComment)
        {
            _medicineService.RejectMedicine(id, doctorComment);
        }

        public void Delete(int id)
        {
            _medicineService.Delete(id);
        }

        public void Update(int id, string name, VerificationType verification, string description)
        {
            _medicineService.Update(id, name, verification, description);
        }

        public int GenerateNewId()
        {
            return _medicineService.GenerateNewId();
        }
    }
}