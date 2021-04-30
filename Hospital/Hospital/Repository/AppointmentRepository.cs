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
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\appointments.json";
        private List<Appointment> _appointments = new List<Appointment>();

        public AppointmentRepository()
        {
            ReadJson();
        }

        public void ReadJson()
        {
            if (!File.Exists(_fileLocation))
            {
                File.Create(_fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(_fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    _appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_appointments, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Appointment> GetAll()
        {
            ReadJson();
            return _appointments;
        }

        public Appointment GetById(int id)
        {
            ReadJson();
            return _appointments.Find(obj => obj.Id == id);
        }

        public int GenerateNewId()
        {
            ReadJson();
            try
            {
                int maxId = _appointments.Max(obj => obj.Id);
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

            _appointments.Add(appointment);
            WriteToJson();
        }

        public void Delete(int id)
        {
            ReadJson();

            int index = _appointments.FindIndex(obj => obj.Id == id);
            _appointments.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Appointment appointment)
        {
            ReadJson();
            int index = _appointments.FindIndex(obj => obj.Id == appointment.Id);
            _appointments[index] = appointment;
            WriteToJson();
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            ReadJson();
            return _appointments.FindAll(appointment => appointment.DoctorJmbg == jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            ReadJson();
            return _appointments.FindAll(appointment => appointment.PatientJmbg == jmbg);
        }

    }
}