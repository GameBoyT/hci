using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public DoctorMedicine()
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;

            Doctor = app.employeeController.GetByJmbg("1");
            Medicines = new ObservableCollection<Medicine>(app.medicineController.GetNotRejected());
            lvMedicineDataBinding.ItemsSource = Medicines;
        }

        private void lvMedicineDataBinding_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Medicine = (Medicine)(sender as ListView).SelectedItem;
                MedicineDescriptionText = Medicines[lvMedicineDataBinding.SelectedIndex].Description;
                VerificationButtonVisibility(Medicine.Verification);

                lvIngredientsDataBinding.ItemsSource = Medicine.Ingredients;
                lvAlternativesDataBinding.ItemsSource = Medicine.Alternatives;
            }
            catch { }
        }
        

        private void VerificationButtonVisibility (VerificationType verification)
        {
            if (verification == VerificationType.needsVerification)
            {
                VerifyButton.Visibility = Visibility.Visible;
                RejectButton.Visibility = Visibility.Visible;
            }
            else
            {
                VerifyButton.Visibility = Visibility.Collapsed;
                RejectButton.Visibility = Visibility.Collapsed;
            }
        }

        private void EditMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorMedicineUpdate doctorMedicineUpdate = new DoctorMedicineUpdate(this);
            doctorMedicineUpdate.Show();
        }

        private void VerifyButton_Click(object sender, RoutedEventArgs e)
        {
            Medicine medicine = Medicines[lvMedicineDataBinding.SelectedIndex];
            app.medicineController.Update(medicine.Id, medicine.Name, VerificationType.verified, medicine.Description);

            VerifyButton.Visibility = Visibility.Collapsed;
            RejectButton.Visibility = Visibility.Collapsed;

            UpdateWindow();
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorVerificationRejection doctorVerificationRejection = new DoctorVerificationRejection(this);
            doctorVerificationRejection.Show();
        }

        public void UpdateWindow()
        {
            Medicines = new ObservableCollection<Medicine>(app.medicineController.GetNotRejected());
            lvMedicineDataBinding.ItemsSource = Medicines;
        }
    }
}
