using Model;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\appointments.json";
            ReadJson();
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            ReadJson();
            return _objects.FindAll(appointment => appointment.DoctorJmbg == jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            ReadJson();
            return _objects.FindAll(appointment => appointment.PatientJmbg == jmbg);
        }

        public List<Appointment> GetAppointmentsForRoom(int roomId)
        {
            ReadJson();
            return _objects.FindAll(appointment => appointment.RoomId == roomId);
        }
    }
}