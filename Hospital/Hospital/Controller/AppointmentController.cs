using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {

        private Service.AppointmentService appointmentService;



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

        

    }
}