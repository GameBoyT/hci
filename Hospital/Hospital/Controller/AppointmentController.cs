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

        public AppointmentDTO SaveDTO(AppointmentDTO appointment)
        {
            return appointmentService.SaveDTO(appointment);
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

        public List<AppointmentDTO> GetAppointmentsForPatient(String jmbg)
        {
            return appointmentService.GetAppointmentsForPatient(jmbg);
        }

        public bool AppointmentTimeIsInvalid(Appointment appointment)
        {
            return appointmentService.AppointmentTimeIsInvalid(appointment);
        }
        public bool IsTimeInFuture(DateTime appointmentStartTime)
        {
            return appointmentService.IsTimeInFuture(appointmentStartTime);
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
        public List<AppointmentDTO> GetAppointmentsFromPast(String patientJmbg)
        {
            return appointmentService.GetAppointmentsFromPast( patientJmbg);
        }

        public Appointment ConvertToModel(AppointmentDTO appointmentDTO)
        {
            return appointmentService.ConvertToModel(appointmentDTO);
        }

    }
}