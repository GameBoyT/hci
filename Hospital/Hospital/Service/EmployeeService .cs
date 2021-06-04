using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service
{
    public class EmployeeService
    {
        private Repository.EmployeeRepository employeeRepository = new Repository.EmployeeRepository();
        public List<Employee> GetAll()
        {
            return employeeRepository.GetAll();
        }

        public Employee GetByJmbg(String jmbg)
        {

            return employeeRepository.GetByJmbg(jmbg);
        }

        public void Save(Employee employee)
        {
            List<Employee> employees = employeeRepository.GetAll();
            foreach (Employee employee1 in employees)
            {
                if (employee1.User.Jmbg == employee.User.Jmbg)
                {
                    MessageBox.Show("Isti jmbg", "greska");
                    return;
                }

                if (employee1.User.Username == employee.User.Username)
                {
                    MessageBox.Show("Isti username", "greska");
                    return;
                }
            }
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

        public List<Employee> GetDoctors()
        {
            return employeeRepository.GetDoctors();
        }

        public void RateDoctor(string doctorJmbg, Review review)
        {
            Employee doctor = employeeRepository.GetByJmbg(doctorJmbg);
            doctor.Reviews.Add(review);
            employeeRepository.Update(doctor);
        }
        public List<Employee> GetDoctorsBySpecialization(string specialization)
        {
            return employeeRepository.GetDoctorsBySpecialization(specialization);
        }

        public Employee GetDirector()
        {
            return employeeRepository.GetDirector();
        }

        public Employee GetSecretary()
        {
            return employeeRepository.GetSecretary();
        }
    }
}