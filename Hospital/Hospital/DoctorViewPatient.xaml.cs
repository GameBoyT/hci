using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DoctorViewPatient : Window, INotifyPropertyChanged
    {
        App app;
        public Appointment Appointment { get; set; }
        public ObservableCollection<Anamnesis> Anamnesis { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAppointmentTime;
        private string detailText;

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
            {
                Anamnesis = new ObservableCollection<Anamnesis>(Appointment.Patient.MedicalRecord.Anamnesis);
            }
            else
            {
                Anamnesis = new ObservableCollection<Anamnesis>();
            }
            lvDataBinding.ItemsSource = Anamnesis;

        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if (!app.appointmentController.AppoinementTimeInFuture(Appointment))
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
            //int anamnesisId = Appointment.Patient.MedicalRecord.Anamnesis[lvDataBinding.SelectedIndex].Id;
            //app.patientController.UpdateAnamnesisDescription(Appointment.Patient.User.Jmbg, anamnesisId, DetailText);
            //Appointment = app.appointmentController.GetById(Appointment.Id);
            //Anamnesis = new ObservableCollection<Anamnesis>(Appointment.Patient.MedicalRecord.Anamnesis);

        }
        
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorNewAnamnesis doctorNewAnamnesisWindow = new DoctorNewAnamnesis(Appointment);
            doctorNewAnamnesisWindow.Show();
            this.Close();
        }
    }
}
