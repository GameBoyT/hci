using Model;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IStaticEquipmentRepository : IGenericRepository<StaticEquipment>
    {
        List<StaticEquipment> GetByName(string name);

        List<StaticEquipment> FilterRoomId(int id);
    }
}
