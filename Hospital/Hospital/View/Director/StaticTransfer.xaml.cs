using System;
using System.Collections.Generic;
using Model;
using System.Windows;
using Controller;

namespace Hospital.View.Director
{

    public partial class StaticTransfer : Window
    {
        private StaticEquipmentController staticEquipmentController = new StaticEquipmentController();
        private RoomController roomController = new RoomController();
        int id = 1;
        private List<StaticEquipment> staticEquipments = new List<StaticEquipment>();
        
        public StaticTransfer()
        {
            InitializeComponent();
            staticDataGrid.ItemsSource = staticEquipmentController.GetAll();

        }

        private void Move_To_Transfer_Click(object sender, RoutedEventArgs e)
        {
            StaticEquipment equipment = (StaticEquipment)staticDataGrid.SelectedItems[0];
            acceptButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;

            id = equipment.Id;
            name.Text = equipment.Name;
            toRoom.Text = equipment.RoomId.ToString();
        }

        private void Accept_Transfer_Click(object sender, RoutedEventArgs e)
        {
            DateTime pickerDate = transfer_date.SelectedDate.Value;
            DateTime TransferDateTime = new DateTime(pickerDate.Year, pickerDate.Month, pickerDate.Day, 00, 00, 00);
            roomController.MoveStaticEquipment(id, Int32.Parse(toRoom.Text), TransferDateTime);
            Cancel_Transfer_Click(sender, e);
        }
        private void Cancel_Transfer_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            toRoom.Text = "";
            acceptButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
        }
    }
}
