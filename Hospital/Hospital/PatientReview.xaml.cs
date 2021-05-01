using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using Model;
using DTO;


namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientReview.xaml
    /// </summary>
    public partial class PatientReview : Window
    {
        AppointmentController appointmentController = new AppointmentController();
        EmployeeController employeeController = new EmployeeController();
        public PatientReview()
        {
            InitializeComponent();
            AppointmentsListView.ItemsSource = appointmentController.GetAppointmentsFromPast("5");



        }

        Review getReviewFromForm()
        {
            int information = Int32.Parse(informationTextBox.Text);
            int speed = Int32.Parse(speedTextBox.Text);
            int overall = Int32.Parse(overallTextBox.Text);
            int kindness = Int32.Parse(kindnessTextBox.Text);
            string description = descriptionTextBox.Text;

            Review review = new Review(speed, kindness, information, overall, description);
            return review;

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppointmentDTO appointmentDTO = (AppointmentDTO)AppointmentsListView.SelectedItems[0];
            employeeController.RateDoctor(appointmentDTO.DoctorJmbg,getReviewFromForm());
        }
    }
}
