using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class EmployeeController
    {
        private EmployeeService employeeService = new EmployeeService();

        public List<Employee> GetAll()
        {
            return employeeService.GetAll();
        }

        public Employee GetByJmbg(String jmbg)
        {
            return employeeService.GetByJmbg(jmbg);
        }

        public void Save(Employee employee)
        {
            employeeService.Save(employee);
        }

        public void Delete(String jmbg)
        {
            employeeService.Delete(jmbg);
        }

        public void Update(Employee employee)
        {
            employeeService.Update(employee);
        }

        public List<Employee> GetDoctors()
        {
            return employeeService.GetDoctors();
        }
    }
}