using DTO;
using Hospital.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View.Doctor
{
    public partial class DoctorViewPatient : Window, INotifyPropertyChanged
    {
        App app;
        public AppointmentDTO Appointment { get; set; }
        public Patient Patient { get; set; }
        public Medicine Medicine { get; set; }
        public StaticEquipment HospitalStayBed { get; set; }
        public ObservableCollection<Anamnesis> Anamnesis { get; set; }
        public ObservableCollection<Anamnesis> PastAppointments { get; set; }
        public Anamnesis PastAppointment { get; set; }
        public ObservableCollection<Prescription> Prescriptions { get; set; }

        private List<Employee> Doctors { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAppointmentTime;
        private string detailText;
        private string descriptionText;
        private string quantity;
        private string referralDescriptionText;
        private string details;

        public string DetailText
        {
            get { return detailText; }
            set
            {
                if (detailText != value)
                {
                    detailText = value;
                    OnPropertyChanged("detailText");
                }
            }
        }

        public string DescriptionText
        {
            get { return descriptionText; }
            set
            {
                if (descriptionText != value)
                {
                    descriptionText = value;
                    OnPropertyChanged("descriptionText");
                }
            }
        }

        public string Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged("quantity");
                }
            }
        }

        public bool IsAppointmentTime
        {
            get { return isAppointmentTime; }
            set
            {
                if (isAppointmentTime != value)
                {
                    isAppointmentTime = value;
                    OnPropertyChanged("isAppointmentTime");
                }
            }
        }

        public string ReferralDescriptionText
        {
            get { return referralDescriptionText; }
            set
            {
                if (referralDescriptionText != value)
                {
                    referralDescriptionText = value;
                    OnPropertyChanged("referralDescriptionText");
                }
            }
        }

        public string Details
        {
            get { return details; }
            set
            {
                if (details != value)
                {
                    details = value;
                    OnPropertyChanged("detailText");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DoctorViewPatient(AppointmentViewModel appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            app = Application.Current as App;

            Appointment = new AppointmentDTO
               (
                   appointment.Id,
                   appointment.MedicalAppointmentType,
                   appointment.StartTime,
                   appointment.DurationInMinutes,
                   appointment.Doctor.Jmbg,
                   appointment.Doctor.FirstName,
                   appointment.Doctor.LastName,
                   appointment.Doctor.Specialization,
                   appointment.Patient.Jmbg,
                   appointment.Patient.FirstName,
                   appointment.Patient.LastName,
                   appointment.Room.Id,
                   appointment.Room.Name
               );

            if (!app.appointmentController.IsTimeInFuture(Appointment.StartTime))
                IsAppointmentTime = true;
            Patient = app.patientController.GetByJmbg(Appointment.PatientJmbg);

            Doctors = app.employeeController.GetDoctors();

            Anamnesis = new ObservableCollection<Anamnesis>(Patient.MedicalRecord.Anamnesis);
            Prescriptions = new ObservableCollection<Prescription>(Patient.MedicalRecord.Prescription);
            lvDataBinding.ItemsSource = Anamnesis;
            lvPrescriptionDataBinding.ItemsSource = app.medicineController.GetByVerification(VerificationType.verified);
            lvPatientPrescriptionDataBinding.ItemsSource = Prescriptions;
            doctorsDataGrid.ItemsSource = Doctors;

            lvHospitalStayRoomsDataBinding.ItemsSource = app.roomController.GetRoomsByRoomType(RoomType.patients);
            hospitalStayStartDate.SelectedDate = DateTime.Today;

            InitPastAppointments();
        }


        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if (!app.appointmentController.IsTimeInFuture(Appointment.StartTime))
                {
                    IsAppointmentTime = true;
                }
                DetailText = Anamnesis[lvDataBinding.SelectedIndex].Description;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Anamnesis selectedAnamnesis = Anamnesis[lvDataBinding.SelectedIndex];
            Anamnesis updatedAnamnesis = app.patientController.UpdateAnamnesisDescription(Patient.User.Jmbg, selectedAnamnesis.Id, DetailText);
            selectedAnamnesis.Description = updatedAnamnesis.Description;
            MessageBox.Show("Anamnesis successfully updated!");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DetailText = Anamnesis[lvDataBinding.SelectedIndex].Description;
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            if (app.appointmentController.IsTimeInFuture(Appointment.StartTime))
            {
                MessageBox.Show("You cannot add anamnesis until or after the appointment");
            }
            else
            {
                DoctorNewAnamnesis doctorNewAnamnesisWindow = new DoctorNewAnamnesis(this);
                doctorNewAnamnesisWindow.Show();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listViewPrescription_Click(object sender, RoutedEventArgs e)
        {
            Medicine = (Medicine)(sender as ListView).SelectedItem;
        }

        private void PrescriptionAddButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (string ingredient in Medicine.Ingredients)
            {
                if (Patient.MedicalRecord.Alergies.Contains(ingredient))
                {
                    MessageBox.Show("The patient is alergic to a medicine ingredient!");
                    return;
                }
            }
            try
            {
                Prescription prescription = app.patientController.AddPrescription(Appointment.PatientJmbg, Medicine, Int32.Parse(Quantity), DescriptionText);
                DescriptionText = "";
                Quantity = "";
                Prescriptions.Add(prescription);
                MessageBox.Show("Prescription successfully updated!");
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void specializationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            doctorsDataGrid.ItemsSource = Doctors.FindAll(obj => obj.Specialization.IndexOf(specializationName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void ReferralAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReferralDescriptionText == "" || ReferralDescriptionText == null)
            {
                MessageBox.Show("You have to enter a description!");
                return;
            }

            try
            {
                Employee doctor = (Employee)doctorsDataGrid.SelectedItems[0];
                app.patientController.AddReferral(Appointment.PatientJmbg, doctor.User.Jmbg, ReferralDescriptionText);
                ReferralDescriptionText = "";
                MessageBox.Show("Referral successfully updated!");
            }
            catch
            {
                MessageBox.Show("Choose a doctor!");
            }
        }


        private void listViewHospitalStayRooms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = (Room)(sender as ListView).SelectedItem;
                lvRoomBedsDataBinding.ItemsSource = room.StaticEquipments;
            }
            catch { }
        }

        private void listViewRoomBeds_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HospitalStayBed = (StaticEquipment)(sender as ListView).SelectedItem;
            }
            catch { }
        }

        private void HospitalStayAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (app.appointmentController.IsTimeInFuture(Appointment.StartTime))
            {
                MessageBox.Show("You cannot add an hospital stay until or after the appointment");
                return;
            }

            DateTime startDate = hospitalStayStartDate.SelectedDate.Value;
            DateTime endDate = hospitalStayEndDate.SelectedDate.Value;

            if (startDate.DayOfYear < DateTime.Now.DayOfYear)
            {
                MessageBox.Show("You can't select a before today!");
                return;
            }

            if (endDate.DayOfYear < startDate.DayOfYear )
            {
                MessageBox.Show("You can't select a start day before the end date!");
                return;
            }

            try
            {
                app.patientController.AddHospitalStay(Patient.User.Jmbg, HospitalStayBed, startDate, endDate);
                MessageBox.Show("Hospital stay successfully added!");
            }
            catch
            {
                MessageBox.Show("You have to select all fields!");
            }
        }

        private void PastAppointmentsListView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                PastAppointment = PastAppointments[PastAppointmentsListView.SelectedIndex];
                PADescription.Text = PastAppointment.Description;
            }
        }
        
        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            PastAppointmentsReportWindow pastAppointmentsReportWindow = new PastAppointmentsReportWindow(PastAppointments, BeginningDate.SelectedDate.Value, EndDate.SelectedDate.Value);
            pastAppointmentsReportWindow.Show();
        }

        private void InitPastAppointments()
        {
            PastAppointments = new ObservableCollection<Anamnesis>();
            PastAppointment = new Anamnesis();
            Anamnesis a = new Anamnesis
            {
                Name = "23.9.2020 - Examination",
                Description = "Pacijent je imao glavobolju, propisan andol"
            };
            Anamnesis b = new Anamnesis
            {
                Name = "16.2.2021 - Operation",
                Description = "Uspjesno obavljena operacija srca"
            };
            Anamnesis c = new Anamnesis
            {
                Name = "27.9.2020 - Examination",
                Description = "Stanje pacijenta se poboljsalo"
            };
            PastAppointments.Add(a);
            PastAppointments.Add(c);
            PastAppointments.Add(b);
        }


    }
}
