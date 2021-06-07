using Hospital.ViewModels;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class NewOperationView : Page
    {
        public NewOperationView(NewOperationViewModel newOperationViewModel)
        {
            InitializeComponent();
            DataContext = newOperationViewModel;
        }
    }
}
