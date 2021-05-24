using Model;
using DTO;
using System;
using Repository;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        public readonly RoomRepository roomRepository = new RoomRepository();
        public readonly AppointmentService appointmentService = new AppointmentService();
        public readonly StaticEquipmentRepository staticRepository = new StaticEquipmentRepository();
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

        public List<Room> GetRoomsByRoomType(RoomType roomType)
        {
            return roomRepository.GetRoomsByRoomType(roomType);
        }

        public int GenerateNewId()
        {
            return roomRepository.GenerateNewId();
        }

        public bool Renovation(int roomId, DateTime renovationDate, double duration)
        {
            AppointmentDTO appointment = new AppointmentDTO(renovationDate, duration, roomId);
            if (appointmentService.IsRoomAvailable(appointment)){
                appointmentService.SaveRenovation(appointment);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Room> GetRoomsWithEquipmentName(string name)
        {
            return roomRepository.GetRoomsWithEquipmentName(name);
        }

        public void MoveStaticEquipment(int staticId, int toRoom)
        {
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


        public void AttachRooms(int roomAId, int roomBId)
        {
            Room roomA = GetById(roomAId);
            Room roomB = GetById(roomBId);


            ExtractEquipment(roomB, roomA);
            roomA.Name = roomA.Name + "/" + roomB.Name;

            Update(roomA);
            Delete(roomBId);
        }

        private void ExtractEquipment(Room fromRoom, Room toRoom)
        {
            foreach (StaticEquipment staticEquipment in fromRoom.StaticEquipments)
                toRoom.StaticEquipments.Add(staticEquipment);
        }

        public void DettachRooms(int roomId)
        {
            Room roomA = GetById(roomId);
            Save(roomA.Name + "-A", roomA.RoomType, roomA.Floor, roomA.Detail); //room b
        }
    }
}