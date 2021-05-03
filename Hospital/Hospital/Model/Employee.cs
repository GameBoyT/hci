using System;

namespace Model
{
    public class Employee
    {
        public Employee(User user, EmployeeType employeeType)
        {
            User = user;
            EmployeeType = employeeType;
            Appointments = new System.Collections.Generic.List<Appointment>();
            Notifications = new System.Collections.Generic.List<Notification>();
            Reviews = new System.Collections.Generic.List<Review>();
            Specialization = "";
        }

        public User User { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public Double Salary { get; set; }

        public int AnnualLeave { get; set; }

        public int RoomId { get; set; }

        public string Specialization { get; set; }

        public System.Collections.Generic.List<Appointment> Appointments { get; set; }

        public System.Collections.Generic.List<Notification> Notifications { get; set; }

        public System.Collections.Generic.List<Review> Reviews { get; set; }
    }
}