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
using Model;
using Controller;
using System.Collections.ObjectModel;


namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecord.xaml
    /// </summary>
    public partial class PatientMedicalRecord : Window
    {
        PatientController patientController = new PatientController();
        AppointmentController appointmentController = new AppointmentController();
        Patient patient;
        ObservableCollection<Anamnesis> anamneses = new ObservableCollection<Anamnesis>();
        public PatientMedicalRecord()
        {

            InitializeComponent();
            patient = patientController.GetByJmbg("5");
            AnamnesisListView.ItemsSource = patientController.GetAllAnamnesisForPatient(patient.User.Jmbg);
            PrescriptionListView.ItemsSource = patientController.GetAllPrescriptionsForPatient(patient.User.Jmbg);
            OperationsListView.ItemsSource = appointmentController.GetOperationsForPatient(patient.User.Jmbg);

        }
    }
}
