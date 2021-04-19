using Model;
using Service;
using System.Collections.Generic;

namespace Controller
{
    public class DynamicEquipmentController
    {
        private DynamicEquipmentService dynamicService = new DynamicEquipmentService();

        public List<DynamicEquipment> GetAll()
        {
            return dynamicService.GetAll();
        }

        public DynamicEquipment GetById(int id)
        {
            return dynamicService.GetById(id);
        }

        public void Save(DynamicEquipment dinamicEquipment)
        {
            dynamicService.Save(dinamicEquipment);
        }

        public void Delete(int id)
        {
            dynamicService.Delete(id);
        }

        public void Update(DynamicEquipment dinamicEquipment)
        {
            dynamicService.Update(dinamicEquipment);
        }
        public int GenerateNewId()
        {
            return dynamicService.GenerateNewId();
        }


    }
}