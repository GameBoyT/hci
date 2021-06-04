using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

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

        public List<Room> GetRoomsByRoomType(RoomType roomType)
        {
            return _objects.FindAll(obj => obj.RoomType == roomType);
        }
    }
}