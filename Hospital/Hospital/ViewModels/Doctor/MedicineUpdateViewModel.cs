using Hospital.Commands;
using Hospital.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Doctor
{
    public class MedicineUpdateViewModel : ViewModel
    {
        private MedicineViewModel medicine;

        private string ingredient;

        public Injector Inject { get; set; }
        
        public NavigationService NavService { get; set; }

        public string Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
                OnPropertyChanged();
            }
        }

        public MedicineViewModel Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
                OnPropertyChanged();
            }
        }

        public MedicineViewModel OriginalMedicine { get; set; }

        public ObservableCollection<MedicineViewModel> Medicines { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }
        
        public RelayCommand CancelCommand { get; set; }

        public void Executed_AddCommand(object obj)
        {
            Medicine._Medicine.Ingredients.Add(Ingredient);
            Medicine.Ingredients.Add(Ingredient);
            Ingredient = "";
        }

        public void Executed_SaveCommand(object obj)
        {
            Medicine._Medicine.Description = Medicine.Description;
            Inject.MedicineService.Update(Medicine._Medicine);
            OriginalMedicine = Medicine;
        }

        public void Executed_CancelCommand(object obj)
        {
            Medicine.Alternatives = OriginalMedicine.Alternatives;
            Medicine.Description = OriginalMedicine.Description;
            //Medicine.Ingredients.Clear();
            //foreach (string ingredient in OriginalMedicine.Ingredients)
            //{
            //    Medicine.Ingredients.Add(ingredient);
            //}
            Medicine.Ingredients = OriginalMedicine.Ingredients;
            Ingredient = "";
        }


        public MedicineUpdateViewModel(NavigationService navService, MedicineViewModel medicine)
        {
            Inject = new Injector();
            NavService = navService;
            OriginalMedicine = new MedicineViewModel
            {
                Alternatives = medicine.Alternatives,
                Description = medicine.Description,
                Ingredients = medicine.Ingredients
            };
            Medicine = medicine;
            Medicines = new ObservableCollection<MedicineViewModel>(Inject.MedicineConverter.ConvertCollectionToViewModel(Inject.MedicineService.GetByVerification(Model.VerificationType.verified)));
            AddCommand = new RelayCommand(Executed_AddCommand);
            SaveCommand = new RelayCommand(Executed_SaveCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand);
        }
    }
}
