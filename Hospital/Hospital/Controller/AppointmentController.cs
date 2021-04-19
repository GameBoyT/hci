using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {

        private Service.AppointmentService appointmentService = new Service.AppointmentService();

        public List<Appointment> GetAll()
        {
            return appointmentService.GetAll();
        }

        public Appointment GetById(int id)
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

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
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
        public bool AppointmentTimeInFuture(Appointment appointment)
        {
            return appointmentService.AppointmentTimeInFuture(appointment);
        }
        public bool AppointmentIsTaken(Appointment appointment)
        {
            return appointmentService.AppointmentIsTaken(appointment);
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