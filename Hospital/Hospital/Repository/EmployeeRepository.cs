using Model;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\employees.json";
            ReadJson();
        }
        public Employee GetByJmbg(String jmbg)
        {
            ReadJson();
            return _objects.Find(obj => obj.User.Jmbg == jmbg);
        }

        public Employee Delete(String jmbg)
        {
            ReadJson();
            Employee employee = _objects.Find(obj => obj.User.Jmbg == jmbg);
            _objects.Remove(employee);
            WriteToJson();
            return employee;
        }

        public new Employee Update(Employee employee)
        {
            ReadJson();
            int index = _objects.FindIndex(obj => obj.User.Jmbg == employee.User.Jmbg);
            _objects[index] = employee;
            WriteToJson();
            return employee;
        }

        public List<Employee> GetDoctors()
        {
            ReadJson();
            return _objects.FindAll(obj => obj.EmployeeType == EmployeeType.doctor);
        }

        public List<Employee> GetDoctorsBySpecialization(string specialization)
        {
            ReadJson();
            List<Employee> doctors = GetDoctors();
            return doctors.FindAll(obj => string.Equals(obj.Specialization, specialization, StringComparison.OrdinalIgnoreCase));
        }

        public Employee GetDirector()
        {
            ReadJson();
            return _objects.Find(obj => obj.EmployeeType == EmployeeType.director);
        }

        public Employee GetSecretary()
        {
            ReadJson();
            return _objects.Find(obj => obj.EmployeeType == EmployeeType.secretary);
        }
    }
}