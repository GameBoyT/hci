using Controller;
using Model;
using System.Collections.Generic;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for SecretaryAllergens.xaml
    /// </summary>
    public partial class SecretaryAllergens : Window
    {
        List<Patient> patients = new List<Patient>();
        private PatientController patientController = new PatientController();
        public Patient patient { get; set; }

        public SecretaryAllergens()
        {
            InitializeComponent();
            patients = patientController.GetAll();
            secretaryAllergensDataGrid.ItemsSource = patients;
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                                patient = (Patient)secretaryAllergensDataGrid.SelectedItems[0];
            }
            catch
            {

            }
            Show_Alergens();
            
        }

        public void Show_Alergens()
        {
            patients = patientController.GetAll();
            
            
            patient = patientController.GetByJmbg(patient.User.Jmbg);
            secretaryAllergensDataGrid.ItemsSource = patients;
            lvDataBinding.ItemsSource = patient.MedicalRecord.Alergies;
           
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            patient.MedicalRecord.Alergies.Add(addTextBox.Text);
            patientController.Update(patient);
            Show_Alergens();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            List<String> alergens = patient.MedicalRecord.Alergies;
            
            patient.MedicalRecord.Alergies.Remove(lvDataBinding.SelectedItems[0].ToString());
            patientController.Update(patient);
            Show_Alergens();

        }
    }
}
