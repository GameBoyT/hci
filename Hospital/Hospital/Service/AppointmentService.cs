using DTO;
using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepository appointmentRepository = new AppointmentRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private PatientRepository patientRepository = new PatientRepository();
        private RoomRepository roomRepository = new RoomRepository();


        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public List<AppointmentDTO> GetAllDTO()
        {
            List<Appointment> appointments = appointmentRepository.GetAll();
            return ConvertListToDTO(appointments);
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

        public int GenerateNewId()
        {
            return appointmentRepository.GenerateNewId();
        }

        public bool AppointmentTimeInFuture(Appointment appointment)
        {
            if (appointment.StartTime.Ticks > DateTime.Now.Ticks)
                return true;
            return false;
        }


        public bool AppointmentIsTaken(Appointment appointment, string doctorId)
        {
            List<Appointment> appointments = appointmentRepository.GetAppointmentsForDoctor(doctorId);

            foreach (Appointment app in appointments)
            {
                if (app.Id != appointment.Id)
                {
                    DateTime endTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                    DateTime appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    //Provera da li postoji pregled u tom terminu
                    if ((appointment.StartTime.Ticks >= app.StartTime.Ticks && appointment.StartTime.Ticks <= endTime.Ticks) ||
                        appointmentEndTime.Ticks >= app.StartTime.Ticks && appointmentEndTime.Ticks <= endTime.Ticks)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public bool AppointmentValidationWithoutOverlaping(Appointment appointment)
        {
            List<Appointment> appointments = appointmentRepository.GetAll();

            if (!AppointmentTimeInFuture(appointment))
            {
                return true;
            }

            foreach (Appointment app in appointments)
            {
                if (app.Id != appointment.Id)
                {
                    DateTime endTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                    DateTime appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);


                }
                else
                {
                    //Provera da li je vreme updeta u narednih 24h
                    if (DateTime.Now.AddDays(1).Ticks > app.StartTime.Ticks)
                    {
                        return true;
                    }
                    //Provera da li pomera pregled za datum preko 2 dana kasnije
                    if (app.StartTime.AddDays(2).Ticks < appointment.StartTime.Ticks)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AppointmentTimeIsInvalid(Appointment appointment)
        {
            List<Appointment> appointments = appointmentRepository.GetAll();

            if (!AppointmentTimeInFuture(appointment))
            {
                return true;
            }

            foreach (Appointment app in appointments)
            {
                if (app.Id != appointment.Id)
                {
                    DateTime endTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                    DateTime appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    //Provera da li postoji pregled u tom terminu
                    if ((appointment.StartTime.Ticks >= app.StartTime.Ticks && appointment.StartTime.Ticks <= endTime.Ticks) ||
                        appointmentEndTime.Ticks >= app.StartTime.Ticks && appointmentEndTime.Ticks <= endTime.Ticks)
                    {
                        return true;
                    }
                }
                else
                {
                    //Provera da li je vreme updeta u narednih 24h
                    if (DateTime.Now.AddDays(1).Ticks > app.StartTime.Ticks)
                    {
                        return true;
                    }
                    //Provera da li pomera pregled za datum preko 2 dana kasnije
                    if (app.StartTime.AddDays(2).Ticks < appointment.StartTime.Ticks || app.StartTime.AddDays(-2).Ticks > appointment.StartTime.Ticks)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public AppointmentDTO convertToDTO (Appointment appointment)
        {
            Employee doctor = employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            Patient patient = patientRepository.GetByJmbg(appointment.PatientJmbg);
            Room room = roomRepository.GetById(appointment.RoomId);

            AppointmentDTO appointmentDTO = new AppointmentDTO
                (
                    appointment.Id,
                    appointment.AppointmentType,
                    appointment.StartTime,
                    appointment.DurationInMinutes,
                    doctor.User.Jmbg,
                    doctor.User.FirstName,
                    doctor.User.LastName,
                    doctor.Specialization,
                    patient.User.Jmbg,
                    patient.User.FirstName,
                    patient.User.LastName,
                    room.Id,
                    room.Name
                );
            return appointmentDTO;
        }

        public List<AppointmentDTO> ConvertListToDTO(List<Appointment> appointments)
        {
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            foreach (Appointment appointment in appointments)
            {
                appointmentDTOs.Add(convertToDTO(appointment));
            }
            return appointmentDTOs;
        }

    }
}