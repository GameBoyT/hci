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
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\staticEquipment.json";
        private List<StaticEquipment> staticEquipments = new List<StaticEquipment>();

        public StaticEquipmentRepository()
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
                    staticEquipments = JsonConvert.DeserializeObject<List<StaticEquipment>>(json);
                }
            }
        }
        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(staticEquipments);
            File.WriteAllText(fileLocation, json);
        }

        public List<StaticEquipment> GetAll()
        {
            return staticEquipments;
        }

        public List<StaticEquipment> GetAllRoomsWithEquipmentName(string name)
        {
            return staticEquipments.FindAll(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public Model.StaticEquipment GetById(int id)
        {
            return staticEquipments.Find(obj => obj.Id == id);
        }

        public int GenerateNewId()
        {
            try
            {
                int maxId = staticEquipments.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Save(Model.StaticEquipment staticEquipment)
        {
            staticEquipments.Add(staticEquipment);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = staticEquipments.FindIndex(obj => obj.Id == id);
            staticEquipments.RemoveAt(index);
            WriteToJson();
        }

        public void Update(StaticEquipment staticEquipment)
        {
            int index = staticEquipments.FindIndex(obj => obj.Id == staticEquipment.Id);
            staticEquipments[index] = staticEquipment;
            WriteToJson();
        }

    }
}