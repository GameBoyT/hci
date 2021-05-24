using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MovingStatic : Entity
    {

        public MovingStatic(int id, int staticId, int roomId, DateTime dateTime)
        {
            Id = id;
            StaticId = staticId;
            RoomId = roomId;
            DateTime = dateTime;


        }
        public int StaticId { get; set; }
        public int RoomId { get; set; }
        public DateTime DateTime { get; set; }
        
    }
}
