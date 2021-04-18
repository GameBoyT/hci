using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Controller;
using System.Diagnostics;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        DoctorController doctorController = new DoctorController();
        AppointmentController appointmentController = new AppointmentController();
        RoomController roomController = new RoomController();
        List<Doctor> doctors = new List<Doctor>();
        Patient patient;
        PatientController patientController = new PatientController();
        Doctor doctor;
        public PatientWindow()
        {
            InitializeComponent();
            /*//PATIENT DATA GEN
            DateTime date2 = new DateTime(1998, 6, 12);
            DateTime date3 = new DateTime(1973, 8, 9);
            DateTime medicineStart1 = new DateTime(2021, 4, 17, 12, 20, 00);
            DateTime medicineStart2 = new DateTime(2021, 3, 20, 14, 00, 00);
            DateTime medicineEnd1 = new DateTime(2021, 6, 2, 12, 20, 00);
            DateTime medicineEnd2 = new DateTime(2021, 7, 8, 14, 00, 00);
            User user2 = new User("3", "Zarko", "Zarkovic", "zarko", "sifra", "email", "adresa", date2);
            User user3 = new User("4", "Pero", "Peric", "pero", "sifra", "email", "adresa", date3);
            Medicine medicine2 = new Medicine(1, "Paracetamol");
            Medicine medicine3 = new Medicine(2, "Brufen");
            MedicalRecord medicalRecord = new MedicalRecord();
            Prescription prescription1 = new Prescription(3, medicineStart1, medicineEnd1, "opis",100,medicine2);
            Prescription prescription2 = new Prescription(6, medicineStart2, medicineEnd2, "opis", 100, medicine3);
            Anamnesis anamnesis1 = new Anamnesis(1,"Licna","ime anamneze", "opis anamneze");
            Anamnesis anamnesis2 = new Anamnesis(2, "Anamneza", "naziv", "opis ");

            prescription1.Medicine = medicine2;
            prescription2.Medicine = medicine3;

            List<Prescription> prescriptions = new List<Prescription>();
            prescriptions.Add(prescription2);
            prescriptions.Add(prescription1);

            List<Anamnesis> anamneses = new List<Anamnesis>();
            anamneses.Add(anamnesis1);
            anamneses.Add(anamnesis2);

            List<String> alergies = new List<String>();
            alergies.Add("Alergija na polen");
            alergies.Add("Alergija na dlake");

            medicalRecord.Prescription = prescriptions;
            medicalRecord.Anamnesis = anamneses;
            medicalRecord.Alergies = alergies;
            

            Patient patient1 = new Patient(user2);
            patient1.MedicalRecord = medicalRecord;

            patientController.Save(patient1);
            */





            timeDataGrid.Visibility = Visibility.Collapsed;
            patient = patientController.GetByJmbg("3");
            
            List<Appointment> apps = new List<Appointment>();

            doctors = doctorController.GetAll();
            doctorsDataGrid.ItemsSource = doctors;

            //obavjestenja
            List<Prescription> prescriptions = patient.MedicalRecord.Prescription;
            foreach(Prescription p in prescriptions)
            {
                DateTime time = p.StartDate;
                DateTime timeMinusOne = time.AddHours(-1) ;
                
                for (int i = 0; i < p.Interval; i++)
                {
                    
                    if(DateTime.Now.TimeOfDay > timeMinusOne.TimeOfDay && DateTime.Now.TimeOfDay < time.TimeOfDay)
                    {
                        MessageBox.Show(time.TimeOfDay.ToString(), "obavjestenje");
                    }


                    
                    time = time.AddHours(24 / p.Interval);
                    timeMinusOne = time.AddHours(-1);
                }
            }


        }
        private Appointment CreateAppointmentFromData()
        {
            //int id = Int32.Parse(idTextBox.Text);
            DateTime pickedDate = new_appointment_date.SelectedDate.Value;
            int hours = Int32.Parse(startTimeTextBox.Text.Split(':')[0]);
            int minutes = Int32.Parse(startTimeTextBox.Text.Split(':')[1]);
            DateTime appointmentDateTime = new DateTime(pickedDate.Year, pickedDate.Month, pickedDate.Day, hours, minutes, 00);
            double duration = Convert.ToDouble(durationTextBox.Text);
            return new Appointment(appointmentController.GenerateNewId() , AppointmentType.examination, appointmentDateTime, duration, patient, doctor, roomController.GetById(1));
        }



        private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                doctor = (Doctor)doctorsDataGrid.SelectedItems[0];
                Appointment newAppointment = CreateAppointmentFromData();
                List<Appointment> doctorsAppointments = appointmentController.GetAppointmentsForDoctor(doctor.User.Jmbg);
                bool error = false;


                foreach (Appointment app in doctorsAppointments)
                {
                    DateTime newAppEnd = newAppointment.StartTime.AddMinutes(newAppointment.DurationInMinutes);
                    DateTime newAppStart = newAppointment.StartTime;
                    DateTime doctorsAppEndTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                    DateTime doctorAppStartTime = app.StartTime;
                    if ((newAppStart.Ticks <= doctorsAppEndTime.Ticks && newAppStart.Ticks >= doctorAppStartTime.Ticks) ||
                        (newAppEnd.Ticks >= doctorAppStartTime.Ticks && newAppEnd.Ticks <= doctorsAppEndTime.Ticks))
                    {
                        error = true;
                    }
                }

                if (error == true)
                {
                    if (doctorRadioButton.IsChecked == true)
                    {
                        MessageBox.Show("Doktor je zauzet, izaberi drugi termin", "greska");
                        timeDataGrid.Visibility = Visibility.Visible;
                        timeDataGrid.ItemsSource = doctorsAppointments;

                    }
                    else
                    {
                        MessageBox.Show("Termin je zauzet, izaberi drugog doktora", "greska");


                        timeDataGrid.ItemsSource = doctorsAppointments;

                    }
                }
                else if (appointmentController.AppointmentValidationWithoutOverlaping(newAppointment))
                {
                    MessageBox.Show("Provjeri ulazne podatke", "greska");

                }
                else
                {
                    appointmentController.Save(newAppointment);
                }

            }
            catch
            {
                MessageBox.Show("Unesi podatke u sva polja","greska");
            }
        }
        

     

        private void timeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            timeDataGrid.Visibility = Visibility.Collapsed;
        }

        private void showAppointments_Click(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientAppointments();
            new_window.Show();
            App.Current.MainWindow.Hide();

        }

         
        



    }
}
