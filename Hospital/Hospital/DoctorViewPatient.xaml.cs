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
        private bool foo;
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

        public bool Foo
        {
            get { return foo; }
            set
            {
                if (foo != value)
                {
                    foo = value;
                    OnPropertyChanged("foo");
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
            Anamnesis = new ObservableCollection<Anamnesis>(Appointment.Patient.MedicalRecord.Anamnesis);
            lvDataBinding.ItemsSource = Anamnesis;
            //MessageBox.Show("You have to enter both fielaaaaads!");
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                DetailText = Appointment.Patient.MedicalRecord.Anamnesis[lvDataBinding.SelectedIndex].Description;

            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorNewAnamnesis doctorNewAnamnesisWindow = new DoctorNewAnamnesis(Appointment.Patient);
            doctorNewAnamnesisWindow.Show();
            this.Close();
        }
    }
}
