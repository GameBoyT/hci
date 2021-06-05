using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using Model;
using Hospital.ViewModels.DTO;
using Hospital.Commands;
using Hospital.View.Doctor;

namespace Hospital.ViewModels.Doctor
{
    public class MedicinePageViewModel : ViewModel
    {
        private MedicineViewModel selectedMedicine;

        public Injector Inject { get; set; }

        public MedicineViewModel SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MedicineViewModel> Medicines { get; set; }

        public NavigationService NavService { get; set; }

        public RelayCommand UpdateCommand { get; set; }


        public void Executed_UpdateCommand(object obj)
        {
            MedicineUpdateViewModel vm = new MedicineUpdateViewModel(NavService, SelectedMedicine);
            MedicineUpdateView view = new MedicineUpdateView(vm);
            NavService.Navigate(view);
        }

        public MedicinePageViewModel(NavigationService navigationService)
        {
            Inject = new Injector();
            NavService = navigationService;
            UpdateCommand = new RelayCommand(Executed_UpdateCommand);
            Medicines = new ObservableCollection<MedicineViewModel>(Inject.MedicineConverter.ConvertCollectionToViewModel(Inject.MedicineService.GetNotRejected()));
            SelectedMedicine = Medicines[0];
        }


        //private void VerificationButtonVisibility(VerificationType verification)
        //{
        //    if (verification == VerificationType.needsVerification)
        //    {
        //        VerifyButton.Visibility = Visibility.Visible;
        //        RejectButton.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        VerifyButton.Visibility = Visibility.Collapsed;
        //        RejectButton.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void EditMedicineButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DoctorMedicineUpdate doctorMedicineUpdate = new DoctorMedicineUpdate(this);
        //    doctorMedicineUpdate.Show();
        //}

        //private void VerifyButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Medicine medicine = Medicines[lvMedicineDataBinding.SelectedIndex];
        //    app.medicineController.Update(medicine.Id, medicine.Name, VerificationType.verified, medicine.Description);

        //    VerifyButton.Visibility = Visibility.Collapsed;
        //    RejectButton.Visibility = Visibility.Collapsed;

        //    UpdateWindow();
        //}

        //private void RejectButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DoctorVerificationRejection doctorVerificationRejection = new DoctorVerificationRejection(this);
        //    doctorVerificationRejection.Show();
        //}

        //public void UpdateWindow()
        //{
        //    Medicines = new ObservableCollection<Medicine>(app.medicineController.GetNotRejected());
        //    lvMedicineDataBinding.ItemsSource = Medicines;
        //}
    }
}
