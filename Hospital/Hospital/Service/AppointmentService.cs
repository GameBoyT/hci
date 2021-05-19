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
        private IAppointmentRepository _appointmentRepository = new AppointmentRepository();
        private IEmployeeRepository _employeeRepository = new EmployeeRepository();
        private IPatientRepository _patientRepository = new PatientRepository();
        private IRoomRepository _roomRepository = new RoomRepository();

        public AppointmentService()
        {

        }


        public List<MedicalAppointmentDTO> GetAll()
        {
            List<MedicalAppointment> appointments = _appointmentRepository.GetAll();
            return ConvertListToDTO(appointments);
        }

        public MedicalAppointmentDTO GetById(int id)
        {
            return ConvertToDTO(_appointmentRepository.GetById(id));
        }

        public MedicalAppointmentDTO Save(MedicalAppointmentDTO appointmentDTO)
        {
            appointmentDTO.Id = GenerateNewId();
            MedicalAppointment appointment = ConvertToModel(appointmentDTO);
            _appointmentRepository.Save(appointment);
            AddAppointmentToParticipants(appointment);
            return ConvertToDTO(appointment);
        }

        public void AddAppointmentToParticipants(MedicalAppointment appointment)
        {
            AddAppointmentToDoctor(appointment);
            AddAppointmentToPatient(appointment);
            AddAppointmentToRoom(appointment);
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

        public MedicalAppointmentDTO Update(MedicalAppointmentDTO appointmentDTO)
        {
            MedicalAppointment appointment = ConvertToModel(appointmentDTO);
            MedicalAppointmentDTO updatedAppointment = ConvertToDTO(_appointmentRepository.Update(appointment));
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
        
        public MedicalAppointmentDTO Delete(int id)
        {
            return ConvertToDTO(_appointmentRepository.Delete(id));
        }

        public List<MedicalAppointmentDTO> GetAppointmentsForDoctor(string jmbg)
        {
            return ConvertListToDTO(_appointmentRepository.GetAppointmentsForDoctor(jmbg));
        }

        public List<MedicalAppointmentDTO> GetAppointmentsForPatient(string jmbg)
        {
            return ConvertListToDTO(_appointmentRepository.GetAppointmentsForPatient(jmbg));
        }

        public List<MedicalAppointmentDTO> GetAppointmentsForRoom(int id)
        {
            return ConvertListToDTO(_appointmentRepository.GetAppointmentsForRoom(id));
        }

        public int GenerateNewId()
        {
            return _appointmentRepository.GenerateNewId();
        }

        public bool IsDoctorAvailable(MedicalAppointmentDTO appointment)
        {
            List<MedicalAppointmentDTO> appointments = GetAppointmentsForDoctor(appointment.DoctorJmbg);
            return IsTimeSlotFree(appointment, appointments);
        }

        public bool IsPatientAvailable(MedicalAppointmentDTO appointment)
        {
            List<MedicalAppointmentDTO> appointments = GetAppointmentsForPatient(appointment.PatientJmbg);
            return IsTimeSlotFree(appointment, appointments);
        }

        public bool IsRoomAvailable(MedicalAppointmentDTO appointment)
        {
            List<MedicalAppointmentDTO> appointments = GetAppointmentsForRoom(appointment.RoomId);
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

        public bool IsTimeSlotFree(MedicalAppointmentDTO appointmentToCheck, List<MedicalAppointmentDTO> appointments)
        {
            DateTime appointmentToCheckEndTime = appointmentToCheck.StartTime.AddMinutes(appointmentToCheck.DurationInMinutes);
            foreach (MedicalAppointmentDTO appointment in appointments)
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
        public bool AppointmentIsTaken(MedicalAppointmentDTO appointment, string doctorId)
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

        public bool AppointmentValidationWithoutOverlaping(MedicalAppointmentDTO appointment)
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

        public bool AppointmentTimeIsInvalid(MedicalAppointmentDTO appointment)
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

        public List<MedicalAppointmentDTO> GetAppointmentsFromPast(String patientJmbg)
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

        private MedicalAppointmentDTO ConvertToDTO(MedicalAppointment appointment)
        {
            Employee doctor = _employeeRepository.GetByJmbg(appointment.DoctorJmbg);
            Patient patient = _patientRepository.GetByJmbg(appointment.PatientJmbg);
            Room room = _roomRepository.GetById(appointment.RoomId);

            MedicalAppointmentDTO appointmentDTO = new MedicalAppointmentDTO
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

        private List<MedicalAppointmentDTO> ConvertListToDTO(List<MedicalAppointment> appointments)
        {
            List<MedicalAppointmentDTO> appointmentDTOs = new List<MedicalAppointmentDTO>();
            foreach (MedicalAppointment appointment in appointments)
            {
                appointmentDTOs.Add(ConvertToDTO(appointment));
            }
            return appointmentDTOs;
        }

        private MedicalAppointment ConvertToModel(MedicalAppointmentDTO appointmentDTO)
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

        private List<MedicalAppointment> ConvertListToModel(List<MedicalAppointmentDTO> appointmentsDTOs)
        {
            List<MedicalAppointment> appointments = new List<MedicalAppointment>();
            foreach (MedicalAppointmentDTO appointmentDTO in appointmentsDTOs)
            {
                appointments.Add(ConvertToModel(appointmentDTO));
            }
            return appointments;
        }
    }
}