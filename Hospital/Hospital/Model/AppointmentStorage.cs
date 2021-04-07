using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital.Model
{
    class AppointmentStorage
    {
        private readonly string appointmentDirectory = Directory.GetCurrentDirectory() + "\\appointment.txt";

        private List<Appointment> appointments;
        public AppointmentStorage()
        {
            using (StreamReader r = new StreamReader(appointmentDirectory))
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


        public void CreateAppointment(Appointment adding_app)
        {
            appointments.Add(adding_app);
            string json = JsonConvert.SerializeObject(appointments);
            File.WriteAllText(appointmentDirectory, json);
        }


        public void DeleteAppointment(Appointment delApp)
        {
            appointments.Remove(delApp);
            string json = JsonConvert.SerializeObject(appointments);
            File.WriteAllText(appointmentDirectory, json);
        }

        public void UpdateAppointment(String id, Appointment updatedAppointment)
        {
            int index = appointments.FindIndex(e => e.Id == id);
            appointments[index] = updatedAppointment;
            string json = JsonConvert.SerializeObject(appointments);
            File.WriteAllText(appointmentDirectory, json);
        }

    }
}
