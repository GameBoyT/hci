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

        private AppointmentService appointmentService = new AppointmentService();

        private RoomService roomService = new RoomService();


        private EmployeeConverter employeeConverter = new EmployeeConverter();

        private RoomConverter roomConverter = new RoomConverter();

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

        public AppointmentService AppointmentService
        {
            get { return appointmentService; }
        }


        public RoomService RoomService
        {
            get { return roomService; }
        }

        public RoomConverter RoomConverter
        {
            get { return roomConverter; }
        }

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
