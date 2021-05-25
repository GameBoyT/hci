using Controller;
using DTO;
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
    /// Interaction logic for SecretatyDoctorVacation.xaml
    /// </summary>
    public partial class SecretatyDoctorVacation : Window
    {
        AppointmentController appointmentController = new AppointmentController();
        private EmployeeController employeeController = new EmployeeController();
        List<Employee> doctors = new List<Employee>();

        public SecretatyDoctorVacation()
        {
            InitializeComponent();
            doctors = employeeController.GetDoctors();
            DoctorDataGrid.ItemsSource = doctors;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SecretaryDoctorCRUD secretaryDoctorCRUD = new SecretaryDoctorCRUD();
            secretaryDoctorCRUD.Show();
            this.Close();
        }

        private void CalculateDuration(object sender, RoutedEventArgs e)
        {
            DateTime StartVacation = StartDatePicker.SelectedDate.Value;
            DateTime EndVacation = EndDatePicker.SelectedDate.Value;

            TimeSpan duration = EndVacation - StartVacation;
            double durationInMinutes = duration.TotalMinutes;

            durationTB.Text = durationInMinutes.ToString();
        }

        private AppointmentDTO CreateVacation()
        {
            Employee selectedDoctor = (Employee)DoctorDataGrid.SelectedItems[0];
            DateTime pickedDate = StartDatePicker.SelectedDate.Value;
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, pickedDate.Hour, pickedDate.Minute, pickedDate.Second);
            double duration = Convert.ToDouble(durationTB.Text);

            return new AppointmentDTO(MedicalAppointmentType.examination, appointmentDateTime, duration, "6", selectedDoctor.User.Jmbg, selectedDoctor.RoomId, "6");
        }

        private void SaveVacation(object sender, RoutedEventArgs e)
        {
            AppointmentDTO vacation = CreateVacation();
            appointmentController.Save(vacation);
        }


    }
}
