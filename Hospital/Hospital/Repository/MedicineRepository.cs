using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class MedicineRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicine.json";
        private List<Medicine> medicines = new List<Medicine>();

        public MedicineRepository()
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


        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(medicines);
            File.WriteAllText(fileLocation, json);
        }

        public List<Medicine> GetAll()
        {
            return medicines;
        }

        public Medicine GetById(int id)
        {
            return medicines.Find(obj => obj.Id == id);
        }

        public void Save(Medicine medicine)
        {
            medicines.Add(medicine);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = medicines.FindIndex(obj => obj.Id == id);
            medicines.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Medicine medicine)
        {
            int index = medicines.FindIndex(obj => obj.Id == medicine.Id);
            medicines[index] = medicine;
            WriteToJson();
        }

    }
}