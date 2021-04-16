using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        public List<Room> GetAll()
        {
            throw new NotImplementedException();
        }

        public Room GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(int id, String name, RoomType roomType, int floor, String detail)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(String name, RoomType roomType, int floor, String detail)
        {
            throw new NotImplementedException();
        }

        public Repository.RoomRepository roomRepository;

    }
}