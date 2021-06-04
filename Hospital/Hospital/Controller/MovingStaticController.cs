using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    class MovingStaticController
    {
        private readonly MovingStaticService _movingStaticService = new MovingStaticService();


        public List<MovingStatic> GetAll()
        {
            return _movingStaticService.GetAll();

        }
        /*
        public AppointmentDTO GetById(int id)
        {
            return ConvertToDTO(_appointmentRepository.GetById(id));
        }
        */
        public void Save(int staticId, int roomId, DateTime dateTime)
        {
            _movingStaticService.Save(staticId, roomId, dateTime);
        }

        public void Delete(int movingId)
        {
            _movingStaticService.Delete(movingId);
        }

    }
}

