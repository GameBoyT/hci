using System;
using System.Collections.Generic;
using Model;
using System.Windows;
using Controller;

namespace Hospital.View.Director
{

    public partial class StaticTransfer : Window
    {
        private readonly StaticEquipmentController staticEquipmentController = new StaticEquipmentController();
        private readonly RoomController roomController = new RoomController();
        private readonly MovingStaticController movingStaticController = new MovingStaticController();
        int staticId;
        
        public StaticTransfer()
        {
            InitializeComponent();
            staticDataGrid.ItemsSource = staticEquipmentController.GetAll();
            MoviStaticEquipment();

        }
        private void MoviStaticEquipment()
        {
            List<MovingStatic> listOfStatic = movingStaticController.GetAll();
            foreach (MovingStatic staticToMove in listOfStatic)
            {
                if (staticToMove.DateTime.Ticks <= DateTime.Now.Ticks)
                {
                    roomController.MoveStaticEquipment(staticToMove.StaticId, staticToMove.RoomId);
                    movingStaticController.Delete(staticToMove.Id);
                }

            }

        }

        private void Move_To_Transfer_Click(object sender, RoutedEventArgs e)
        {
            StaticEquipment equipment = (StaticEquipment)staticDataGrid.SelectedItems[0];
            acceptButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;

            staticId = equipment.Id;
            name.Text = equipment.Name;
            toRoom.Text = equipment.RoomId.ToString();
        }

        private void Accept_Transfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime transferTime = SelectedDate();

                movingStaticController.Save(staticId, Int32.Parse(toRoom.Text), transferTime);
                staticId = -1;
                Cancel_Transfer_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Please enter the date of transfer");
            }
            
        }
        private void Cancel_Transfer_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            toRoom.Text = "";
            acceptButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
        }
        private DateTime SelectedDate()
        {
            DateTime pickedDate = date.SelectedDate.Value;
            int hours = Int32.Parse(startTime.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTime.Text.Split(':')[1]);
            DateTime renovationDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            return renovationDateTime;
        }
    }
}
