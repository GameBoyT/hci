using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class AppointmentStorage
    {
        private readonly string fileLocation = Directory.GetCurrentDirectory() + "\\appointments.json";

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

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(appointments);
            File.WriteAllText(fileLocation, json);
        }

        public Appointment GetById(int id)
        {
            return appointments.Find(app => app.Id == id);
        }

        public bool UniqueId(int id)
        {
            if (appointments.FindIndex(app => app.Id == id) == -1)
                return true;
            else
                return false;
        }

        public void Save(Appointment appointment)
        {
            appointments.Add(appointment);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = appointments.FindIndex(app => app.Id == id);
            appointments.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Appointment appointment)
        {
            int index = appointments.FindIndex(app => app.Id == appointment.Id);
            appointments[index] = appointment;
            WriteToJson();
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            return appointments.FindAll(appointment => appointment.Doctor.User.Jmbg == jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            throw new NotImplementedException();
        }

    }
}