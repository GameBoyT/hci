using Controller;
using Repository;
using System;
using System.IO;
using System.Windows;

namespace Hospital
{
    public partial class App : Application
    {
        //private static readonly string _medicineFileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicines.json";
        //private static readonly MedicineRepository medicineRepository = new MedicineRepository(_medicineFileLocation);
        //public readonly MedicineController medicineController = new MedicineController(medicineRepository);

        private static readonly string _medicineFileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicines.json";



        private static readonly MedicineRepository medicineRepository = new MedicineRepository(_medicineFileLocation);



        public readonly AppointmentController appointmentController = new AppointmentController();
        public readonly PatientController patientController = new PatientController();
        public readonly RoomController roomController = new RoomController();
        public readonly StaticEquipmentController staticEquipmentController = new StaticEquipmentController();
        public readonly MedicineController medicineController = new MedicineController(medicineRepository);
        public readonly EmployeeController employeeController = new EmployeeController();
    }
}
