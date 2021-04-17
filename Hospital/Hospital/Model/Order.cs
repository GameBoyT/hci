using System;

namespace Model
{
    public class Order
    {
        public long Id
        {
            get
            ;
            set
            ;
        }

        public string Supplier
        {
            get
            ;
            set
            ;
        }

        public DateTime Date
        {
            get
            ;
            set
            ;
        }

        public System.Collections.Generic.List<StaticEquipment> StaticEquipment { get; set; }
    }
}