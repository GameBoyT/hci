using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Controller;
using Model;

namespace Hospital.View.Director
{
    /// <summary>
    /// Interaction logic for Renovation.xaml
    /// </summary>
    public partial class Renovation : Window
    {
        private readonly RoomController roomController = new RoomController();

        public Renovation()
        {
            InitializeComponent();
            roomsDataGrid.ItemsSource = roomController.GetAll();
            
        }

        private void AcceptRenovation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = (Room)roomsDataGrid.SelectedItems[0];
                DateTime renovationDate = SelectedDate();
                bool goodDate = roomController.Renovation(room.Id ,renovationDate, Double.Parse(duration.Text));

                if (!goodDate)
                {
                    MessageBox.Show("Ponovo zauzmite datum");
                }
                else
                {
                    MessageBox.Show("Uspesno ste zakazali renoviranje");
                }
            }
            catch
            {
                MessageBox.Show("Niste zauzeli datum");               
            }

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
