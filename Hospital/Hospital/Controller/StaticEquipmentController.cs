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

        public int GenerateNewId()
        {
            return staticEquipmentService.GenerateNewId();
        }

        public StaticEquipment GetById(int id)
        {
            return staticEquipmentService.GetById(id);
        }

        public void Save(String name, String roomName, int quantity, String description)
        {
            staticEquipmentService.Save(name, roomName, quantity, description);
        }

        public void Delete(int id)
        {
            staticEquipmentService.Delete(id);
        }

        public void Update(StaticEquipment staticEquipment)
        {
            staticEquipmentService.Update(staticEquipment);
        }

    }
}