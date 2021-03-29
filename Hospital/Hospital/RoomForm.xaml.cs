using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
