namespace Model
{
    public class Patient
    {
        public User User { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public System.Collections.Generic.List<Appointment> appointments { get; set; }

        public System.Collections.Generic.List<Notification> notifications { get; set; }

       
     


    }
}