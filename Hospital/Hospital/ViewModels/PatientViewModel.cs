using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Hospital.ViewModels
{
    public class PatientViewModel : ViewModel
    {
        private Injector injector;

        private Patient patient;

        private string jmbg;

        private string firstName;

        private string lastName;

        public Injector Injector
        {
            get { return injector; }
            set
            {
                injector = value;
            }
        }

        public Patient _Patient
        {
            get { return patient; }
            set
            {
                patient = value;
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

        public PatientViewModel()
        {
            Injector = new Injector();

            _Patient = new Patient();
        }
    }
}
