using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class RoomController
    {
        public Service.RoomService roomService = new Service.RoomService();


        public List<Room> GetAll()
        {
            return roomService.GetAll();
        }

        public Room GetById(int id)
        {
            return roomService.GetById(id);
        }

        public Room GetByName(String name)
        {
            return roomService.GetByName(name);
        }

        public void Save(String name, RoomType roomType, int floor, String detail)
        {
            roomService.Save(name, roomType, floor, detail);
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