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


        public List<AppointmentDTO> GetAll()
        {
            List<Appointment> appointments = appointmentRepository.GetAll();
            return ConvertListToDTO(appointments);
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

        public List<AppointmentDTO> GetAppointmentsForDoctor(String jmbg)
        {
            return ConvertListToDTO(appointmentRepository.GetAppointmentsForDoctor(jmbg));
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

        public AppointmentDTO ConvertToDTO (Appointment appointment)
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
                appointmentDTOs.Add(ConvertToDTO(appointment));
            }
            return appointmentDTOs;
        }


        public Appointment ConvertToModel(AppointmentDTO appointmentDTO)
        {
            Appointment appointment = new Appointment
                (
                    appointmentDTO.Id,
                    appointmentDTO.AppointmentType,
                    appointmentDTO.StartTime,
                    appointmentDTO.DurationInMinutes,
                    appointmentDTO.PatientJmbg,
                    appointmentDTO.DoctorJmbg,
                    appointmentDTO.RoomId
                );
            return appointment;
        }

        public List<Appointment> ConvertListToModel(List<AppointmentDTO> appointmentsDTOs)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (AppointmentDTO appointmentDTO in appointmentsDTOs)
            {
                appointments.Add(ConvertToModel(appointmentDTO));
            }
            return appointments;
        }
    }
}