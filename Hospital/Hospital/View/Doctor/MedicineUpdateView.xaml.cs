using Hospital.ViewModels.Doctor;
using Hospital.ViewModels.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Doctor
{
    public partial class MedicineUpdateView : Page
    {
        Point startPoint = new Point();
        
        private MedicineViewModel medicine;


        public Injector Inject { get; set; }

        public string Ingredient { get; set; }

        public string Description { get; set; }

        public MedicineViewModel Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
            }
        }

        public ObservableCollection<MedicineAlternativeViewModel> Alternatives { get; set; }

        public ObservableCollection<string> Ingredients { get; set; }

        public ObservableCollection<MedicineAlternativeViewModel> Medicines { get; set; }
        
        public ObservableCollection<MedicineAlternativeViewModel> OriginalMedicines { get; set; }


        public MedicineUpdateView(MedicineViewModel medicine)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            List<Medicine> m =  Inject.MedicineService.GetAvaliableAlternatives(medicine.Id);
            OriginalMedicines = Inject.MedicineConverter.ConvertCollectionToAlternativeViewModel(m);
            Medicines = new ObservableCollection<MedicineAlternativeViewModel>(OriginalMedicines);
            Alternatives = new ObservableCollection<MedicineAlternativeViewModel>(medicine.Alternatives);
            Ingredients = new ObservableCollection<string>(medicine.Ingredients);
            Description = medicine.Description;
            Medicine = medicine;

        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem == null) return;

                // Find the data behind the ListViewItem
                MedicineAlternativeViewModel student = (MedicineAlternativeViewModel)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", student);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                MedicineAlternativeViewModel medicine = e.Data.GetData("myFormat") as MedicineAlternativeViewModel;
                Medicines.Remove(medicine);
                Medicine.Alternatives.Add(medicine);
                Medicine._Medicine.Alternatives.Add(Inject.MedicineService.GetById(medicine.Id));
            }
        }

        private void AddingredientBtnClick(object sender, RoutedEventArgs e)
        {
            Medicine._Medicine.Ingredients.Add(Ingredient);
            Medicine.Ingredients.Add(Ingredient);
            Ingredient = "";
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            Medicine._Medicine.Description = Medicine.Description;
            Inject.MedicineService.Update(Medicine._Medicine);
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            Medicine.Alternatives = new ObservableCollection<MedicineAlternativeViewModel>(Alternatives);
            Medicine.Description = Description;
            Medicine.Ingredients = new ObservableCollection<string>(Ingredients);
            Medicines = new ObservableCollection<MedicineAlternativeViewModel>(OriginalMedicines);
            Ingredient = "";

            AlternativesListView.ItemsSource = Medicine.Alternatives;
            IngredientsListView.ItemsSource = Medicine.Ingredients;
            AllMedicineListView.ItemsSource = Medicines;
        }
    }
}
