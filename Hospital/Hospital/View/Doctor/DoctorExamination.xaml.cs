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
    public partial class DoctorExamination : Window
    {
        public DoctorWindow ParentWindow { get; set; }
        public DoctorExamination(DoctorWindow parentWindow)
        {
            InitializeComponent();

            ParentWindow = parentWindow;
        }
    }
}
