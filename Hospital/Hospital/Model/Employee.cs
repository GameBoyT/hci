using System;

namespace Model
{
    public class Employee : Entity 
    {
        public Employee(User user, EmployeeType employeeType,string specialization, string start, string end)
        {
            User = user;
            EmployeeType = employeeType;
            this.StartWork = start;
            this.EndWork = end;
            Appointments = new System.Collections.Generic.List<MedicalAppointment>();
            Notifications = new System.Collections.Generic.List<Notification>();
            Reviews = new System.Collections.Generic.List<Review>();
            Specialization = specialization;
        }


        public User User { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public Double Salary { get; set; }

        public int AnnualLeave { get; set; }

        public int RoomId { get; set; }

        public string StartWork { get; set; }

        public string EndWork { get; set; }

        public string Specialization { get; set; }

        public System.Collections.Generic.List<MedicalAppointment> Appointments { get; set; }

        public System.Collections.Generic.List<Notification> Notifications { get; set; }

        public System.Collections.Generic.List<Review> Reviews { get; set; }
    }
}