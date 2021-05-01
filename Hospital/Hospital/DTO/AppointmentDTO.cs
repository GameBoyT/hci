using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DTO
{
    public class AppointmentDTO
    {
        public Appointment Appointment { get; set; }
        public String DoctorJMBG { get; set; }
        public String DoctorFirstName { get; set; }
        public String DoctorLastName { get; set; }
      //  public String DoctorSpecialization { get; set; }
        public String PatientJMBG { get; set; }
        public String PatientFirstName { get; set; }
        public String PatientLastName { get; set; }
        public String RoomName { get; set; }

        public AppointmentDTO(Appointment appointment, String doctorJmbg, string doctorFirstName, string doctorLastName)
        {
            Appointment = appointment;
            DoctorJMBG = doctorJmbg;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            

        }

        public AppointmentDTO (Appointment appointment, String patientJmbg, String patientFirstName, String patientLastName, String roomName)
        {
            Appointment = appointment;
            PatientJMBG = patientJmbg;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            RoomName = roomName;

        }


    }
}
