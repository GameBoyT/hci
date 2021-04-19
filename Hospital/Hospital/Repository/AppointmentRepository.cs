using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class AppointmentRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\appointments.json";
        private List<Appointment> appointments = new List<Appointment>();

        public AppointmentRepository()
        {
            ReadJson();
        }

        public void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(appointments);
            File.WriteAllText(fileLocation, json);
        }

        public List<Appointment> GetAll()
        {
            ReadJson();
            return appointments;
        }

        public Appointment GetById(int id)
        {
            ReadJson();
            return appointments.Find(obj => obj.Id == id);
        }

        public int GenerateNewId()
        {
            ReadJson();

            try
            {
                int maxId = appointments.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Save(Appointment appointment)
        {
            ReadJson();

            appointments.Add(appointment);
            WriteToJson();
        }

        public void Delete(int id)
        {
            ReadJson();

            int index = appointments.FindIndex(obj => obj.Id == id);
            appointments.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Appointment appointment)
        {
            ReadJson();
            int index = appointments.FindIndex(obj => obj.Id == appointment.Id);
            appointments[index] = appointment;
            WriteToJson();
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            ReadJson();
            return appointments.FindAll(appointment => appointment.Doctor.User.Jmbg == jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            ReadJson();
            return appointments.FindAll(appointment => appointment.Patient.User.Jmbg == jmbg);
        }

    }
}