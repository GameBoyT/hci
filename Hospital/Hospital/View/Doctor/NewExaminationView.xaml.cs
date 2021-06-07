using Hospital.ViewModels;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class NewExaminationView : Page
    {
        public NewExaminationView(NewExaminationViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
