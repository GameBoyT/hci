using DTO;
using Model;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RenovationService
    {
        private readonly IRenovationRepository _renovationRepository = new RenovationRepository();
        private RoomRepository roomRepository = new RoomRepository();
        //public StaticEquipmentRepository staticRepository = new StaticEquipmentRepository();
        private readonly AppointmentService _appointmentService = new AppointmentService();

        public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }

        public List<RenovationAppointment> GetAll()
        {
            return _renovationRepository.GetAll();

        }
        /*
        public AppointmentDTO GetById(int id)
        {
            return ConvertToDTO(_appointmentRepository.GetById(id));
        }
        */
        public void Save(RenovationAppointment renovation)
        {
            _renovationRepository.Save(renovation);
        }

        public int GenerateNewId()
        {
            return _renovationRepository.GenerateNewId();
        }

        public void Delete(int renovationId)
        {
            _renovationRepository.Delete(renovationId);
        }

        public bool AttachRooms(int roomAId, int roomBId, DateTime dateTime, double duration)
        {
            AppointmentDTO appointment1 = new AppointmentDTO(dateTime, duration, roomAId);
            AppointmentDTO appointment2 = new AppointmentDTO(dateTime, duration, roomBId);
            if (_appointmentService.IsRoomAvailable(appointment1) && _appointmentService.IsRoomAvailable(appointment2))
            {
                _appointmentService.SaveRenovation(appointment1);
                _appointmentService.SaveRenovation(appointment2);
                RenovationAppointment renovation = new RenovationAppointment(GenerateNewId(), dateTime, duration, roomAId, roomBId, "bla", 0);
                _renovationRepository.Save(renovation);
                return true;
            }
            return false;
        }
        public bool DettachRooms(int roomId, DateTime dateTime, double duration)
        {
            AppointmentDTO appointment1 = new AppointmentDTO(dateTime, duration, roomId);
            if (_appointmentService.IsRoomAvailable(appointment1))
            {
                _appointmentService.SaveRenovation(appointment1);
                RenovationAppointment renovation = new RenovationAppointment(GenerateNewId(), dateTime, duration, roomId, 0, "bla", 1);
                _renovationRepository.Save(renovation);
                return true;
            }
            return false;
        }
    }
}
