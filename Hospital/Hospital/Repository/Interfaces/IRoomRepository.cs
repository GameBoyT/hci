using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Room GetByName(String name);

        List<Room> GetRoomsByRoomType(RoomType roomType);

        List<Room> GetRoomsWithEquipmentName(string name);

    }
}
