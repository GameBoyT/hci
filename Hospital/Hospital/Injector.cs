using Service;
using Hospital.VMConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Injector
    {
        private PatientService patientService = new PatientService();

        private EmployeeService employeeService = new EmployeeService();

        //private ClassesService subjectService = new ClassesService();

        private EmployeeConverter employeeConverter = new EmployeeConverter();

        //private SubjectConverter subjectConverter = new SubjectConverter();

        private PatientConverter patientConverter = new PatientConverter();

        private AppointmentConverter appointmentConverter = new AppointmentConverter();

        public PatientService PatientService
        {
            get { return patientService; }
        }

        public EmployeeService EmployeeService
        {
            get { return employeeService; }
        }

        //public ClassesService SubjectService
        //{
        //    get { return subjectService; }
        //}

        //public SubjectConverter SubjectConverter
        //{
        //    get { return subjectConverter; }
        //}

        public EmployeeConverter EmployeeConverter
        {
            get { return employeeConverter; }
        }

        public PatientConverter PatientConverter
        {
            get { return patientConverter; }
        }

        public AppointmentConverter AppointmentConverter 
        { 
            get { return appointmentConverter; }

        }
    }
}
