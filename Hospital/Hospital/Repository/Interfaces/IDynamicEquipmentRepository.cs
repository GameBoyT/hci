using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    interface IDynamicEquipmentRepository : IGenericRepository<DynamicEquipment>
    {
        void MoveDynamicEquipment(DynamicEquipment dynamicEquipment);

        List<DynamicEquipment> GetByName(string name);
    }
}
