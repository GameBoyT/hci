using Model;
using Repository;
using System.Collections.Generic;

namespace Service
{
    public class DynamicEquipmentService
    {
        private DynamicEquipmentRepository dynamicRepository = new DynamicEquipmentRepository();
        public List<DynamicEquipment> GetAll()
        {
            return dynamicRepository.GetAll();
        }

        public DynamicEquipment GetById(int id)
        {
            return dynamicRepository.GetById(id);
        }

        public void Save(DynamicEquipment dinamicEquipment)
        {
            dynamicRepository.Save(dinamicEquipment);
        }

        public void Delete(int id)
        {
            dynamicRepository.Delete(id);
        }

        public void Update(DynamicEquipment dinamicEquipment)
        {
            dynamicRepository.Update(dinamicEquipment);
        }

        public int GenerateNewId()
        {
            return dynamicRepository.GenerateNewId();
        }
    }
}