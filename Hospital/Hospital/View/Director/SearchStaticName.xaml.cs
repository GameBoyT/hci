using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Model;
using Controller;

namespace Hospital.View.Director
{
    /// <summary>
    /// Interaction logic for SearchStaticName.xaml
    /// </summary>
    public partial class SearchStaticName : Window
    {
        private StaticEquipmentController equipmentController = new StaticEquipmentController();
        public SearchStaticName()
        {
            InitializeComponent();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            StaticDataGrid.ItemsSource = equipmentController.GetByName(searchBar.Text);
        }
    }
}
