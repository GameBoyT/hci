namespace Model
{
    public class StaticEquipment
    {
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

        public Room Room { get; set; }

    }
}