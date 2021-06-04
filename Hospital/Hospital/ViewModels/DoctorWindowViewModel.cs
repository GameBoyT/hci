using Hospital.Commands;
using Hospital.View.Doctor;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class DoctorWindowViewModel : ViewModel
    {
        private DateTime appointmentsDate;

        public Injector Inject { get; set; }

        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public ObservableCollection<AppointmentViewModel> SelectedDateAppointments { get; set; }

        public AppointmentViewModel SelectedAppointment { get; set; }

        public RelayCommand NewExaminationCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }


        private bool CanExecute_DeleteCommand(object obj)
        {
            if (SelectedAppointment != null) return true;
            return false;
        }

        private void Execute_DeleteCommand(object obj)
        {
            Inject.AppointmentService.Delete(SelectedAppointment.Id);
            Appointments.Remove(SelectedAppointment);
            SelectedDateAppointments.Remove(SelectedAppointment);
        }

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
                UpdateSelectedDateAppointmens();
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
            injector.AppointmentConverter.RoomConverter = injector.RoomConverter;
            injector.AppointmentConverter.RoomService = injector.RoomService;


            DeleteCommand = new RelayCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            
            Appointments = new ObservableCollection<AppointmentViewModel>(Inject.AppointmentConverter.ConvertCollectionToViewModel(Inject.AppointmentService.GetAllForLoggedInDoctor()));
            SelectedDateAppointments = new ObservableCollection<AppointmentViewModel>(Appointments.Where(appointment => appointment.StartTime.Date == DateTime.Now.Date));
            AppointmentsDate = DateTime.Now;
        }

        private void UpdateSelectedDateAppointmens()
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
