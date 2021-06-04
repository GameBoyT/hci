using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class AppointmentRepository : GenericRepository<MedicalAppointment>, IAppointmentRepository
    {
        public AppointmentRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\appointments.json";
            ReadJson();
        }

        public List<MedicalAppointment> GetAppointmentsForDoctor(String jmbg)
        {
            ReadJson();
            return _objects.FindAll(appointment => appointment.DoctorJmbg == jmbg);
        }

        public List<MedicalAppointment> GetAppointmentsForPatient(String jmbg)
        {
            ReadJson();
            return _objects.FindAll(appointment => appointment.PatientJmbg == jmbg);
        }

        public List<MedicalAppointment> GetAppointmentsForRoom(int roomId)
        {
            ReadJson();
            return _objects.FindAll(appointment => appointment.RoomId == roomId);
        }
    }
}