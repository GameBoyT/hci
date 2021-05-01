﻿using Model;
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
        public Patient Patient { get; set; }
        public Medicine Medicine { get; set; }
        public ObservableCollection<Anamnesis> Anamnesis { get; set; }
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

            Patient = app.patientController.GetByJmbg(appointment.PatientJmbg);
            //TODO: Change to this or to parameter appointmentId
            //Appointment = appointment;

            Appointment = app.appointmentController.GetById(appointment.Id);
            if (!app.appointmentController.AppointmentTimeInFuture(Appointment))
                IsAppointmentTime = true;

            Anamnesis = new ObservableCollection<Anamnesis>(Patient.MedicalRecord.Anamnesis);
            Prescriptions = new ObservableCollection<Prescription>(Patient.MedicalRecord.Prescription);
            lvDataBinding.ItemsSource = Anamnesis;
            lvPrescriptionDataBinding.ItemsSource = app.medicineController.GetAll();
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
                DetailText = Anamnesis[lvDataBinding.SelectedIndex].Description;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Anamnesis selectedAnamnesis = Patient.MedicalRecord.Anamnesis[lvDataBinding.SelectedIndex];
            Anamnesis updatedAnamnesis = app.patientController.UpdateAnamnesisDescription(Patient.User.Jmbg, selectedAnamnesis.Id, DetailText);
            int index = Anamnesis.IndexOf(selectedAnamnesis);
            Anamnesis[index] = updatedAnamnesis;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DetailText = Anamnesis[lvDataBinding.SelectedIndex].Description;
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
            try
            {
                Prescription prescription = app.patientController.AddPrescription(Appointment.PatientJmbg, Medicine, Int32.Parse(Quantity), DescriptionText);
                DescriptionText = "";
                Quantity = "";
                Prescriptions.Add(prescription);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
    }
}