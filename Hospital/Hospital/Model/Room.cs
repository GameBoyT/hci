using System;

namespace Model
{
    public class Room : Entity
    {
        public Room()
        {
        }

        public Room(int id, string name, RoomType roomType, int floor, string detail, bool status)
        {
            Id = id;
            Name = name;
            RoomType = roomType;
            Floor = floor;
            Detail = detail;
            Status = status;
            Appointments = new System.Collections.Generic.List<MedicalAppointment>();
            StaticEquipments = new System.Collections.Generic.List<StaticEquipment>();

        }

        public bool Status { get; set; }

        public String Name { get; set; }

        public RoomType RoomType { get; set; }

        public int Floor { get; set; }

        public String Detail { get; set; }

        public System.Collections.Generic.List<MedicalAppointment> Appointments { get; set; }

        public System.Collections.Generic.List<StaticEquipment> StaticEquipments { get; set; }
    }
}