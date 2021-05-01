using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;


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
            List<Appointment> allAppointments = appointmentController.GetAppointmentsForPatient("5");
            List<Appointment> appointmentsInPast = new List<Appointment>();
            foreach (Appointment appointment in allAppointments)
            {
                if (appointment.StartTime < DateTime.Now)
                {
                    appointmentsInPast.Add(appointment);
                }
            }
            AppointmentsListView.ItemsSource = appointmentsInPast;



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

            Appointment appointment = (Appointment)AppointmentsListView.SelectedItems[0];
            employeeController.RateDoctor(appointment.DoctorJmbg, getReviewFromForm());
        }
    }
}
