using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Hospital.Model
{
    class AppointmentStorage
    {
        
        private List<Appointment> appointments;
        public AppointmentStorage()
        {
            string[] lines = System.IO.File.ReadAllLines(@"d:\appointment.txt");
            string[] temp_app;
            this.appointments  = new List<Appointment>();

            foreach (string line in lines)
            {
                temp_app = line.Split(',');
                int i = 0;
                String id = ""; String timeStart = ""; String duration = "";
                foreach (String attr in temp_app)
                {
                    Console.WriteLine("\t" + attr);
                    if (i == 0) { id = attr; }
                    if (i == 1) { timeStart = attr; }
                    if (i == 2) { duration = attr; }
                   
                    i++;
                }
                Appointment appointment = new Appointment(id, timeStart, duration);
                appointments.Add(appointment);
                Debug.WriteLine(appointment.timeStart);
            }
        }
        public List<Appointment> GetAll()
        {
            return appointments;
        }

 


        public void deleteAppointment(Appointment delApp)
        {
            appointments.Remove(delApp);
            string line = "";
            foreach(Appointment appointment in appointments){
                line += appointment.id + "," + appointment.timeStart + "," + appointment.duration;
                line += '\n';
            }
            File.WriteAllText(@"d:\proba.txt",line);
        }

    }
}
