using Model;
using System;

namespace Hospital.ViewModels
{
    public class PatientViewModel : ViewModel
    {
        private string jmbg;

        private string firstName;

        private string lastName;

        private DateTime dateOfBirth;

        private string address;

        public Injector Injector { get; set; }

        public Patient _Patient { get; set; }

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

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        public PatientViewModel()
        {
            Injector = new Injector();
            _Patient = new Patient();
        }
    }
}
