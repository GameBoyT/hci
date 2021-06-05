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

        public MedicineViewModel Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
            }
        }

        public ObservableCollection<MedicineAlternativeViewModel> Alternatives { get; set; }

        public ObservableCollection<MedicineAlternativeViewModel> Medicines { get; set; }


        public MedicineUpdateView(MedicineViewModel medicine)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            List<Medicine> m =  Inject.MedicineService.GetByVerification(VerificationType.verified);
            Medicines = new ObservableCollection<MedicineAlternativeViewModel>(Inject.MedicineConverter.ConvertCollectionToAlternativeViewModel(m));
            Alternatives = medicine.Alternatives;

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
                Alternatives.Add(medicine);
            }
        }
    }
}
