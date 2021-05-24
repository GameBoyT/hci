using Controller;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientReferral.xaml
    /// </summary>
    public partial class PatientReferral : Window
    {
        PatientController patientController = new PatientController();
        EmployeeController employeeController = new EmployeeController();
        AppointmentController appointmentController = new AppointmentController();
        List<Referral> referrals;
        ObservableCollection<ReferralDTO> referralDTOs = new ObservableCollection<ReferralDTO>();
        Patient patient;
        ReferralDTO selectedDTO;
        public PatientReferral()
        {
            InitializeComponent();
            patient = patientController.GetByJmbg("5");
            referrals = patient.MedicalRecord.Referrals;
            foreach (Referral referral in referrals)
            {

                ReferralDTO referralDTO = new ReferralDTO(employeeController.GetByJmbg(referral.DoctorJmbg), referral.Description);
                referralDTOs.Add(referralDTO);
            }
            ReferralListView.ItemsSource = referralDTOs;

        }

        private void UpdateReferralList(ReferralDTO toDeleteReferral)
        {

            referrals.RemoveAt(referralDTOs.IndexOf(toDeleteReferral));
            referralDTOs.Remove(toDeleteReferral);

            patient.MedicalRecord.Referrals = referrals;
            patientController.Update(patient);

        }

        private AppointmentDTO CreateAppointmentFromData()
        {

            //int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            selectedDTO = (ReferralDTO)ReferralListView.SelectedItems[0];
            Employee doctor = selectedDTO.Doctor;

            return new AppointmentDTO(MedicalAppointmentType.examination, appointmentDateTime, duration, patient.User.Jmbg, doctor.User.Jmbg, doctor.RoomId, patient.User.Jmbg);
        }

        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentDTO newAppointment = CreateAppointmentFromData();
            if (!appointmentController.AppointmentIsTaken(newAppointment, newAppointment.DoctorJmbg))
            {
                if (!appointmentController.AppointmentTimeIsInvalid(newAppointment))
                {
                    appointmentController.Save(newAppointment);
                    UpdateReferralList(selectedDTO);
                    MessageBox.Show("Specijalisticki pregled uspjesno dodat", "obavjestenje");

                }

            }
            else
            {
                MessageBox.Show("Nije moguce dodati novi specijalisticki pregleda na taj termin", "greska");
            }

        }

        private void NewAppointmentClick(object sender, RoutedEventArgs e)
        {
                var new_window = new PatientWindow();
                new_window.Show();
                this.Close();
        }

        private void showAppointments_Click(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientAppointments();
            new_window.Show();
            this.Close();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientReview();
            new_window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientReferral();
            new_window.Show();
            this.Close();

        }

         private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var newWindow = new PatientMedicalRecord();
            newWindow.Show();
            //this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var newWindow = new PatientReminder();
            newWindow.Show();
            //this.Close();
        }

        
    }
}
