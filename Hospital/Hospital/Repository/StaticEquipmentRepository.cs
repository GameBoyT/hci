using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class StaticEquipmentRepository : GenericRepository<StaticEquipment>, IStaticEquipmentRepository
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