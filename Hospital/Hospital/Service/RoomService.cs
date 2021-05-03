using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        public Repository.RoomRepository roomRepository = new Repository.RoomRepository();
        public Repository.StaticEquipmentRepository staticRepository = new Repository.StaticEquipmentRepository();

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

        //public void AddAppointment(Appointment appointment)
        //{
        //    Room room = GetById(appointment.RoomId);
        //    room.Appointments.Add(appointment);
        //    Update(room);
        //}

        public int GenerateNewId()
        {
            return roomRepository.GenerateNewId();
        }

        public void MoveEquipment(Model.Room fromRoom, Model.Room toRoom, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetOperationRooms()
        {
            return roomRepository.GetOperationRooms();
        }

        public List<Room> GetRoomsWithEquipmentName(string name)
        {
            return roomRepository.GetRoomsWithEquipmentName(name);
        }

        public void MoveStaticEquipment(int staticId, int toRoom, DateTime time)
        {
            if (time.Ticks < DateTime.Now.Ticks){
                StaticEquipment staticEquipment = staticRepository.GetById(staticId);
                Room room = GetById(staticEquipment.RoomId);
                int index = room.StaticEquipments.FindIndex(obj => obj.Id == room.Id);
                room.StaticEquipments.RemoveAt(index);
                Room room2 = GetById(toRoom);
                staticEquipment.RoomId = room2.Id;
                room2.StaticEquipments.Add(staticEquipment);
                staticRepository.Update(staticEquipment);
                Update(room);
                Update(room2);
            }
        }
    }
}