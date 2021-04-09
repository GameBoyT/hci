using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class AppointmentStorage
    {
        private readonly string fileLocation = Directory.GetCurrentDirectory() + "\\appointment.json";

        private List<Appointment> appointments;
        public AppointmentStorage()
        {
            appointments = new List<Appointment>();

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
                }
            }
        }

        public List<Appointment> GetAll()
        {
            return appointments;
        }

        public void WriteAppointmentsToJson()
        {
            string json = JsonConvert.SerializeObject(appointments);
            File.WriteAllText(fileLocation, json);
        }

        public Appointment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Appointment appointment)
        {
            appointments.Add(appointment);
            WriteAppointmentsToJson();
        }

        public void Delete(int id)
        {
            //appointments.Remove(appointmentToDelete);
            WriteAppointmentsToJson();
        }

        public void Update(Appointment appointment)
        {
            int index = appointments.FindIndex(app => app.Id == appointment.Id);
            appointments[index] = appointment;
            WriteAppointmentsToJson();
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            throw new NotImplementedException();
        }

    }
}