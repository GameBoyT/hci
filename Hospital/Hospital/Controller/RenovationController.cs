using System;
using Model;
using Service;
using System.Collections.Generic;
using System.Text;


namespace Controller
{
    class RenovationController
    {
        private readonly RenovationService _renovationService = new RenovationService();
        public List<RenovationAppointment> GetAll()
        {
            return _renovationService.GetAll();
        }
        /*
        public RenovationAppointment GetById(int id)
        {
            return appointmentService.GetById(id);
        }
        */
        public void Save(RenovationAppointment renovation)
        {
            _renovationService.Save(renovation);
        }

        public void Delete(int renovationId)
        {
            _renovationService.Delete(renovationId);
        }
        public bool AttachRooms(int roomAId, int roomBId, DateTime dateTime, double duration)
        {
            return _renovationService.AttachRooms(roomAId, roomBId, dateTime, duration);
        }
        public bool DettachRooms(int roomId, DateTime dateTime, double duration)
        {
            return _renovationService.DettachRooms(roomId, dateTime, duration);
        }
    }
}
