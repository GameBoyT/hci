using DTO;
using Model;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository = new AppointmentRepository();
        private readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();
        private readonly IPatientRepository _patientRepository = new PatientRepository();
        private readonly IRoomRepository _roomRepository = new RoomRepository();

        public AppointmentService()
        {

        }


        public List<AppointmentDTO> GetAll()
        {
            List<MedicalAppointment> appointments = _appointmentRepository.GetAll();
            return ConvertListToDTO(appointments);
        }

        public AppointmentDTO GetById(int id)
        {
            return ConvertToDTO(_appointmentRepository.GetById(id));
        }

        public AppointmentDTO Save(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.Id = GenerateNewId();
            MedicalAppointment appointment = ConvertToModel(appointmentDTO);
            _appointmentRepository.Save(appointment);
            AddAppointmentToParticipants(appointment);
            return ConvertToDTO(appointment);
        }
        public void SaveRenovation(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.Id = GenerateNewId();
            MedicalAppointment appointment = ConvertToModel(appointmentDTO);
            _appointmentRepository.Save(appointment);
        }

        public void AddAppointmentToParticipants(MedicalAppointment appointment)
        {
            AddAppointmentToRoom(appointment);
            AddAppointmentToDoctor(appointment);
            AddAppointmentToPatient(appointment);
        }

        public void AddAppointmentToDoctor(MedicalAppointment appointment)
        {
            Employee doctor = _employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            doctor.Appointments.Add(appointment);
            _employeeRepository.Update(doctor);
        }

        public void AddAppointmentToPatient(MedicalAppointment appointment)
        {
            Patient patient = _patientRepository.GetByJmbg(appointment.PatientJmbg);
            patient.Appointments.Add(appointment);
            _patientRepository.Update(patient);
        }

        public void AddAppointmentToRoom(MedicalAppointment appointment)
        {
            Room room = _roomRepository.GetById(appointment.RoomId);
            room.Appointments.Add(appointment);
            _roomRepository.Update(room);
        }

        public AppointmentDTO Update(AppointmentDTO appointmentDTO)
        {
            MedicalAppointment appointment = ConvertToModel(appointmentDTO);
            AppointmentDTO updatedAppointment = ConvertToDTO(_appointmentRepository.Update(appointment));
            UpdateAppointmentParticipants(appointment);
            return updatedAppointment;
        }

        public void UpdateAppointmentParticipants (MedicalAppointment appointment)
        {
            UpdateAppointmentForDoctor(appointment);
            UpdateAppointmentForPatient(appointment);
            UpdateAppointmentForRoom(appointment);
        }

        public void UpdateAppointmentForDoctor(MedicalAppointment appointment)
        {
            Employee doctor = _employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            int index = doctor.Appointments.FindIndex(obj => obj.Id == appointment.Id);
            doctor.Appointments[index] = appointment;
            _employeeRepository.Update(doctor);
        }

        public void UpdateAppointmentForPatient(MedicalAppointment appointment)
        {
            Patient patient = _patientRepository.GetByJmbg(appointment.PatientJmbg);
            int index = patient.Appointments.FindIndex(obj => obj.Id == appointment.Id);
            patient.Appointments[index] = appointment;
            _patientRepository.Update(patient);
        }

        public void UpdateAppointmentForRoom(MedicalAppointment appointment)
        {
            Room room = _roomRepository.GetById(appointment.RoomId);
            int index = room.Appointments.FindIndex(obj => obj.Id == appointment.Id);
            room.Appointments[index] = appointment;
            _roomRepository.Update(room);
        }
        
        public AppointmentDTO Delete(int id)
        {
            return ConvertToDTO(_appointmentRepository.Delete(id));
        }

        public List<AppointmentDTO> GetAppointmentsForDoctor(string jmbg)
        {
            return ConvertListToDTO(_appointmentRepository.GetAppointmentsForDoctor(jmbg));
        }

        public List<AppointmentDTO> GetAppointmentsForPatient(string jmbg)
        {
            return ConvertListToDTO(_appointmentRepository.GetAppointmentsForPatient(jmbg));
        }

        public List<AppointmentDTO> GetAppointmentsForRoom(int id)
        {
            return ConvertListToDTO(_appointmentRepository.GetAppointmentsForRoom(id));
        }

        public int GenerateNewId()
        {
            return _appointmentRepository.GenerateNewId();
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
            DateTime appointmentEndTime;
            foreach (AppointmentDTO appointment in appointments)
            {
                if (appointmentToCheck.Id != appointment.Id)
                {
                    appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    if (AreAppointmentsOverlapping(appointmentToCheck.StartTime, appointmentToCheckEndTime, appointment.StartTime, appointmentEndTime))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        public List<DateTime> DoctorFreeTimeSlotsInNextHour(string jmbg)
        {
            List<DateTime> appointments = new List<DateTime>();
            Employee doctor = _employeeRepository.GetByJmbg(jmbg);
            DateTime startTime = DateTime.Now;
            DateTime endTime;
            DateTime appointmentEndTime;
            for (int i = 0; i < 4; i++)
            {
                startTime.AddMinutes(15 * i);
                endTime = startTime.AddMinutes(15);

                foreach (Appointment appointment in doctor.Appointments)
                {
                    appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    if (!AreAppointmentsOverlapping(startTime, endTime, appointment.StartTime, appointmentEndTime))
                        appointments.Add(startTime);
                }
            }

            return appointments;
        }

        private bool AreAppointmentsOverlapping(DateTime firstAppointmentStartTime, DateTime firstAppointmentEndTime, DateTime secondAppointmentStartTime, DateTime secondAppointmentEndTime)
        {
            if (IsDateTimeBetween(firstAppointmentStartTime, secondAppointmentStartTime, secondAppointmentStartTime) ||
                    IsDateTimeBetween(firstAppointmentEndTime, secondAppointmentStartTime, secondAppointmentStartTime))
            {
                return true;
            }
            return false;
        }


        // Stari kod, za brisanje kad se prebaci sve na novo
        public bool AppointmentIsTaken(AppointmentDTO appointment, string doctorId)
        {
            List<MedicalAppointment> appointments = _appointmentRepository.GetAppointmentsForDoctor(doctorId);

            foreach (MedicalAppointment app in appointments)
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
            List<MedicalAppointment> appointments = _appointmentRepository.GetAll();

            if (!IsTimeInFuture(appointment.StartTime))
            {
                return true;
            }

            foreach (MedicalAppointment app in appointments)
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
            List<MedicalAppointment> appointments = _appointmentRepository.GetAll();

            if (!IsTimeInFuture(appointment.StartTime))
            {
                return true;
            }

            foreach (MedicalAppointment app in appointments)
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
        // Sve iznad do komentara je za brisanje

       

        public List<AppointmentDTO> GetAppointmentsFromPast(String patientJmbg)
        {
            List<MedicalAppointment> appointments = _appointmentRepository.GetAppointmentsForPatient(patientJmbg);
            List<MedicalAppointment> appointmentsInPast = new List<MedicalAppointment>();
            foreach (MedicalAppointment appointment in appointments)
            {
                if (appointment.StartTime < DateTime.Now)
                    appointmentsInPast.Add(appointment);
            }
            return ConvertListToDTO(appointmentsInPast);
        }

        public List<AppointmentDTO> GetOperationsForPatient(String patientJmbg)
        {
            List<MedicalAppointment> allAppointments = _appointmentRepository.GetAppointmentsForPatient(patientJmbg);
            List<MedicalAppointment> operations = new List<MedicalAppointment>();
            foreach (MedicalAppointment appointment in allAppointments)
            {
                if (appointment.MedicalAppointmentType == MedicalAppointmentType.operation) { operations.Add(appointment); }
            }

            return ConvertListToDTO(operations);
        }

        private AppointmentDTO ConvertToDTO(MedicalAppointment appointment)
        {
            Room room = _roomRepository.GetById(appointment.RoomId);
            AppointmentDTO appointmentDTO;
            if (appointment.DoctorJmbg == null)
            {
                appointmentDTO = new AppointmentDTO
                    (
                        appointment.Id,
                        appointment.StartTime,
                        appointment.DurationInMinutes,
                        room.Id
                    );

                return appointmentDTO;
            }
            Employee doctor = _employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            Patient patient = _patientRepository.GetByJmbg(appointment.PatientJmbg);
             appointmentDTO = new AppointmentDTO
                (
                    appointment.Id,
                    appointment.MedicalAppointmentType,
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

        private List<AppointmentDTO> ConvertListToDTO(List<MedicalAppointment> appointments)
        {
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            foreach (MedicalAppointment appointment in appointments)
            {
                appointmentDTOs.Add(ConvertToDTO(appointment));
            }

            return appointmentDTOs;
        }

        private MedicalAppointment ConvertToModel(AppointmentDTO appointmentDTO)
        {
            MedicalAppointment appointment = new MedicalAppointment
                (
                    appointmentDTO.Id,
                    appointmentDTO.MedicalAppointmentType,
                    appointmentDTO.StartTime,
                    appointmentDTO.DurationInMinutes,
                    appointmentDTO.PatientJmbg,
                    appointmentDTO.DoctorJmbg,
                    appointmentDTO.RoomId
                );

            return appointment;
        }

        private List<MedicalAppointment> ConvertListToModel(List<AppointmentDTO> appointmentsDTOs)
        {
            List<MedicalAppointment> appointments = new List<MedicalAppointment>();
            foreach (AppointmentDTO appointmentDTO in appointmentsDTOs)
            {
                appointments.Add(ConvertToModel(appointmentDTO));
            }

            return appointments;
        }
    }
}