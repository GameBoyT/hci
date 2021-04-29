using Controller;
using System.Windows;

namespace Hospital
{
    public partial class App : Application
    {
        public readonly AppointmentController appointmentController = new AppointmentController();
        public readonly PatientController patientController = new PatientController();
        public readonly RoomController roomController = new RoomController();
        public readonly StaticEquipmentController staticEquipmentController = new StaticEquipmentController();
        public readonly MedicineController medicineController = new MedicineController();
        public readonly EmployeeController employeeController = new EmployeeController();
    }
}
