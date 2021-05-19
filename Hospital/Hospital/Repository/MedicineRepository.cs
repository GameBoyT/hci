using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Interfaces;

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
    }
}