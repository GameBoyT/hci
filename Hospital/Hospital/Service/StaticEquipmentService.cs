using Model;
using System.Collections.Generic;

namespace Service
{
    public class StaticEquipmentService
    {
        public Repository.StaticEquipmentRepository staticEquipmentRepository = new Repository.StaticEquipmentRepository();

        public List<StaticEquipment> GetAll()
        {
            return staticEquipmentRepository.GetAll();
        }

        public Model.StaticEquipment GetById(int id)
        {
            return staticEquipmentRepository.GetById(id);
        }

        public void Save(Model.StaticEquipment staticEquipment)
        {
            staticEquipmentRepository.Save(staticEquipment);
        }

        public void Delete(int id)
        {
            staticEquipmentRepository.Delete(id);
        }

        public void Update(StaticEquipment staticEquipment)
        {
            staticEquipmentRepository.Update(staticEquipment);
        }
    }
}