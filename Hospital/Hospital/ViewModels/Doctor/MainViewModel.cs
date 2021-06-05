using Hospital.Commands;
using Hospital.View.Doctor;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private DateTime appointmentsDate;

        public Injector Inject { get; set; }

        public NavigationService NavService { get; set; }

        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public ObservableCollection<AppointmentViewModel> SelectedDateAppointments { get; set; }

        public AppointmentViewModel SelectedAppointment { get; set; }

        public RelayCommand NewExaminationCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }


        private bool CanExecute_AppointmentCommand(object obj)
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

        private void Execute_UpdateCommand(object obj)
        {
            UpdateExaminationViewModel vm = new UpdateExaminationViewModel(NavService, SelectedAppointment);
            UpdateExaminationView updateExaminationView = new UpdateExaminationView(vm);
            NavService.Navigate(updateExaminationView);
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
                DateChange();
            }
        }

        public MainViewModel()
        {
            Injector injector = new Injector();
            Inject = injector;
            injector.AppointmentConverter.EmployeeConverter = injector.EmployeeConverter;
            injector.AppointmentConverter.PatientConverter = injector.PatientConverter;
            injector.AppointmentConverter.EmployeeService = injector.EmployeeService;
            injector.AppointmentConverter.PatientService = injector.PatientService;
            injector.AppointmentConverter.RoomConverter = injector.RoomConverter;
            injector.AppointmentConverter.RoomService = injector.RoomService;

            DeleteCommand = new RelayCommand(Execute_DeleteCommand, CanExecute_AppointmentCommand);
            UpdateCommand = new RelayCommand(Execute_UpdateCommand);

            Appointments = Inject.AppointmentConverter.ConvertCollectionToViewModel(Inject.AppointmentService.GetAllForLoggedInDoctor());
            SelectedDateAppointments = new ObservableCollection<AppointmentViewModel>();
            AppointmentsDate = DateTime.Now;
        }


        private void DateChange()
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
    }
}
