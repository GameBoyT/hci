using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Model;
using Controller;
using System.Windows.Controls;

namespace Hospital.View.Director
{
    public partial class HardRenovation : Window
    {
        private readonly RoomController roomController = new RoomController();
        private readonly RenovationController renovationController = new RenovationController();
        
        
        public HardRenovation()
        {
            InitializeComponent();
            List<Room> rooms = roomController.GetAll();
            foreach(Room room in rooms)
            {
                roomA.Items.Add(room.Name);
                roomB.Items.Add(room.Name);
                dettachRoom.Items.Add(room.Name);
            }
        }

        private void AcceptRenovation_Click(object sender, RoutedEventArgs e)
        {
            try 
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

            DateTime renovationDateTime = SelectedDate();

            bool goodDate = renovationController.AttachRooms(roomA.Id, roomB.Id, renovationDateTime, Double.Parse(duration.Text));
            Greska(goodDate);
            
        }

        private void Dettach(string roomName)
        {
            Room room = roomController.GetByName(roomName);
            DateTime renovationDateTime = SelectedDate();

            bool goodDate = renovationController.DettachRooms(room.Id, renovationDateTime, Double.Parse(duration.Text));
            Greska(goodDate);
        }
        private DateTime SelectedDate()
        {
            DateTime pickedDate = date.SelectedDate.Value;
            int hours = Int32.Parse(startTime.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTime.Text.Split(':')[1]);
            DateTime renovationDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            return renovationDateTime;
        }

        private void Greska(bool goodDate)
        {
            if (!goodDate)
            {
                MessageBox.Show("Datum je vec zauzet, odaberite drugi");
            }
            else
            {
                MessageBox.Show("Uspesno ste zakazali renoviranje");
            }
        }
    }
}
