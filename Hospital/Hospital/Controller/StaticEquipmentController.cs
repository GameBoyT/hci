using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class StaticEquipmentController
    {
        private Service.StaticEquipmentService staticEquipmentService = new Service.StaticEquipmentService();

        public List<StaticEquipment> GetAll()
        {
            return staticEquipmentService.GetAll();

        }

        public StaticEquipment GetById(int id)
        {
            return staticEquipmentService.GetById(id);
        }

        public void Save(int quantity, String type, String description, int name)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            staticEquipmentService.Delete(id);
        }

        public void Update(int quantity, String type, String description, int name)
        {
            throw new NotImplementedException();
        }

    }
}