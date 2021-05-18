using DTO;
using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {
        private Service.AppointmentService appointmentService = new Service.AppointmentService();
        private Service.NotificationService notificationService = new Service.NotificationService();

        public List<AppointmentDTO> GetAll()
        {
            return appointmentService.GetAll();
        }

        public AppointmentDTO GetById(int id)
        {
            return appointmentService.GetById(id);
        }

        public AppointmentDTO Save(AppointmentDTO appointment)
        {
            AppointmentDTO createdAppointment = appointmentService.Save(appointment);
            createdAppointment.ModifiedByJmbg = appointment.ModifiedByJmbg;
            notificationService.NotifyAppointmentCreation(createdAppointment);
            return createdAppointment;
        }

        public void Update(AppointmentDTO appointment)
        {
            AppointmentDTO updatetAppointment = appointmentService.Update(appointment);
            updatetAppointment.ModifiedByJmbg = appointment.ModifiedByJmbg;
            notificationService.NotifyAppointmentUpdate(updatetAppointment);
        }

        public AppointmentDTO Delete(int id, string modifiedByJmbg)
        {
            AppointmentDTO appointment = appointmentService.Delete(id);
            appointment.ModifiedByJmbg = modifiedByJmbg;
            notificationService.NotifyAppointmentDeletion(appointment);
            return appointment;
        }

        public List<AppointmentDTO> GetAppointmentsForDoctor(String jmbg)
        {
            return appointmentService.GetAppointmentsForDoctor(jmbg);
        }

        public List<AppointmentDTO> GetAppointmentsForPatient(String jmbg)
        {
            return appointmentService.GetAppointmentsForPatient(jmbg);
        }

        public bool IsDoctorAvailable(AppointmentDTO appointment)
        {
            return appointmentService.IsDoctorAvailable(appointment);
        }

        public bool IsPatientAvailable(AppointmentDTO appointment)
        {
            return appointmentService.IsPatientAvailable(appointment);
        }

        public bool IsRoomAvailable(AppointmentDTO appointment)
        {
            return appointmentService.IsRoomAvailable(appointment);
        }

        public bool AppointmentTimeIsInvalid(AppointmentDTO appointment)
        {
            return appointmentService.AppointmentTimeIsInvalid(appointment);
        }
        public bool IsTimeInFuture(DateTime appointmentStartTime)
        {
            return appointmentService.IsTimeInFuture(appointmentStartTime);
        }
        public bool AppointmentIsTaken(AppointmentDTO appointment, string doctorId)
        {
            return appointmentService.AppointmentIsTaken(appointment, doctorId);
        }

        public int GenerateNewId()
        {
            return appointmentService.GenerateNewId();
        }
        public bool AppointmentValidationWithoutOverlaping(AppointmentDTO appointment)
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