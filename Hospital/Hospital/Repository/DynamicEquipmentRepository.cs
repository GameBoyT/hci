using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Interfaces;

namespace Repository
{
    public class DynamicEquipmentRepository : GenericRepository<DynamicEquipment>, IDynamicEquipmentRepository
    {
        private readonly string _spisak = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\dynamicTransfer.txt";
        public DynamicEquipmentRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\dynamicEquipments.json";
            ReadJson();
        }

        public void MoveDynamicEquipment(DynamicEquipment dynamicEquipment)
        {
            DynamicEquipment dynamic = _objects.Find(obj => obj.Id == dynamicEquipment.Id);
            dynamic.Quantity -= dynamicEquipment.Quantity;
            Update(dynamic);
            WriteToJson();

            string lines = "Extracted dynamic equipment: " + Convert.ToString(dynamicEquipment.Quantity) + " " + dynamicEquipment.Name + "\n";
            File.AppendAllText(_spisak, lines);
        }
        public List<DynamicEquipment> GetByName(string name)
        {
            return _objects.FindAll(obj => obj.Name == name);
        }
    }
}