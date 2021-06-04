using Hospital.ViewModels;
using Model;
using Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.VMConverters
{
    public class AppointmentConverter
    {
        #region Polja
        public Injector Injector { get; set; }

        public PatientService PatientService { get; set; }

        public EmployeeService EmployeeService { get; set; }

        public RoomService RoomService { get; set; }

        public PatientConverter PatientConverter { get; set; }

        public EmployeeConverter EmployeeConverter { get; set; }

        public RoomConverter RoomConverter { get; set; }

        #endregion

        public AppointmentConverter()
        {

        }
        public AppointmentViewModel ConvertModelToViewModel(MedicalAppointment appointment)
        {

            PatientViewModel patient = PatientConverter.ConvertModelToViewModel(PatientService.GetByJmbg(appointment.PatientJmbg));
            EmployeeViewModel doctor = EmployeeConverter.ConvertModelToViewModel(EmployeeService.GetByJmbg(appointment.DoctorJmbg));
            RoomViewModel room = RoomConverter.ConvertModelToViewModel(RoomService.GetById(appointment.RoomId));

            AppointmentViewModel appointmentViewModel = new AppointmentViewModel
               (
                   appointment.Id,
                   appointment.MedicalAppointmentType,
                   appointment.StartTime,
                   appointment.DurationInMinutes,
                   doctor.Jmbg,
                   doctor.FirstName,
                   doctor.LastName,
                   doctor.Specialization,
                   patient.Jmbg,
                   patient.FirstName,
                   patient.LastName,
                   room.Id,
                   room.Name
               );

            appointmentViewModel._Appointment = appointment;

            return appointmentViewModel;
        }

        public ObservableCollection<AppointmentViewModel> ConvertCollectionToViewModel(List<MedicalAppointment> appointments)
        {
            ObservableCollection<AppointmentViewModel> vmAppointments = new ObservableCollection<AppointmentViewModel>();
            AppointmentViewModel appointmentViewModel;
            foreach (MedicalAppointment appointment in appointments)
            {
                appointmentViewModel = ConvertModelToViewModel(appointment);
                vmAppointments.Add(appointmentViewModel);
            }
            return vmAppointments;
        }

    }
}
