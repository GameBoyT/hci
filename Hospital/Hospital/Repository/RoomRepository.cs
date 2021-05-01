using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class RoomRepository
    {
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\rooms.json";
        private List<Room> _rooms = new List<Room>();

        public RoomRepository()
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
                    _rooms = JsonConvert.DeserializeObject<List<Room>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_rooms, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Room> GetAll()
        {
            return _rooms;
        }

        public Room GetById(int id)
        {
            return _rooms.Find(obj => obj.Id == id);
        }

        public Room GetByName(String name)
        {
            return _rooms.Find(obj => obj.Name == name);
        }

        public int GenerateNewId()
        {
            try
            {
                int maxId = _rooms.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Save(Room room)
        {
            _rooms.Add(room);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = _rooms.FindIndex(obj => obj.Id == id);
            _rooms.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Room room)
        {
            int index = _rooms.FindIndex(obj => obj.Id == room.Id);
            _rooms[index] = room;
            WriteToJson();
        }

        public void MoveEquipment(Model.Room fromRoom, Model.Room toRoom, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetOperationRooms()
        {
            return _rooms.FindAll(obj => obj.RoomType == RoomType.operating);
        }

        public List<Room> GetRoomsWithEquipmentName(string name)
        {
            List<Room> roomsToReturn = new List<Room>();
            foreach (Room room in _rooms)
            {
                if (room.StaticEquipments.Exists(obj => string.Equals(obj.Name, name, StringComparison.OrdinalIgnoreCase)))
                    roomsToReturn.Add(room);
            }
            return roomsToReturn;
        }

    }
}