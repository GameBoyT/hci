using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class EmployeeRepository
    {
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\employees.json";
        private List<Employee> _employees = new List<Employee>();

        public EmployeeRepository()
        {
            ReadJson();
        }

        public void ReadJson()
        {
            if (!File.Exists(_fileLocation))
            {
                File.Create(_fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(_fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    _employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_employees, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Employee> GetAll()
        {
            ReadJson();
            return _employees;
        }

        public Employee GetByJmbg(String jmbg)
        {
            ReadJson();
            return _employees.Find(obj => obj.User.Jmbg == jmbg);
        }

        public void Save(Employee employee)
        {
            ReadJson();
            _employees.Add(employee);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            ReadJson();
            int index = _employees.FindIndex(obj => obj.User.Jmbg == jmbg);
            _employees.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Employee employee)
        {
            ReadJson();
            int index = _employees.FindIndex(obj => obj.User.Jmbg == employee.User.Jmbg);
            _employees[index] = employee;
            WriteToJson();
        }

        public List<Employee> GetDoctors()
        {
            ReadJson();
            return _employees.FindAll(obj => obj.EmployeeType == EmployeeType.doctor);
        }

        public List<Employee> GetDoctorsBySpecialization(string specialization)
        {
            ReadJson();
            return _employees.FindAll(obj => string.Equals(obj.Specialization, specialization, StringComparison.OrdinalIgnoreCase));
        }

        public Employee GetDirector()
        {
            ReadJson();
            return _employees.Find(obj => obj.EmployeeType == EmployeeType.director);
        }

        public Employee GetSecretary()
        {
            ReadJson();
            return _employees.Find(obj => obj.EmployeeType == EmployeeType.secretary);
        }
    }
}