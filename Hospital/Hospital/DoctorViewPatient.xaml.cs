using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital
{
    public partial class DoctorViewPatient : Window, INotifyPropertyChanged
    {
        App app;
        public Appointment Appointment { get; set; }
        public Medicine Medicine { get; set; }
        public ObservableCollection<Anamnesis> Anamnesis { get; set; }
        public ObservableCollection<Medicine> Medicines { get; set; }
        public ObservableCollection<Prescription> Prescriptions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAppointmentTime;
        private string detailText;
        private string descriptionText;
        private string quantity;

        public string DetailText
        {
            get { return detailText; }
            set
            {
                if (detailText != value)
                {
                    detailText = value;
                    OnPropertyChanged("detailText");
                }
            }
        }

        public string DescriptionText
        {
            get { return descriptionText; }
            set
            {
                if (descriptionText != value)
                {
                    descriptionText = value;
                    OnPropertyChanged("descriptionText");
                }
            }
        }

        public string Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged("quantity");
                }
            }
        }

        public bool IsAppointmentTime
        {
            get { return isAppointmentTime; }
            set
            {
                if (isAppointmentTime != value)
                {
                    isAppointmentTime = value;
                    OnPropertyChanged("isAppointmentTime");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DoctorViewPatient(Appointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            app = Application.Current as App;



            Appointment = app.appointmentController.GetById(appointment.Id);
            if (Appointment.Patient.MedicalRecord != null && Appointment.Patient.MedicalRecord.Anamnesis != null)
                Anamnesis = new ObservableCollection<Anamnesis>(Appointment.Patient.MedicalRecord.Anamnesis);
            else
                Anamnesis = new ObservableCollection<Anamnesis>();

            if (!app.appointmentController.AppointmentTimeInFuture(Appointment))
                IsAppointmentTime = true;

            Medicines = new ObservableCollection<Medicine>(app.medicineController.GetAll());
            Prescriptions = new ObservableCollection<Prescription>(appointment.Patient.MedicalRecord.Prescription);
            lvDataBinding.ItemsSource = Anamnesis;
            lvPrescriptionDataBinding.ItemsSource = Medicines;
            lvPatientPrescriptionDataBinding.ItemsSource = Prescriptions;
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if (!app.appointmentController.AppointmentTimeInFuture(Appointment))
                {
                    IsAppointmentTime = true;
                }
                DetailText = Appointment.Patient.MedicalRecord.Anamnesis[lvDataBinding.SelectedIndex].Description;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            int anamnesisId = Appointment.Patient.MedicalRecord.Anamnesis[lvDataBinding.SelectedIndex].Id;
            app.patientController.UpdateAnamnesisDescription(Appointment.Patient.User.Jmbg, anamnesisId, DetailText);
            Appointment = app.appointmentController.GetById(Appointment.Id);
            Anamnesis = new ObservableCollection<Anamnesis>(Appointment.Patient.MedicalRecord.Anamnesis);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DetailText = Appointment.Patient.MedicalRecord.Anamnesis[lvDataBinding.SelectedIndex].Description;
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            if (app.appointmentController.AppointmentTimeInFuture(Appointment))
            {
                MessageBox.Show("You cannot add anamnesis until or after the appointment");
            }
            else
            {
                DoctorNewAnamnesis doctorNewAnamnesisWindow = new DoctorNewAnamnesis(Appointment);
                doctorNewAnamnesisWindow.Show();
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindow doctorWindow = new DoctorWindow();
            doctorWindow.Show();
            this.Close();
        }

        private void listViewPrescription_Click(object sender, RoutedEventArgs e)
        {
            Medicine = (Medicine)(sender as ListView).SelectedItem;
        }

        private void PrescriptionAddButton_Click(object sender, RoutedEventArgs e)
        {
            Prescription prescription = new Prescription(Medicine, Int32.Parse(Quantity), DescriptionText);
            Patient patient = Appointment.Patient;
            patient.MedicalRecord.Prescription.Add(prescription);
            app.patientController.Update(patient);
            DescriptionText = "";
            Quantity = "";
            Prescriptions = new ObservableCollection<Prescription>(patient.MedicalRecord.Prescription);
            lvPatientPrescriptionDataBinding.ItemsSource = Prescriptions;
        }
    }
}
