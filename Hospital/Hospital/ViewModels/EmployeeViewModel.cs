using Model;

namespace Hospital.ViewModels
{
    public class EmployeeViewModel : ViewModel
    {
        private Injector injector;

        private Employee employee;

        private string jmbg;

        private string firstName;

        private string lastName;

        private string specialization;

        public Injector Injector
        {
            get { return injector; }
            set
            {
                injector = value;
            }
        }

        public Employee _Employee
        {
            get { return employee; }
            set
            {
                employee = value;
            }
        }

        public string Jmbg
        {
            get { return jmbg; }
            set
            {
                jmbg = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        public string Specialization
        {
            get { return specialization; }
            set
            {
                specialization = value;
                OnPropertyChanged();
            }
        }

        public EmployeeViewModel()
        {
            Injector = new Injector();
            _Employee = new Employee();
        }

    }
}
