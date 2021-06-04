using Model;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IDynamicEquipmentRepository : IGenericRepository<DynamicEquipment>
    {
        void MoveDynamicEquipment(DynamicEquipment dynamicEquipment);

        List<DynamicEquipment> GetByName(string name);
    }
}
