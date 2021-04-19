namespace Model
{
    public class DynamicEquipment
    {
        public DynamicEquipment(int id, string name, int quantity, string description)
        {
            Id = id;
            Name = name;
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

        public int Quantity
        {
            get
            ;
            set
            ;
        }

        public string Type
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

        public string Name
        {
            get
            ;
            set
            ;
        }

    }
}