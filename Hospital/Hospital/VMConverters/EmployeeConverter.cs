using Hospital.ViewModels;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.VMConverters
{
    public class EmployeeConverter
    {
        public EmployeeViewModel ConvertModelToViewModel(Employee employee)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                Jmbg = employee.User.Jmbg,
                FirstName = employee.User.FirstName,
                LastName = employee.User.LastName,
                Specialization = employee.Specialization,
                _Employee = employee
            };

            return employeeViewModel;
        }

        public ObservableCollection<EmployeeViewModel> ConvertCollectionToViewModel(List<Employee> employees)
        {
            ObservableCollection<EmployeeViewModel> vmEmployees = new ObservableCollection<EmployeeViewModel>();
            EmployeeViewModel employeeViewModel;
            foreach (Employee employee in employees)
            {
                employeeViewModel = ConvertModelToViewModel(employee);
                vmEmployees.Add(employeeViewModel);
            }
            return vmEmployees;
        }
    }
}
