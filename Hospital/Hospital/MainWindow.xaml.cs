using System.Windows;
using Service;
using System;
using Model;
using System.Collections.Generic;
using Hospital.View.Doctor;
using Hospital.ViewModels;

namespace Hospital
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this.frame.NavigationService);
        }
    }
}
