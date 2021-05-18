using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    interface IRoomRepository : IGenericRepository<Room>
    {
        Room GetByName(String name);

        List<Room> GetOperationRooms();

        List<Room> GetRoomsWithEquipmentName(string name);

    }
}
