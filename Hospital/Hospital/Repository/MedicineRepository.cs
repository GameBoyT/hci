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
        // napisati 1 fkju koja vraca po nekoj verifikaciji a ne 4 posebne
        
        public List<Medicine> GetVerified()
        {
            ReadJson();
            return _objects.FindAll(obj => obj.Verification == VerificationType.verified);
        }
        public List<Medicine> GetRejected()
        {
            ReadJson();
            return _objects.FindAll(obj => obj.Verification == VerificationType.rejected);
        }

        public List<Medicine> GetNotRejected()
        {
            ReadJson();
            return _objects.FindAll(obj => obj.Verification != VerificationType.rejected);
        }
        public List<Medicine> GetNotVerified()
        {
            ReadJson();
            return _objects.FindAll(obj => obj.Verification == VerificationType.needsVerification);
        }

        public Medicine GetByName(string name)
        {
            ReadJson();
            return _objects.Find(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}