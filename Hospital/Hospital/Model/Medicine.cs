namespace Model
{
    public class Medicine
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public VerificationType Verification { get; set; }

        public string Description { get; set; }

        public string DoctorComment { get; set; }

        //public Medicine(int id, string name, string description)
        //{
        //    Id = id;
        //    Name = name;
        //    Verification = VerificationType.needsVerification;
        //    Description = description;
        //}

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