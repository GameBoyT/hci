using Hospital.ViewModels.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital.VMConverters
{
    public class MedicineConverter
    {
        public MedicineViewModel ConvertModelToViewModel(Medicine medicine)
        {
            MedicineViewModel medicineViewModel = new MedicineViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                DoctorComment = medicine.DoctorComment,
                Alternatives = ConvertCollectionToAlternativeViewModel(medicine.Alternatives),
                Ingredients = new ObservableCollection<string>(medicine.Ingredients),
                _Medicine = medicine
            };

            return medicineViewModel;
        }

        public ObservableCollection<MedicineViewModel> ConvertCollectionToViewModel(List<Medicine> medicines)
        {
            ObservableCollection<MedicineViewModel> vmMedicines = new ObservableCollection<MedicineViewModel>();
            MedicineViewModel medicineViewModel;
            foreach (Medicine medicine in medicines)
            {
                medicineViewModel = ConvertModelToViewModel(medicine);
                vmMedicines.Add(medicineViewModel);
            }
            return vmMedicines;
        }

        public MedicineAlternativeViewModel ConvertMedicineToAlternativeViewModel(Medicine medicine)
        {
            MedicineAlternativeViewModel medicineViewModel = new MedicineAlternativeViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                _Medicine = medicine
            };
            return medicineViewModel;
        }

        public ObservableCollection<MedicineAlternativeViewModel> ConvertCollectionToAlternativeViewModel(List<Medicine> medicines)
        {
            ObservableCollection<MedicineAlternativeViewModel> vmMedicines = new ObservableCollection<MedicineAlternativeViewModel>();
            MedicineAlternativeViewModel medicineViewModel;
            foreach (Medicine medicine in medicines)
            {
                medicineViewModel = ConvertMedicineToAlternativeViewModel(medicine);
                vmMedicines.Add(medicineViewModel);
            }
            return vmMedicines;
        }
    }
}
