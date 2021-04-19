using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class DoctorViewPatient : Window
    {
        App app;
        public Appointment Appointment { get; set; }
        public DoctorViewPatient(Appointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            app = Application.Current as App;
            Appointment = appointment;

            lvDataBinding.ItemsSource = appointment.Patient.MedicalRecord.Anamnesis;
        }
    }
}
