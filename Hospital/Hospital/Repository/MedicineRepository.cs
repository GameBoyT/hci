using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class MedicineRepository
    {
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicines.json";
        private List<Medicine> _medicines = new List<Medicine>();

        public MedicineRepository()
        {
            ReadJson();
        }

        public void ReadJson()
        {
            if (!File.Exists(_fileLocation))
            {
                File.Create(_fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(_fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    _medicines = JsonConvert.DeserializeObject<List<Medicine>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_medicines, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Medicine> GetAll()
        {
            ReadJson();
            return _medicines;
        }

        // napisati 1 fkju koja vraca po nekoj verifikaciji a ne 4 posebne
        
        public List<Medicine> GetVerified()
        {
            ReadJson();
            return _medicines.FindAll(obj => obj.Verification == VerificationType.verified);
        }
        public List<Medicine> GetRejected()
        {
            ReadJson();
            return _medicines.FindAll(obj => obj.Verification == VerificationType.rejected);
        }

        public List<Medicine> GetNotRejected()
        {
            ReadJson();
            return _medicines.FindAll(obj => obj.Verification != VerificationType.rejected);
        }
        public List<Medicine> GetNotVerified()
        {
            ReadJson();
            return _medicines.FindAll(obj => obj.Verification == VerificationType.needsVerification);
        }

        public Medicine GetById(int id)
        {
            ReadJson();
            return _medicines.Find(obj => obj.Id == id);
        }

        public Medicine GetByName(string name)
        {
            ReadJson();
            return _medicines.Find(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public int GenerateNewId()
        {
            ReadJson();
            try
            {
                int maxId = _medicines.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Save(Medicine medicine)
        {
            ReadJson();
            _medicines.Add(medicine);
            WriteToJson();
        }

        public void Delete(int id)
        {
            ReadJson();
            int index = _medicines.FindIndex(obj => obj.Id == id);
            _medicines.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Medicine medicine)
        {
            ReadJson();
            int index = _medicines.FindIndex(obj => obj.Id == medicine.Id);
            _medicines[index] = medicine;
            WriteToJson();
        }

    }
}