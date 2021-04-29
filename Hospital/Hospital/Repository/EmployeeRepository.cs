using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class EmployeeRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\employees.json";
        private List<Employee> employees = new List<Employee>();

        public EmployeeRepository()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                }
            }
        }
        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(employees);
            File.WriteAllText(fileLocation, json);
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        public Employee GetByJmbg(String jmbg)
        {
            return employees.Find(obj => obj.User.Jmbg == jmbg);
        }

        public void Save(Employee employee)
        {
            employees.Add(employee);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            int index = employees.FindIndex(obj => obj.User.Jmbg == jmbg);
            employees.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Employee employee)
        {
            int index = employees.FindIndex(obj => obj.User.Jmbg == employee.User.Jmbg);
            employees[index] = employee;
            WriteToJson();
        }
    }
}