using System;

namespace Model
{
    public class Anamnesis
    {
        public int Id { get; set; }
        public String Type { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public Anamnesis(int id, String type, String name, String description)
        {
            this.Id = id;
            this.Type = type;
            this.Name = name;
            this.Description = description;
        }


    }
}