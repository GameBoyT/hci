using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Controller;
using Model;

namespace Hospital
{

    public partial class Equipment : Window
    {
        private StaticEquipmentController staticController = new StaticEquipmentController();

        List<StaticEquipment> staticEquipment = new List<StaticEquipment>();
        private DynamicEquipmentController dynamicController = new DynamicEquipmentController();

        List<DynamicEquipment> dynamicEquipment = new List<DynamicEquipment>();
        int id;
        public Equipment()
        {
            InitializeComponent();
            staticEquipment = staticController.GetAll();
            StaticDataGrid.ItemsSource = staticEquipment;
            dynamicEquipment = dynamicController.GetAll();
            DynamicDataGrid.ItemsSource = dynamicEquipment;
        }

        private StaticEquipment CreateEquipment()
        {
            int id = staticController.GenerateNewId();
            string textname = name.Text;
            string desc = description.Text;
            int quant = Int32.Parse(quantity.Text);
            staticController.Save(textname, "302", quant, desc);
            //Room room = new Room(101, textname, (RoomType)1, 4, "test soba");
            return new StaticEquipment(id, textname, null, quant, desc);
        }

        private void addEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StaticEquipment equipment = CreateEquipment();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void updateEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StaticEquipment equipment = (StaticEquipment)StaticDataGrid.SelectedItems[0];
                staticController.Update(equipment);
            }
            catch
            {
                MessageBox.Show("You have to select an equipmnet to update!");
            }
        }

        private void deleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StaticEquipment equipment = (StaticEquipment)StaticDataGrid.SelectedItems[0];
                staticController.Delete(equipment.Id);
            }
            catch
            {
                MessageBox.Show("You have to select an equipmnet to delete!");
            }
        }

        private DynamicEquipment CreateDynamic()
        {
            int id = dynamicController.GenerateNewId();
            string textname = dynName.Text;
            string desc = dynDescription.Text;
            int quant = Int32.Parse(dynQuantity.Text);
            DynamicEquipment dynamic = new DynamicEquipment(id, textname, quant, desc);
            dynamicController.Save(dynamic);
            return new DynamicEquipment(id, textname, quant, desc);
        }

        private void addDynamic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DynamicEquipment equipment = CreateDynamic();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void deleteDynamic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DynamicEquipment equipment = (DynamicEquipment)DynamicDataGrid.SelectedItems[0];
                dynamicController.Delete(equipment.Id);
            }
            catch
            {
                MessageBox.Show("You have to select an equipment to delete!");
            }
        }

        private void updateDynamic_Show(object sender, RoutedEventArgs e)
        {
            try
            {
                DynamicEquipment equipment = (DynamicEquipment)DynamicDataGrid.SelectedItems[0];
                dynamicController.Update(equipment);
            }
            catch
            {
                MessageBox.Show("You have to select an equipment to update!");
            }
        }

        private void updateDynamic_Click(object sender, RoutedEventArgs e)
        {
            string textname = dynName.Text;
            string desc = dynDescription.Text;
            int quant = Int32.Parse(dynQuantity.Text);
            DynamicEquipment equipment = new DynamicEquipment(id, textname, quant, desc);
            dynamicController.Update(equipment);
            id = -1;
        }

        private void cancelupdateDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            quantity.Text = "";
            description.Text = "";

            addDynamic.Visibility = Visibility.Visible;
            updateDynamicButton.Visibility = Visibility.Collapsed;
            cancelupdateDynamicButton.Visibility = Visibility.Collapsed;
            title.Content = "Create new room";
        }
    }
}
