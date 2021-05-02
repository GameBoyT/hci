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

namespace Hospital.View.Doctor
{
    public partial class DoctorMedicine : Window, INotifyPropertyChanged
    {
        App app;
        public event PropertyChangedEventHandler PropertyChanged;
        public Employee Doctor { get; set; }
        public Medicine Medicine { get; set; }
        public ObservableCollection<Medicine> Medicines { get; set; }


        private string medicineDescriptionText;

        public string MedicineDescriptionText
        {
            get { return medicineDescriptionText; }
            set
            {
                if (medicineDescriptionText != value)
                {
                    medicineDescriptionText = value;
                    OnPropertyChanged("medicineDescriptionText");
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

        public DoctorMedicine(Employee doctor)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;

            Doctor = doctor;
            Medicines = new ObservableCollection<Medicine>(app.medicineController.GetNotRejected());
            lvMedicineDataBinding.ItemsSource = Medicines;
        }

        private void lvMedicineDataBinding_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Medicine = (Medicine)(sender as ListView).SelectedItem;
            MedicineDescriptionText = Medicines[lvMedicineDataBinding.SelectedIndex].Description;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Medicine medicine = Medicines[lvMedicineDataBinding.SelectedIndex];
            app.medicineController.Update(medicine.Id, medicine.Name, medicine.Verification, MedicineDescriptionText);
            medicine.Description = MedicineDescriptionText;
        }

        private void VerifyButton_Click(object sender, RoutedEventArgs e)
        {
            Medicine medicine = Medicines[lvMedicineDataBinding.SelectedIndex];
            app.medicineController.Update(medicine.Id, medicine.Name, VerificationType.verified, medicine.Description);
            //medicine.Verification = VerificationType.verified;

            Medicines = new ObservableCollection<Medicine>(app.medicineController.GetNotRejected());
            lvMedicineDataBinding.ItemsSource = Medicines;
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
