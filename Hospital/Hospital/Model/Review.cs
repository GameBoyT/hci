using System;

namespace Model
{
    public class Review : Entity
    {
        public String Description { get; set; }
        public int Speed { get; set; }
        public int Kindness { get; set; }
        public int Informations { get; set; }
        public int Overall { get; set; }

        public Review(int speed, int kindness, int informations, int overall, string description)
        {
            this.Speed = speed;
            this.Kindness = kindness;
            this.Informations = informations;
            this.Overall = overall;
            this.Description = description;

        }




    }
}
