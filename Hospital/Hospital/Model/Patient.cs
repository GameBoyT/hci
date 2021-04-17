namespace Model
{
    public class Patient
    {
        public User User { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public System.Collections.Generic.List<Appointment> Appointments { get; set; }

        public System.Collections.Generic.List<Notification> Notifications { get; set; }
    }
}