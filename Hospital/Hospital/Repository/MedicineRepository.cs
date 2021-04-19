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
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicine.json";
        private List<Medicine> medicines = new List<Medicine>();

        public MedicineRepository()
        {
            ReadJson();
        }


        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(medicines);
            File.WriteAllText(fileLocation, json);
        }

        public void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    medicines = JsonConvert.DeserializeObject<List<Medicine>>(json);
                }
            }
        }

        public List<Medicine> GetAll()
        {
            ReadJson();
            return medicines;
        }

        public Medicine GetById(int id)
        {
            ReadJson();
            return medicines.Find(obj => obj.Id == id);
        }
        public Medicine GetByName(string name)
        {
            return medicines.Find(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public int GenerateNewId()
        {
            ReadJson();
            try
            {
                int maxId = medicines.Max(obj => obj.Id);
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
            medicines.Add(medicine);
            WriteToJson();
        }

        public void Delete(int id)
        {
            ReadJson();
            int index = medicines.FindIndex(obj => obj.Id == id);
            medicines.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Medicine medicine)
        {
            ReadJson();
            int index = medicines.FindIndex(obj => obj.Id == medicine.Id);
            medicines[index] = medicine;
            WriteToJson();
        }

    }
}