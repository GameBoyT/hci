using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepository appointmentRepository = new AppointmentRepository();


        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public Appointment GetById(int id)
        {
            return appointmentRepository.GetById(id);
        }

        public void Save(Appointment appointment)
        {
            appointmentRepository.Save(appointment);
        }

        public void Delete(int id)
        {
            appointmentRepository.Delete(id);
        }

        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            return appointmentRepository.GetAppointmentsForDoctor(jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            return appointmentRepository.GetAppointmentsForPatient(jmbg);
        }



    }
}