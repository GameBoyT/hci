using Hospital.ViewModels;
using System.Windows;

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
