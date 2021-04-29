using System;
using System.Collections.Generic;

namespace Model
{
    public class Employee
    {
        public Employee(User user, EmployeeType employeeType)
        {
            User = user;
            EmployeeType = employeeType;
        }
        public User User { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public Double Salary { get; set; }

        public int AnnualLeave { get; set; }

        public Room Room { get; set; }
        public String Specialization { get; set; }

        public System.Collections.Generic.List<Review> Reviews { get; set; }


        public System.Collections.Generic.List<Appointment> Appointment { get; set; }

        public System.Collections.Generic.List<Notification> Notification { get; set; }


    }
}