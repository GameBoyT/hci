namespace Model
{
    public class Medicine : Entity
    {
        public string Name { get; set; }
        
        public VerificationType Verification { get; set; }

        public string Description { get; set; }

        public string DoctorComment { get; set; }

        public Medicine(int id, string name, string description, string doctorComment)
        {
            Id = id;
            Name = name;
            Verification = VerificationType.needsVerification;
            Description = description;
            DoctorComment = doctorComment;
        }
    }
}