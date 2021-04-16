using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class RoomController
    {
        public Service.RoomService roomService;


        public List<Room> GetAll()
        {
            return roomService.GetAll();
        }

        public Room GetById(int id)
        {
            return roomService.GetById(id);
        }

        public void Save(int id, String name, RoomType roomType, int floor, String detail)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            roomService.Delete(id);
        }

        public void Update(String name, RoomType roomType, int floor, String detail)
        {
            throw new NotImplementedException();
        }


    }
}