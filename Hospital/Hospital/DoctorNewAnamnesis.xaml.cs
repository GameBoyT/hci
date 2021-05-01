using Model;
using System.Windows;

namespace Hospital
{
    public partial class DoctorNewAnamnesis : Window
    {
        App app;
        public string AnamnesisName { get; set; }
        public string AnamnesisType { get; set; }

        public Appointment Appointment { get; set; }

        public DoctorViewPatient PastWindow { get; set; }

        public DoctorNewAnamnesis(Appointment appointment, DoctorViewPatient window)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;
            Appointment = appointment;
            PastWindow = window;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnamnesisName != "" && AnamnesisType != "")
            {
                Anamnesis anamnesis = app.patientController.AddAnamnesis(Appointment.PatientJmbg, AnamnesisName, AnamnesisType, "");
                Appointment = app.appointmentController.GetById(Appointment.Id);

                PastWindow.Anamnesis.Add(anamnesis);
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
