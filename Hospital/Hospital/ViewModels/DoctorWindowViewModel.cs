using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Controls;
using Hospital.View.Doctor;
using Hospital.Commands;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class DoctorWindowViewModel : ViewModel
    {
        private DateTime appointmentsDate;

        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public ObservableCollection<AppointmentViewModel> SelectedDateAppointments { get; set; }

        public RelayCommand NewExaminationCommand { get; set; }

        public Injector Inject { get; set; }

        public DateTime AppointmentsDate
        {
            get
            {
                return appointmentsDate;
            }
            set
            {
                appointmentsDate = value;
                OnPropertyChanged();
                DateChanged();
            }
        }

        public DoctorWindowViewModel()
        {
            Injector injector = new Injector();
            Inject = injector;
            injector.AppointmentConverter.EmployeeConverter = injector.EmployeeConverter;
            injector.AppointmentConverter.PatientConverter = injector.PatientConverter;
            injector.AppointmentConverter.EmployeeService = injector.EmployeeService;
            injector.AppointmentConverter.PatientService = injector.PatientService;

            //this.navService = navService;

            //NewExaminationCommand = new RelayCommand(Executed_NewExaminationCommand);


            Appointments = new ObservableCollection<AppointmentViewModel>(Inject.AppointmentConverter.ConvertCollectionToViewModel(Inject.AppointmentService.GetAllForLoggedInDoctor()));
            SelectedDateAppointments = new ObservableCollection<AppointmentViewModel>(Appointments.Where(appointment => appointment.StartTime.Date == DateTime.Now.Date));
            AppointmentsDate = DateTime.Now;
        }

        private void DateChanged()
        {
            try
            {
                SelectedDateAppointments.Clear();
                foreach (AppointmentViewModel appointment in Appointments)
                {
                    if (appointment.StartTime.Date == AppointmentsDate.Date)
                    {
                        SelectedDateAppointments.Add(appointment);
                    }
                }
            }
            catch { }
        }

        //public void Executed_NewExaminationCommand(object obj)
        //{
        //    DoctorExaminationView doctorExamination = new DoctorExaminationView();
        //    this.NavService.Navigate(doctorExamination);

        //    //this.NavService.Navigate(
        //    //    new Uri("DoctorExaminationView.xaml", UriKind.Relative));
        //}
    }
}
