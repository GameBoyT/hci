using System.Collections.Generic;

namespace Model
{
    public class Medicine : Entity
    {
        public Medicine(int id, string name, string description, string doctorComment)
        {
            Id = id;
            Name = name;
            Verification = VerificationType.needsVerification;
            Description = description;
            DoctorComment = doctorComment;
            Ingredients = new List<string>();
            Alternatives = new List<Medicine>();
        }

        public string Name { get; set; }

        public VerificationType Verification { get; set; }

        public string Description { get; set; }

        public string DoctorComment { get; set; }

        public List<string> Ingredients { get; set; }

        public List<Medicine> Alternatives { get; set; }
    }
}