using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var new_window = new Pacijent();
            new_window.Show();
            App.Current.MainWindow.Hide();




        }

        private void roomOptions(object sender, RoutedEventArgs e)
        {
            RoomOptions room = new RoomOptions();
            room.Show();
        }
    }
}
