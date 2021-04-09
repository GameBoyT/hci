using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital.Model
{
    class AppointmentStorage
    {
        private readonly string fileLocation = Directory.GetCurrentDirectory() + "\\appointment.txt";

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

        public void CreateAppointment(Appointment appointmentToAdd)
        {
            appointments.Add(appointmentToAdd);
            WriteAppointmentsToJson();
        }


        public void DeleteAppointment(Appointment appointmentToDelete)
        {
            appointments.Remove(appointmentToDelete);
            WriteAppointmentsToJson();
        }

        public void UpdateAppointment(Appointment updatedAppointment)
        {
            int index = appointments.FindIndex(appointment => appointment.Id == updatedAppointment.Id);
            appointments[index] = updatedAppointment;
            WriteAppointmentsToJson();
        }

    }
}
