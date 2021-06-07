using Hospital.ViewModels.Doctor;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class MedicineView : Page
    {
        public MedicineView(MedicinePageViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
