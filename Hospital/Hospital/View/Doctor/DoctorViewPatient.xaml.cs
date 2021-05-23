using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class DoctorViewPatient : Window, INotifyPropertyChanged
    {
        App app;
        public AppointmentDTO MedicalAppointment { get; set; }
        public Patient Patient { get; set; }
        public Medicine Medicine { get; set; }
        public ObservableCollection<Anamnesis> Anamnesis { get; set; }
        public ObservableCollection<Prescription> Prescriptions { get; set; }

        private List<Employee> Doctors { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAppointmentTime;
        private string detailText;
        private string descriptionText;
        private string quantity;
        private string referralDescriptionText;

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

        public string ReferralDescriptionText
        {
            get { return referralDescriptionText; }
            set
            {
                if (referralDescriptionText != value)
                {
                    referralDescriptionText = value;
                    OnPropertyChanged("referralDescriptionText");
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

        public DoctorViewPatient(AppointmentDTO appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            app = Application.Current as App;

            MedicalAppointment = appointment;
            if (!app.appointmentController.IsTimeInFuture(MedicalAppointment.StartTime))
                IsAppointmentTime = true;
            Patient = app.patientController.GetByJmbg(appointment.PatientJmbg);

            Doctors = app.employeeController.GetDoctors();

            Anamnesis = new ObservableCollection<Anamnesis>(Patient.MedicalRecord.Anamnesis);
            Prescriptions = new ObservableCollection<Prescription>(Patient.MedicalRecord.Prescription);
            lvDataBinding.ItemsSource = Anamnesis;
            lvPrescriptionDataBinding.ItemsSource = app.medicineController.GetByVerification(VerificationType.verified);
            lvPatientPrescriptionDataBinding.ItemsSource = Prescriptions;
            doctorsDataGrid.ItemsSource = Doctors;
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if (!app.appointmentController.IsTimeInFuture(MedicalAppointment.StartTime))
                {
                    IsAppointmentTime = true;
                }
                DetailText = Anamnesis[lvDataBinding.SelectedIndex].Description;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Anamnesis selectedAnamnesis = Anamnesis[lvDataBinding.SelectedIndex];
            Anamnesis updatedAnamnesis = app.patientController.UpdateAnamnesisDescription(Patient.User.Jmbg, selectedAnamnesis.Id, DetailText);
            selectedAnamnesis.Description = updatedAnamnesis.Description;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DetailText = Anamnesis[lvDataBinding.SelectedIndex].Description;
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            if (app.appointmentController.IsTimeInFuture(MedicalAppointment.StartTime))
            {
                MessageBox.Show("You cannot add anamnesis until or after the appointment");
            }
            else
            {
                DoctorNewAnamnesis doctorNewAnamnesisWindow = new DoctorNewAnamnesis(this);
                doctorNewAnamnesisWindow.Show();
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
            foreach (string ingredient in Medicine.Ingredients)
            {
                if (Patient.MedicalRecord.Alergies.Contains(ingredient))
                {
                    MessageBox.Show("The patient is alergic to a medicine ingredient!");
                    return;
                }
            }
            try
            {
                Prescription prescription = app.patientController.AddPrescription(MedicalAppointment.PatientJmbg, Medicine, Int32.Parse(Quantity), DescriptionText);
                DescriptionText = "";
                Quantity = "";
                Prescriptions.Add(prescription);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void specializationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            doctorsDataGrid.ItemsSource = Doctors.FindAll(obj => obj.Specialization.IndexOf(specializationName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void ReferralAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReferralDescriptionText == "" || ReferralDescriptionText == null)
            {
                MessageBox.Show("You have to enter a description!");
                return;
            }

            try
            {
                Employee doctor = (Employee)doctorsDataGrid.SelectedItems[0];
                app.patientController.AddReferral(MedicalAppointment.PatientJmbg, doctor.User.Jmbg, ReferralDescriptionText);
                ReferralDescriptionText = "";
            }
            catch
            {
                MessageBox.Show("Choose a doctor!");
            }
        }
    }
}
