using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for RoomForm.xaml
    /// </summary>
    public partial class RoomForm : Window
    {
        public RoomForm()
        {
            InitializeComponent();
        }

        private void dodato(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Uspesno ste dodali prostoriju");
        }

        private void ponisteno(object sender, RoutedEventArgs e)
        {
            id.Text = "";
            name.Text = "";
            floor.Text = "";
            detail.Text = "";
            MessageBox.Show("Dodavanje prostorije nije uspeno");
        }
    }
}
