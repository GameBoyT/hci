using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Model;
using Controller;

namespace Hospital.View.Director
{
    /// <summary>
    /// Interaction logic for MedicineCrud.xaml
    /// </summary>
    public partial class MedicineCrud : Window
    {

        private readonly MedicineController medController = new MedicineController();
        private int id; // pomaze oko update 
        public MedicineCrud()
        {
            InitializeComponent();
            medicineDataGrid.ItemsSource = medController.GetVerified();
            needsToVerifyDataGrid.ItemsSource = medController.GetNotVerified();
        }


        private Medicine CreateMed()
        {
            int id = medController.GenerateNewId();
            string medicineName = name.Text;
            string medicineDescription = description.Text;
            medController.Save(medicineName, medicineDescription);
            return new Medicine(id, medicineName, medicineDescription, " ");
        }

        private void New_Medicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Medicine med = CreateMed();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }
        private void Delete_Med_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Medicine med = (Medicine)medicineDataGrid.SelectedItems[0];
                medController.Delete(med.Id);
            }
            catch
            {
                MessageBox.Show("You have to select a room to delete!");
            }
        }

        private void UpdateMedShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Medicine med = (Medicine)medicineDataGrid.SelectedItems[0];
                id = med.Id;
                createMedicine.Visibility = Visibility.Collapsed;
                updateMedButton.Visibility = Visibility.Visible;
                cancelupdateMedButton.Visibility = Visibility.Visible;
                title.Content = "Edit medication";

                name.Text = med.Name;
                description.Text = med.Description;

            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void Update_Med_Click(object sender, RoutedEventArgs e)
        {
            string medicineName = name.Text;
            string medicineDescription = description.Text;
            medController.Update(id, medicineName, 0, medicineDescription);
            id = -1;
        }

        private void CancelupdateMedButton_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            description.Text = "";


            createMedicine.Visibility = Visibility.Visible;
            updateMedButton.Visibility = Visibility.Collapsed;
            cancelupdateMedButton.Visibility = Visibility.Collapsed;
            title.Content = "Create new med";
        }
    }
}
