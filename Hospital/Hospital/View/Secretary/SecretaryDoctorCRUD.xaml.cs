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
    /// Interaction logic for SecretaryDoctorCRUD.xaml
    /// </summary>
    public partial class SecretaryDoctorCRUD : Window
    {
        private EmployeeController employeeController = new EmployeeController();
        List<Employee> doctors = new List<Employee>();

        public SecretaryDoctorCRUD()
        {
            InitializeComponent();
            doctors = employeeController.GetDoctors();
            DoctorDataGrid.ItemsSource = doctors;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private void WindowUpdate()
        {
            doctors = employeeController.GetDoctors();
            DoctorDataGrid.ItemsSource = doctors;
        }

        private void ClearFileds()
        {
            JMBGTextBox.Clear();
            NameTB.Clear();
            SurnameTB.Clear();
            UsernameTB.Clear();
            PasswordTB.Clear();
            EmailTB.Clear();
            AddressTB.Clear();
            SpecializationTB.Clear();
            StartTimeTB.Clear();
            EndTimeTB.Clear();
            DateDP.SelectedDate = null;

        }

        private Employee CreateDoctorFromData()
        {
            string jmbg = JMBGTextBox.Text;
            string name = NameTB.Text;
            string surname = SurnameTB.Text;
            string username = UsernameTB.Text;
            string password = PasswordTB.Text;
            string email = EmailTB.Text;
            string address = AddressTB.Text;
            string specialization = SpecializationTB.Text;
            string start = StartTimeTB.Text;
            string end = EndTimeTB.Text;
            DateTime dateBirth = DateDP.SelectedDate.Value; 
            User doctor = new User(jmbg, name, surname, username, password, email, address, dateBirth);
            return new Employee(doctor, 0,specialization, start, end);

        }

        private void AddDoctorToList(object sender, RoutedEventArgs e)
        {
            Employee doctor = CreateDoctorFromData();
            employeeController.Save(doctor);
            WindowUpdate();
            ClearFileds();
        }

        private void DeleteDoctor(object sender, RoutedEventArgs e)
        {
            try 
            {
                Employee employee = (Employee)DoctorDataGrid.SelectedItems[0];
                employeeController.Delete(employee.User.Jmbg);
                WindowUpdate();
            } catch 
            {
                MessageBox.Show("You must select the doctor!");
            }
            
        }

        private void Vacation(object sender, RoutedEventArgs e)
        {
            SecretatyDoctorVacation secretatyDoctorVacation = new SecretatyDoctorVacation();
            secretatyDoctorVacation.Show();
            this.Close();
        }

        private void FillFields(object sender, RoutedEventArgs e)
        {
            Employee doctor = (Employee)DoctorDataGrid.SelectedItems[0];
            JMBGTextBox.Text = doctor.User.Jmbg;
            NameTB.Text = doctor.User.FirstName;
            SurnameTB.Text = doctor.User.LastName;
            UsernameTB.Text = doctor.User.Username;
            PasswordTB.Text = doctor.User.Password;
            EmailTB.Text = doctor.User.Email;
            AddressTB.Text = doctor.User.Address;
            SpecializationTB.Text = doctor.Specialization;
            StartTimeTB.Text = doctor.StartWork;
            EndTimeTB.Text = doctor.EndWork;
        }


        private void SaveUpdate(object sender, RoutedEventArgs e)
        {
            Employee doctor = CreateDoctorFromData();
            employeeController.Update(doctor);
            WindowUpdate();
            ClearFileds();
        }
    }
}
