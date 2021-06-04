using Hospital.ViewModels;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class DoctorExaminationView : Page
    {
        public DoctorExaminationView(ExaminationViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
