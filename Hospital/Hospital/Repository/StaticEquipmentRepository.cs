using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Interfaces;

namespace Repository
{
    public class StaticEquipmentRepository : GenericRepository<StaticEquipment>
    {
        public StaticEquipmentRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\staticEquipments.json";
            ReadJson();
        }

        public List<StaticEquipment> GetByName(string name)
        {
            return _objects.FindAll(obj => obj.Name == name);
        }

        public List<StaticEquipment> FilterRoomId(int id)
        {
            return _objects.FindAll(obj => obj.RoomId == id);
        }
    }
}