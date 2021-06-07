using Hospital.ViewModels;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class UpdateExaminationView : Page
    {
        public UpdateExaminationView(UpdateExaminationViewModel updateExaminationViewModel)
        {
            InitializeComponent();
            DataContext = updateExaminationViewModel;
        }
    }
}
