using Model;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Employee GetByJmbg(String jmbg);

        Employee Delete(String jmbg);

        new Employee Update(Employee employee);

        List<Employee> GetDoctors();

        List<Employee> GetDoctorsBySpecialization(string specialization);

        Employee GetDirector();

        Employee GetSecretary();
    }
}
