using DTO;
using Model;
using System.Windows;

namespace Hospital.View.Doctor
{
    public partial class DoctorNewAnamnesis : Window
    {
        App app;
        public string AnamnesisName { get; set; }
        public string AnamnesisType { get; set; }

        public DoctorViewPatient ParentWindow { get; set; }

        public DoctorNewAnamnesis(DoctorViewPatient window)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;
            ParentWindow = window;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnamnesisName != "" && AnamnesisType != "")
            {
                Anamnesis anamnesis = app.patientController.AddAnamnesis(ParentWindow.Appointment.PatientJmbg, AnamnesisName, AnamnesisType, "");
                ParentWindow.Anamnesis.Add(anamnesis);
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
