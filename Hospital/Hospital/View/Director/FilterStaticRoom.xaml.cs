using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Model;
using Controller;

namespace Hospital.View.Director
{
    /// <summary>
    /// Interaction logic for FilterStaticRoom.xaml
    /// </summary>
    public partial class FilterStaticRoom : Window
    {
        private StaticEquipmentController equipmentController = new StaticEquipmentController();
        private List<StaticEquipment> staticEquipments = new List<StaticEquipment>();

        public FilterStaticRoom()
        {
            InitializeComponent();
        }

    }
}
