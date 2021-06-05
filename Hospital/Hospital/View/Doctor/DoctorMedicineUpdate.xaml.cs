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
    public partial class DoctorMedicineUpdate : Window, INotifyPropertyChanged
    {
        App app;
        public event PropertyChangedEventHandler PropertyChanged;

        public DoctorMedicine ParentWindow { get; set; }

        public Medicine Medicine { get; set; }

        public ObservableCollection<string> Ingredients { get; set; }

        public ObservableCollection<Medicine> Alternatives { get; set; }

        public ObservableCollection<Medicine> Medicines { get; set; }


        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("description");
                }
            }
        }

        private string ingredient;

        public string Ingredient
        {
            get { return ingredient; }
            set
            {
                if (ingredient != value)
                {
                    ingredient = value;
                    OnPropertyChanged("ingredient");
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

        public DoctorMedicineUpdate(DoctorMedicine parentWindow)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;
            ParentWindow = parentWindow;
            Medicine = parentWindow.Medicine;
            Medicines = new ObservableCollection<Medicine>(app.medicineController.GetByVerification(VerificationType.verified));

            Ingredients = new ObservableCollection<string>(Medicine.Ingredients);
            Alternatives = new ObservableCollection<Medicine>(Medicine.Alternatives);

            Description = Medicine.Description;
            lvIngredientsDataBinding.ItemsSource = Ingredients;
            lvAlternativesDataBinding.ItemsSource = Alternatives;
        }

        private void IngredinetAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ingredients.Add(Ingredient);
                Medicine.Ingredients.Add(Ingredient);
                Ingredient = "";
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }



        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Medicine.Description = Description;
            app.medicineController.Update(Medicine);

            ParentWindow.UpdateWindow();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
