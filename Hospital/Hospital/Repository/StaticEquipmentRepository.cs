using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class StaticEquipmentRepository
    {
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\staticEquipments.json";
        private List<StaticEquipment> _staticEquipments = new List<StaticEquipment>();

        public StaticEquipmentRepository()
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
                    _staticEquipments = JsonConvert.DeserializeObject<List<StaticEquipment>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_staticEquipments, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<StaticEquipment> GetAll()
        {
            return _staticEquipments;
        }

        public Model.StaticEquipment GetById(int id)
        {
            return _staticEquipments.Find(obj => obj.Id == id);
        }

        public int GenerateNewId()
        {
            try
            {
                int maxId = _staticEquipments.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Save(Model.StaticEquipment staticEquipment)
        {
            _staticEquipments.Add(staticEquipment);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = _staticEquipments.FindIndex(obj => obj.Id == id);
            _staticEquipments.RemoveAt(index);
            WriteToJson();
        }

        public void Update(StaticEquipment staticEquipment)
        {
            int index = _staticEquipments.FindIndex(obj => obj.Id == staticEquipment.Id);
            _staticEquipments[index] = staticEquipment;
            WriteToJson();
        }

    }
}