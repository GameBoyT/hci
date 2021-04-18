namespace Model
{
    public class StaticEquipment
    {
        public StaticEquipment(int id, string name, Room room, int quantity, string description)
        {
            Id = id;
            Name = name;
            Room = room;
            Quantity = quantity;
            Description = description;
        }

        public int Id
        {
            get
            ;
            set
            ;
        }

        public Room Room { get; set; }


        public string Name
        {
            get
            ;
            set
            ;
        }

        public int Quantity
        {
            get
            ;
            set
            ;
        }

        public string Description
        {
            get
            ;
            set
            ;
        }
    }
}