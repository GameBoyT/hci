using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryUrgentAppointment.xaml
    /// </summary>
    public partial class SecretaryUrgentAppointment : Window
    {

        List<Patient> patients = new List<Patient>();
        private PatientController patientController = new PatientController();
        List<Employee> employees = new List<Employee>();
        private EmployeeController employeeController = new EmployeeController();
        public SecretaryUrgentAppointment()
        {
            InitializeComponent();
            patients = patientController.GetAll();
            patientsDataGrid.ItemsSource = patients;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private List<Employee> Provera()
        {
            string spec = specializationTB.Text;
            employees = employeeController.GetDoctorsBySpecialization(spec);

            return employees;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            doctorsDataGrid.ItemsSource = Provera();
        }

        /*private void WindowUpdate()
        {
            appointments = appointmentController.GetAll();
            secretaryAppointmentDataGrid.ItemsSource = appointments;
        }*/
    }
}
