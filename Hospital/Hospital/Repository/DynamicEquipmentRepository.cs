using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class DynamicEquipmentRepository
    {
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\dynamicEquipments.json";
        private readonly string _spisak = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\dynamicTransfer.txt";
        private List<DynamicEquipment> _dynamicEquipments = new List<DynamicEquipment>();

        public DynamicEquipmentRepository()
        {
            ReadJson();
        }

        public void ReadJson()
        {
            if (!File.Exists(_fileLocation))
            {
                File.Create(_fileLocation).Close();
            }

            using StreamReader r = new StreamReader(_fileLocation);
            string json = r.ReadToEnd();
            if (json != "")
            {
                _dynamicEquipments = JsonConvert.DeserializeObject<List<DynamicEquipment>>(json);
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_dynamicEquipments, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<DynamicEquipment> GetAll()
        {
            return _dynamicEquipments;
        }

        public DynamicEquipment GetById(int id)
        {
            return _dynamicEquipments.Find(obj => obj.Id == id);
        }

        public int GenerateNewId()
        {
            try
            {
                int maxId = _dynamicEquipments.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }
        public void Save(DynamicEquipment dynamicEquipment)
        {
            _dynamicEquipments.Add(dynamicEquipment);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = _dynamicEquipments.FindIndex(obj => obj.Id == id);
            _dynamicEquipments.RemoveAt(index);
            WriteToJson();
        }

        public void Update(DynamicEquipment dynamicEquipment)
        {
            int index = _dynamicEquipments.FindIndex(obj => obj.Id == dynamicEquipment.Id);
            _dynamicEquipments[index] = dynamicEquipment;
            WriteToJson();
        }
        public void MoveDynamicEquipment(DynamicEquipment dynamicEquipment)
        {
            DynamicEquipment dynamic = _dynamicEquipments.Find(obj => obj.Id == dynamicEquipment.Id);
            dynamic.Quantity -= dynamicEquipment.Quantity;
            Update(dynamic);
            WriteToJson();

            string lines = "Extracted dynamic equipment: " + Convert.ToString(dynamicEquipment.Quantity) + " " + dynamicEquipment.Name + "\n";
            File.AppendAllText(_spisak, lines);
        }
        public List<DynamicEquipment> GetByName(string name)
        {
            return _dynamicEquipments.FindAll(obj => obj.Name == name);
        }
    }
}