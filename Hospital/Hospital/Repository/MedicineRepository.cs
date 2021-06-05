using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicines.json";
            ReadJson();
        }

        public MedicineRepository(string fileLocation)
        {
            _fileLocation = fileLocation;
            ReadJson();
        }

        public List<Medicine> GetByVerification(VerificationType verification)
        {
            ReadJson();
            return _objects.FindAll(obj => obj.Verification == verification);
        }


        public List<Medicine> GetNotRejected()
        {
            ReadJson();
            return _objects.FindAll(obj => obj.Verification != VerificationType.rejected);
        }


        public Medicine GetByName(string name)
        {
            ReadJson();
            return _objects.Find(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Medicine> GetAvaliableAlternatives(int id)
        {
            ReadJson();
            Medicine medicine = GetById(id);
            List<Medicine> verified = GetByVerification(VerificationType.verified);
            List<Medicine> toReturn = new List<Medicine>();
            foreach (Medicine med in verified)
            {
                if (!medicine.Alternatives.Exists(obj => obj.Id == med.Id))
                {
                    toReturn.Add(med);
                }
            }
            return toReturn;
        }


    }
}