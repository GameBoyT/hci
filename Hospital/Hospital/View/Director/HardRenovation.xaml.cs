using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Model;
using Controller;
using System.Windows.Controls;

namespace Hospital.View.Director
{
    /// <summary>
    /// Interaction logic for HardRenovation.xaml
    /// </summary>
    public partial class HardRenovation : Window
    {
        private readonly RoomController roomController = new RoomController();
        private List<Room> rooms = new List<Room>();
        
        public HardRenovation()
        {
            InitializeComponent();
            rooms = roomController.GetAll();
            foreach(Room room in rooms)
            {
                roomA.Items.Add(room.Name);
                roomB.Items.Add(room.Name);
                dettachRoom.Items.Add(room.Name);
            }
        }

        private void AcceptRenovation_Click(object sender, RoutedEventArgs e)
        {
            try // promeniti samo ime sobe kad se spajaju 
            {
                if ((bool)attach.IsChecked)
                {
                    Attach(roomA.Text, roomB.Text);
                }
                else
                {
                    Dettach(dettachRoom.Text);
                }
            }
            catch
            {
                MessageBox.Show("Molim Vas da popunite sva polja");
            }
            

        }

        private void Attach(string roomAName, string roomBName)
        {
            Room roomA = roomController.GetByName(roomAName);
            Room roomB = roomController.GetByName(roomBName);
            DateTime test = new DateTime();

            roomController.AttachRooms(roomA.Id, roomB.Id,test ,15);
        }

        private void Dettach(string roomName)
        {
            Room room = roomController.GetByName(roomName);
            DateTime test = new DateTime();
            roomController.DettachRooms(room.Id, test, 15);
        }
    }
}
