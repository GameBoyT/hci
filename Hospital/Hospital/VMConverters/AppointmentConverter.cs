using Hospital.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital.VMConverters
{
    public class AppointmentConverter
    {
        #region Polja
        private Injector injector;

        private PatientConverter patientConverter;

        private EmployeeConverter employeeConverter;

        public Injector Injector
        {
            get { return injector; }
            set
            {
                injector = value;
            }
        }

        public PatientConverter PatientConverter
        {
            get { return patientConverter; }
            set
            {
                patientConverter = value;
            }
        }

        public EmployeeConverter EmployeeConverter
        {
            get { return employeeConverter; }
            set
            {
                employeeConverter = value;
            }
        }

        #endregion

        public AppointmentConverter()
        {
            //Injector = new Injector();
            //Injector = injector;
            //injector.AppointmentConverter.PatientConverter = injector.PatientConverter;
            //injector.AppointmentConverter.EmployeeConverter = injector.EmployeeConverter;
        }
        public AppointmentViewModel ConvertModelToViewModel(MedicalAppointment appointment)
        {

            PatientViewModel patient = PatientConverter.ConvertModelToViewModel(Injector.PatientService.GetByJmbg(appointment.PatientJmbg));
            EmployeeViewModel doctor = EmployeeConverter.ConvertModelToViewModel(Injector.EmployeeService.GetByJmbg(appointment.PatientJmbg));

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
                   //Fali room
                   1,
                   "336"
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
