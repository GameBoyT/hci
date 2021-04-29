using System.Windows;


namespace Hospital
{

    public partial class DirectorWindow : Window
    {
        public DirectorWindow()
        {
            InitializeComponent();
        }

        private void View_rooms(object sender, RoutedEventArgs e)
        {
            RoomCrud roomWindow = new RoomCrud();
            roomWindow.Show();
            System.Windows.Application.Current.MainWindow.Hide();
        }

        private void ViewEquipments(object sender, RoutedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.Show();
            System.Windows.Application.Current.MainWindow.Hide();
        }
    }
}
