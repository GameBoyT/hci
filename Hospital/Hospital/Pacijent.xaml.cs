using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for Pacijent.xaml
    /// </summary>
    public partial class Pacijent : Window
    {


        public ObservableCollection<Appointment>Appointments
        {
            get;
            set;
        }
        public Pacijent()
        {
            InitializeComponent();
            
            this.DataContext = this;
            Appointments = new ObservableCollection<Appointment>();
            Appointments.Add(new Appointment { id = 0, timeStart = "12:15", duration = 45 });
            Appointment test = new Appointment { id = 0, timeStart = "12:15", duration = 45 };
            
            Appointments.Add(new Appointment { id = 0, timeStart = "12:15", duration = 45 });
            Appointments.Add(new Appointment { id = 0, timeStart = "12:15", duration = 45 });
            Appointments.Add(new Appointment { id = 0, timeStart = "12:15", duration = 45 });
            Appointments.Add(new Appointment { id = 0, timeStart = "12:15", duration = 45 });
            Debug.WriteLine(Appointments);
        }

        int colNum = 0;

        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            colNum++;
            if (colNum == 3)
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var new_appointment_wnd = new Add_new_appointment();
            new_appointment_wnd.Show();

        }

        
    }
}
