using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class EmployeeService
    {
        private Repository.EmployeeRepository employeeRepository = new Repository.EmployeeRepository();
        public List<Employee> GetEmployees()
        {
            return employeeRepository.GetEmployees();
        }

        public List<Employee> GetDoctors()
        {
            return employeeRepository.GetDoctors();
        }
        public void SaveDoctor(Doctor doctor)
        {
            employeeRepository.SaveDoctor(doctor);
        }

        public Doctor GetDoctorByJmbg(String jmbg)
        {
            return employeeRepository.GetDoctorByJmbg(jmbg);
        }

        public Employee GetByJmbg(String jmbg)
        {
            return employeeRepository.GetByJmbg(jmbg);
        }

        public void Save(Employee employee)
        {
            employeeRepository.Save(employee);
        }

        public void Delete(String jmbg)
        {
            employeeRepository.Delete(jmbg);
        }

        public void Update(Employee employee)
        {
            employeeRepository.Update(employee);
        }


    }
}