using Model;
using System.Collections.Generic;

namespace Service
{
    public class StaticEquipmentService
    {
        public Repository.StaticEquipmentRepository staticEquipmentRepository = new Repository.StaticEquipmentRepository();
        public Repository.RoomRepository roomRepository = new Repository.RoomRepository();


        public List<StaticEquipment> GetAll()
        {
            return staticEquipmentRepository.GetAll();
        }

        public Model.StaticEquipment GetById(int id)
        {
            return staticEquipmentRepository.GetById(id);
        }

        public void Save(string name, string roomName , int quantity, string description)
        {
            int id = staticEquipmentRepository.GenerateNewId();
            Room room = roomRepository.GetByName(roomName);
            StaticEquipment staticEquipment = new StaticEquipment(id, name, room, quantity, description);
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