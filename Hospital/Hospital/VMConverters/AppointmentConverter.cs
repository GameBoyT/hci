﻿using Hospital.ViewModels;
using Model;
using Service;
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


        public PatientService PatientService { get; set; }
        public EmployeeService EmployeeService { get; set; }

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

        }
        public AppointmentViewModel ConvertModelToViewModel(MedicalAppointment appointment)
        {

            PatientViewModel patient = PatientConverter.ConvertModelToViewModel(PatientService.GetByJmbg(appointment.PatientJmbg));
            EmployeeViewModel doctor = EmployeeConverter.ConvertModelToViewModel(EmployeeService.GetByJmbg(appointment.DoctorJmbg));

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
