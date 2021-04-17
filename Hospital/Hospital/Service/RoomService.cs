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
            //int id = GenerateNewId();
            Room room = new Room(1, name, roomType, floor, detail);
            roomRepository.Save(room);
        }

        public void Delete(int id)
        {
            roomRepository.Delete(id);
        }

        public void Update(String name, RoomType roomType, int floor, String detail)
        {
            //appointmentRepository.Update(appointment);

            throw new NotImplementedException();
        }

        public int GenerateNewId()
        {
            return roomRepository.GenerateNewId();
        }

    }
}