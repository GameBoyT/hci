using Hospital.VMConverters;
using Service;

namespace Hospital
{
    public class Injector
    {
        private PatientService patientService = new PatientService();

        private EmployeeService employeeService = new EmployeeService();

        private AppointmentService appointmentService = new AppointmentService();

        private RoomService roomService = new RoomService();

        private MedicineService medicineService = new MedicineService();


        private EmployeeConverter employeeConverter = new EmployeeConverter();

        private RoomConverter roomConverter = new RoomConverter();

        private MedicineConverter medicineConverter = new MedicineConverter();

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

        public MedicineService MedicineService
        {
            get { return medicineService; }
        }


        public RoomConverter RoomConverter
        {
            get { return roomConverter; }
        }

        public MedicineConverter MedicineConverter
        {
            get { return medicineConverter; }
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
