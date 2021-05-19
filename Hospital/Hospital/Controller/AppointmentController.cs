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

        public List<MedicalAppointmentDTO> GetAll()
        {
            return appointmentService.GetAll();
        }

        public MedicalAppointmentDTO GetById(int id)
        {
            return appointmentService.GetById(id);
        }

        public MedicalAppointmentDTO Save(MedicalAppointmentDTO appointment)
        {
            MedicalAppointmentDTO createdAppointment = appointmentService.Save(appointment);
            createdAppointment.ModifiedByJmbg = appointment.ModifiedByJmbg;
            notificationService.NotifyAppointmentCreation(createdAppointment);
            return createdAppointment;
        }

        public void Update(MedicalAppointmentDTO appointment)
        {
            MedicalAppointmentDTO updatetAppointment = appointmentService.Update(appointment);
            updatetAppointment.ModifiedByJmbg = appointment.ModifiedByJmbg;
            notificationService.NotifyAppointmentUpdate(updatetAppointment);
        }

        public MedicalAppointmentDTO Delete(int id, string modifiedByJmbg)
        {
            MedicalAppointmentDTO appointment = appointmentService.Delete(id);
            appointment.ModifiedByJmbg = modifiedByJmbg;
            notificationService.NotifyAppointmentDeletion(appointment);
            return appointment;
        }

        public List<MedicalAppointmentDTO> GetAppointmentsForDoctor(String jmbg)
        {
            return appointmentService.GetAppointmentsForDoctor(jmbg);
        }

        public List<MedicalAppointmentDTO> GetAppointmentsForPatient(String jmbg)
        {
            return appointmentService.GetAppointmentsForPatient(jmbg);
        }

        public bool IsDoctorAvailable(MedicalAppointmentDTO appointment)
        {
            return appointmentService.IsDoctorAvailable(appointment);
        }

        public bool IsPatientAvailable(MedicalAppointmentDTO appointment)
        {
            return appointmentService.IsPatientAvailable(appointment);
        }

        public bool IsRoomAvailable(MedicalAppointmentDTO appointment)
        {
            return appointmentService.IsRoomAvailable(appointment);
        }

        public bool AppointmentTimeIsInvalid(MedicalAppointmentDTO appointment)
        {
            return appointmentService.AppointmentTimeIsInvalid(appointment);
        }
        public bool IsTimeInFuture(DateTime appointmentStartTime)
        {
            return appointmentService.IsTimeInFuture(appointmentStartTime);
        }
        public bool AppointmentIsTaken(MedicalAppointmentDTO appointment, string doctorId)
        {
            return appointmentService.AppointmentIsTaken(appointment, doctorId);
        }

        public bool AppointmentValidationWithoutOverlaping(MedicalAppointmentDTO appointment)
        {
            return appointmentService.AppointmentValidationWithoutOverlaping(appointment);
        }
        public List<MedicalAppointmentDTO> GetAppointmentsFromPast(String patientJmbg)
        {
            return appointmentService.GetAppointmentsFromPast( patientJmbg);
        }

        public List<MedicalAppointmentDTO>GetOperationsForPatient(String patientJmbg)
        {
            return appointmentService.GetOperationsForPatient(patientJmbg);
        }
    }
}