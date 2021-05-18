using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IStaticEquipmentRepository : IGenericRepository<StaticEquipment>
    {
        List<StaticEquipment> GetByName(string name);

        List<StaticEquipment> FilterRoomId(int id);
    }
}
