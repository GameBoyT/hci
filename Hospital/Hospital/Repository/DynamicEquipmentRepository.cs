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
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\dynamic.json";
        private List<DynamicEquipment> dynamics = new List<DynamicEquipment>();
        
        public DynamicEquipmentRepository()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            using StreamReader r = new StreamReader(fileLocation);
            string json = r.ReadToEnd();
            if (json != "")
            {
                dynamics = JsonConvert.DeserializeObject<List<DynamicEquipment>>(json);
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(dynamics);
            File.WriteAllText(fileLocation, json);
        }

        public List<DynamicEquipment> GetAll()
        {
            return dynamics;
        } 
         
        public DynamicEquipment GetById(int id)
        {
            return dynamics.Find(obj => obj.Id == id);
        }

        public int GenerateNewId()
        {
            try
            {
                int maxId = dynamics.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }
        public void Save(DynamicEquipment dynamicEquipment)
        {
            dynamics.Add(dynamicEquipment);
            WriteToJson();
        }
      
        public void Delete(int id)
        {
            int index = dynamics.FindIndex(obj => obj.Id == id);
            dynamics.RemoveAt(index);
            WriteToJson();
        }
      
        public void Update(DynamicEquipment dynamicEquipment)
        {
            int index = dynamics.FindIndex(obj => obj.Id == dynamicEquipment.Id);
            dynamics[index] = dynamicEquipment;
            WriteToJson();
        }
   
   }
}