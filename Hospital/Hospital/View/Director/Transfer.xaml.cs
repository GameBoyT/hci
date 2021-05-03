using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using Model;
using Controller;
using System.IO;
using System;

namespace Hospital.View.Director
{
    /// <summary>
    /// Interaction logic for Transfer.xaml
    /// </summary>
    public partial class Transfer : Window
    {
        DynamicEquipmentController dynamicController = new DynamicEquipmentController();
        List<DynamicEquipment> dynamicEquipment = new List<DynamicEquipment>();
        int pomocniId;
        string pomocniDescription;
        public Transfer()
        {
            InitializeComponent();
            dynamicEquipment = dynamicController.GetAll();
            DynamicDataGrid.ItemsSource = dynamicEquipment;
            lb_transfers.Items.Clear();
            foreach (string line in File.ReadAllLines("dynamicTransfer.txt"))
            {
                lb_transfers.Items.Add(line);
            }
        }

        private void AddDynamic_Click(object sender, RoutedEventArgs e)
        {
            string textname = dynName.Text;
            int quant = Int32.Parse(dynQuantity.Text);
            DynamicEquipment equipment = new DynamicEquipment(pomocniId, textname, quant, pomocniDescription);
            dynamicController.MoveDynamicEquipment(equipment);
            pomocniDescription = "";
            pomocniId = -1;
        }

        private void MoveDynamic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DynamicEquipment equipment = (DynamicEquipment)DynamicDataGrid.SelectedItems[0];
                pomocniId = equipment.Id;
                pomocniDescription = equipment.Description;
                cancelMoving.Visibility = Visibility.Visible;

                dynName.Text = equipment.Name;
                dynQuantity.Text = equipment.Quantity.ToString();
            }
            catch
            {
                MessageBox.Show("You have to fill in all input boxes!");
            }
        }

        private void CancelMoving_Click(object sender, RoutedEventArgs e)
        {
            dynName.Text = "";
            dynQuantity.Text = "";
        }
    }
}
