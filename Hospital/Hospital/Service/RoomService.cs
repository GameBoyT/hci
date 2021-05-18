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
            Room room = new Room(id, name, roomType, floor, detail, true);
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

        public void Renovation(int roomId, DateTime renovationDate)
        {
            if (renovationDate == DateTime.Today)
            {
                Room room = GetById(roomId);
                room.Status = false;
                Update(room);

            }

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
            if (time.Ticks <= DateTime.Now.Ticks){
                StaticEquipment staticEquipment = staticRepository.GetById(staticId);
                Room room = GetById(staticEquipment.RoomId);

                room.StaticEquipments.Remove(staticEquipment);
                Room room2 = GetById(toRoom);

                staticEquipment.RoomId = room2.Id;
                room2.StaticEquipments.Add(staticEquipment);
                staticRepository.Update(staticEquipment);

                Update(room);
                Update(room2);
            }
        }


        public void AttachRooms(int roomAId, int roomBId)
        {
            //isRoomAvailable
            Room roomA = GetById(roomAId);
            Room roomB = GetById(roomBId);


            ExtractEquipment(roomB, roomA);
            roomA.Name = "RoomC";

            Update(roomA);
            Delete(roomBId);
        }

        public void ExtractEquipment(Room fromRoom, Room toRoom)
        {
            foreach (StaticEquipment staticEquipment in fromRoom.StaticEquipments)
                toRoom.StaticEquipments.Add(staticEquipment);
        }

        public void DettachRooms(int roomId)
        {
            //isRoomAvailable
            Room roomA = GetById(roomId);

            Save(roomA.Name + "-A", roomA.RoomType, roomA.Floor, roomA.Detail);
        }
    }
}