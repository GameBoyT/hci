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

        public AppointmentDTO GetById(int id)
        {
            return ConvertToDTO(appointmentRepository.GetById(id));
        }

        public AppointmentDTO Save(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.Id = GenerateNewId();
            Appointment appointment = ConvertToModel(appointmentDTO);
            appointmentRepository.Save(appointment);
            AddAppointmentToParticipants(appointment);
            return appointmentDTO;
        }

        public void AddAppointmentToParticipants(Appointment appointment)
        {
            AddAppointmentToDoctor(appointment);
            AddAppointmentToPatient(appointment);
            AddAppointmentToRoom(appointment);
        }

        public void AddAppointmentToDoctor(Appointment appointment)
        {
            Employee doctor = employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            doctor.Appointments.Add(appointment);
            employeeRepository.Update(doctor);
        }

        public void AddAppointmentToPatient(Appointment appointment)
        {
            Patient patient = patientRepository.GetByJmbg(appointment.PatientJmbg);
            patient.Appointments.Add(appointment);
            patientRepository.Update(patient);
        }

        public void AddAppointmentToRoom(Appointment appointment)
        {
            Room room = roomRepository.GetById(appointment.RoomId);
            room.Appointments.Add(appointment);
            roomRepository.Update(room);
        }

        public void Update(AppointmentDTO appointmentDTO)
        {
            Appointment appointment = ConvertToModel(appointmentDTO);
            appointmentRepository.Update(appointment);
            UpdateAppointmentParticipants(appointment);
        }

        public void UpdateAppointmentParticipants (Appointment appointment)
        {
            UpdateAppointmentForDoctor(appointment);
            UpdateAppointmentForPatient(appointment);
            UpdateAppointmentForRoom(appointment);
        }

        public void UpdateAppointmentForDoctor(Appointment appointment)
        {
            Employee doctor = employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            int index = doctor.Appointments.FindIndex(obj => obj.Id == appointment.Id);
            doctor.Appointments[index] = appointment;
            employeeRepository.Update(doctor);
        }

        public void UpdateAppointmentForPatient(Appointment appointment)
        {
            Patient patient = patientRepository.GetByJmbg(appointment.PatientJmbg);
            int index = patient.Appointments.FindIndex(obj => obj.Id == appointment.Id);
            patient.Appointments[index] = appointment;
            patientRepository.Update(patient);
        }

        public void UpdateAppointmentForRoom(Appointment appointment)
        {
            Room room = roomRepository.GetById(appointment.RoomId);
            int index = room.Appointments.FindIndex(obj => obj.Id == appointment.Id);
            room.Appointments[index] = appointment;
            roomRepository.Update(room);
        }
        
        public void Delete(int id)
        {
            appointmentRepository.Delete(id);
        }

        public List<AppointmentDTO> GetAppointmentsForDoctor(String jmbg)
        {
            return ConvertListToDTO(appointmentRepository.GetAppointmentsForDoctor(jmbg));
        }

        public List<AppointmentDTO> GetAppointmentsForPatient(String jmbg)
        {
            return ConvertListToDTO(appointmentRepository.GetAppointmentsForPatient(jmbg));
        }

        public List<AppointmentDTO> GetAppointmentsForRoom(int id)
        {
            return ConvertListToDTO(appointmentRepository.GetAppointmentsForRoom(id));
        }

        public int GenerateNewId()
        {
            return appointmentRepository.GenerateNewId();
        }

        public bool IsDoctorAvailable(AppointmentDTO appointment)
        {
            List<AppointmentDTO> appointments = GetAppointmentsForDoctor(appointment.DoctorJmbg);
            return IsTimeSlotFree(appointment, appointments);
        }

        public bool IsPatientAvailable(AppointmentDTO appointment)
        {
            List<AppointmentDTO> appointments = GetAppointmentsForPatient(appointment.PatientJmbg);
            return IsTimeSlotFree(appointment, appointments);
        }

        public bool IsRoomAvailable(AppointmentDTO appointment)
        {
            List<AppointmentDTO> appointments = GetAppointmentsForRoom(appointment.RoomId);
            return IsTimeSlotFree(appointment, appointments);
        }

        public bool IsTimeInFuture(DateTime appointmentStartTime)
        {
            if (appointmentStartTime.Ticks > DateTime.Now.Ticks)
                return true;
            return false;
        }

        public bool IsDateTimeBetween(DateTime dateTimeToCheck, DateTime startTime, DateTime endTime)
        {
            return dateTimeToCheck.Ticks >= startTime.Ticks && dateTimeToCheck.Ticks < endTime.Ticks;
        }

        public bool IsTimeSlotFree(AppointmentDTO appointmentToCheck, List<AppointmentDTO> appointments)
        {
            DateTime appointmentToCheckEndTime = appointmentToCheck.StartTime.AddMinutes(appointmentToCheck.DurationInMinutes);
            foreach (AppointmentDTO appointment in appointments)
            {
                if (appointmentToCheck.Id != appointment.Id)
                {
                    DateTime appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    //Provera da li postoji pregled u tom terminu
                    if (IsDateTimeBetween(appointmentToCheck.StartTime, appointment.StartTime, appointmentEndTime) || 
                            IsDateTimeBetween (appointmentToCheckEndTime, appointment.StartTime, appointmentEndTime))
                    {
                        return false;
                    }
                }

            }
            return true;
        }




        // Stari kod, za brisanje kad se prebaci sve na novo
        public bool AppointmentIsTaken(AppointmentDTO appointment, string doctorId)
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

        public bool AppointmentValidationWithoutOverlaping(AppointmentDTO appointment)
        {
            List<Appointment> appointments = appointmentRepository.GetAll();

            if (!IsTimeInFuture(appointment.StartTime))
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

        public bool AppointmentTimeIsInvalid(AppointmentDTO appointment)
        {
            List<Appointment> appointments = appointmentRepository.GetAll();

            if (!IsTimeInFuture(appointment.StartTime))
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

        public AppointmentDTO ConvertToDTO(Appointment appointment)
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

        public List<AppointmentDTO>  GetAppointmentsFromPast(String patientJmbg)
        {
            List<Appointment>appointments = appointmentRepository.GetAppointmentsForPatient(patientJmbg);
            List<Appointment> appointmentsInPast = new List<Appointment>();
            foreach(Appointment appointment in appointments)
            {
                if (appointment.StartTime < DateTime.Now)
                    appointmentsInPast.Add(appointment);
            }
            return ConvertListToDTO(appointmentsInPast);


        }
      
    }
}