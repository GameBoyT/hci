using Hospital.ViewModels;
using System.Windows;

namespace Hospital.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
    }
}
