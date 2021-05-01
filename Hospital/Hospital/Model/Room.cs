using System;

namespace Model
{
    public class Room
    {
        public Room(int id, string name, RoomType roomType, int floor, string detail)
        {
            Id = id;
            Name = name;
            RoomType = roomType;
            Floor = floor;
            Detail = detail;
            Appointments = new System.Collections.Generic.List<Appointment>();
            StaticEquipments = new System.Collections.Generic.List<StaticEquipment>();
            DynamicEquipments = new System.Collections.Generic.List<DynamicEquipment>();
        }

        public int Id { get; set; }

        public String Name { get; set; }

        public RoomType RoomType { get; set; }

        public int Floor { get; set; }

        public String Detail { get; set; }

        public System.Collections.Generic.List<Appointment> Appointments { get; set; }
        public System.Collections.Generic.List<StaticEquipment> StaticEquipments { get; set; }
        public System.Collections.Generic.List<DynamicEquipment> DynamicEquipments { get; set; }

        //public void AddStaticEquipment(StaticEquipment staticEquipment)
        //{
        //    if (staticEquipment == null)
        //        return;
        //    if (this.StaticEquipments == null)
        //        this.StaticEquipments = new System.Collections.Generic.List<StaticEquipment>();
        //    if (!this.StaticEquipments.Contains(staticEquipment))
        //    {
        //        this.StaticEquipments.Add(staticEquipment);
        //        staticEquipment.Room = this;
        //    }
        //}
    }
}