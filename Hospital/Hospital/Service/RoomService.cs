using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        public Repository.RoomRepository roomRepository = new Repository.RoomRepository();

        public List<Room> GetAll()
        {
            return roomRepository.GetAll();
        }

        public Room GetById(int id)
        {
            return roomRepository.GetById(id);
        }

        public Room GetByName(String name)
        {
            return roomRepository.GetByName(name);
        }

        public void Save(String name, RoomType roomType, int floor, String detail)
        {
            int id = GenerateNewId();
            Room room = new Room(id, name, roomType, floor, detail);
            roomRepository.Save(room);
        }

        public void Delete(int id)
        {
            roomRepository.Delete(id);
        }

        public void Update(Room room)
        {
            roomRepository.Update(room);   
        }

        public int GenerateNewId()
        {
            return roomRepository.GenerateNewId();
        }

        public void MoveEquipment(Model.Room fromRoom, Model.Room toRoom, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}