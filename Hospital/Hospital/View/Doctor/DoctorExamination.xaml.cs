using DTO;
using Hospital.ViewModels;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital.View.Doctor
{
    public partial class DoctorExamination : Window
    {
        public DoctorExamination()
        {
            InitializeComponent();
            this.DataContext = new ExaminationViewModel();
        }
    }
}
