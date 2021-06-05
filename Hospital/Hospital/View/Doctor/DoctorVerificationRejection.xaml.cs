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

namespace Hospital.View.Doctor
{
    public partial class DoctorVerificationRejection : Window
    {
        App app;
        public DoctorMedicine ParentWindow { get; set; }

        public DoctorVerificationRejection(DoctorMedicine parentWindow)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;

            ParentWindow = parentWindow;
        }
        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedicineRejectionText.Text != "")
            {
                app.medicineController.RejectMedicine(ParentWindow.Medicine.Id, MedicineRejectionText.Text);

                ParentWindow.UpdateWindow();
                this.Close();
            }
            else
            {
                MessageBox.Show("You have to enter the reason for the rejection!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
