namespace Model
{
    public class StaticEquipment : Entity
    {
        public StaticEquipment(int id, string name, int roomId, int quantity, string description)
        {
            Id = id;
            Name = name;
            RoomId = roomId;
            Quantity = quantity;
            Description = description;
        }

        public string Name { get; set; }

        public int RoomId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}