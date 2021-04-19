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

namespace Hospital
{
    public partial class DoctorNewAnamnesis : Window
    {
        App app;
        public string AnamnesisName { get; set; }
        public string AnamnesisType { get; set; }

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }


        public DoctorNewAnamnesis(Appointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            app = Application.Current as App;
            Appointment = appointment;
            Patient = appointment.Patient;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnamnesisName != "" && AnamnesisType != "")
            {
                app.patientController.AddAnamnesis(Patient.User.Jmbg, AnamnesisName, AnamnesisType, "");
                Appointment = app.appointmentController.GetById(Appointment.Id);
                DoctorViewPatient doctorViewPatientWindow = new DoctorViewPatient(Appointment);
                doctorViewPatientWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You have to enter both fields!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
