using System;

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
    }
}