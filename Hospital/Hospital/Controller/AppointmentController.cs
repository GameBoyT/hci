using DTO;
using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {
        PatientController patientController = new PatientController();


        private Service.AppointmentService appointmentService = new Service.AppointmentService();

        public List<AppointmentDTO> GetAll()
        {
            return appointmentService.GetAll();
        }

        public AppointmentDTO GetById(int id)
        {
            return appointmentService.GetById(id);
        }

        public void Save(Appointment appointment)
        {
            appointmentService.Save(appointment);
        }

        public void Delete(int id)
        {
            appointmentService.Delete(id);
        }

        public void Update(Appointment appointment)
        {
            appointmentService.Update(appointment);
        }

        public List<AppointmentDTO> GetAppointmentsForDoctor(String jmbg)
        {
            return appointmentService.GetAppointmentsForDoctor(jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            return appointmentService.GetAppointmentsForPatient(jmbg);
        }

        public bool AppointmentTimeIsInvalid(Appointment appointment)
        {
            return appointmentService.AppointmentTimeIsInvalid(appointment);
        }
        public bool AppointmentTimeInFuture(DateTime appointmentStartTime)
        {
            return appointmentService.AppointmentTimeInFuture(appointmentStartTime);
        }
        public bool AppointmentIsTaken(Appointment appointment, string doctorId)
        {
            return appointmentService.AppointmentIsTaken(appointment, doctorId);
        }

        public int GenerateNewId()
        {
            return appointmentService.GenerateNewId();
        }
        public bool AppointmentValidationWithoutOverlaping(Appointment appointment)
        {
            return appointmentService.AppointmentValidationWithoutOverlaping(appointment);
        }

    }
}