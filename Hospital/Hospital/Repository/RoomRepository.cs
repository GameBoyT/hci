using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Interfaces;

namespace Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\rooms.json";
            ReadJson();
        }

        public Room GetByName(String name)
        {
            return _objects.Find(obj => obj.Name == name);
        }    

        public List<Room> GetOperationRooms()
        {
            return _objects.FindAll(obj => obj.RoomType == RoomType.operating);
        }

        public List<Room> GetRoomsWithEquipmentName(string name)
        {
            List<Room> roomsToReturn = new List<Room>();
            foreach (Room room in _objects)
            {
                if (room.StaticEquipments.Exists(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase)))
                    roomsToReturn.Add(room);
            }
            return roomsToReturn;
        }
    }
}