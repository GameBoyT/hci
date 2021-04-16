using System;

namespace Model
{
    public class Order
    {
        public long id
        {
            get
            ;
            set
            ;
        }

        public string supplier
        {
            get
            ;
            set
            ;
        }

        public DateTime date
        {
            get
            ;
            set
            ;
        }

        public System.Collections.Generic.List<StaticEquipment> staticEquipment;

        public System.Collections.Generic.List<StaticEquipment> StaticEquipment
        {
            get
            {
                if (staticEquipment == null)
                    staticEquipment = new System.Collections.Generic.List<StaticEquipment>();
                return staticEquipment;
            }
            set
            {
                RemoveAllStaticEquipment();
                if (value != null)
                {
                    foreach (StaticEquipment oStaticEquipment in value)
                        AddStaticEquipment(oStaticEquipment);
                }
            }
        }

        public void AddStaticEquipment(StaticEquipment newStaticEquipment)
        {
            if (newStaticEquipment == null)
                return;
            if (this.staticEquipment == null)
                this.staticEquipment = new System.Collections.Generic.List<StaticEquipment>();
            if (!this.staticEquipment.Contains(newStaticEquipment))
                this.staticEquipment.Add(newStaticEquipment);
        }

        public void RemoveStaticEquipment(StaticEquipment oldStaticEquipment)
        {
            if (oldStaticEquipment == null)
                return;
            if (this.staticEquipment != null)
                if (this.staticEquipment.Contains(oldStaticEquipment))
                    this.staticEquipment.Remove(oldStaticEquipment);
        }

        public void RemoveAllStaticEquipment()
        {
            if (staticEquipment != null)
                staticEquipment.Clear();
        }

    }
}