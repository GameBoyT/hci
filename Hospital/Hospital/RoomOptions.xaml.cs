
using System.Windows;


namespace Hospital
{

    public partial class RoomOptions : Window
    {
        public RoomOptions()
        {
            InitializeComponent();
        }

        private void createRooms(object sender, RoutedEventArgs e)
        {
            RoomForm room = new RoomForm();
            room.Show();
        }
    }
}
