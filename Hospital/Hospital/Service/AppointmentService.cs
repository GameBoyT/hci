using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepository appointmentRepository = new AppointmentRepository();


        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public Appointment GetById(int id)
        {
            return appointmentRepository.GetById(id);
        }

        public void Save(Appointment appointment)
        {
            appointmentRepository.Save(appointment);
        }

        public void Delete(int id)
        {
            appointmentRepository.Delete(id);
        }

        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }

        public List<Appointment> GetAppointmentsForDoctor(String jmbg)
        {
            return appointmentRepository.GetAppointmentsForDoctor(jmbg);
        }

        public List<Appointment> GetAppointmentsForPatient(String jmbg)
        {
            return appointmentRepository.GetAppointmentsForPatient(jmbg);
        }

        public int GenerateNewId()
        {
            return appointmentRepository.GenerateNewId();
        }

        public bool AppointmentTimeInFuture(Appointment appointment)
        {
            if (DateTime.Now.Ticks > appointment.StartTime.Ticks)
                return true;
            return false;
        }

        public bool AppointmentTimeIsInvalid(Appointment appointment)
        {
            List<Appointment> appointments = appointmentRepository.GetAll();

            if (AppointmentTimeInFuture(appointment)) {
                return true;
            }

            foreach (Appointment app in appointments)
            {
                if (app.Id != appointment.Id)
                {
                    DateTime endTime = app.StartTime.AddMinutes(app.DurationInMinutes);
                    DateTime appointmentEndTime = appointment.StartTime.AddMinutes(appointment.DurationInMinutes);

                    //Provera da li postoji pregled u tom terminu
                    if ((app.StartTime.Ticks < appointment.StartTime.Ticks && endTime.Ticks > appointment.StartTime.Ticks) ||
                            (app.StartTime.Ticks < appointmentEndTime.Ticks && endTime.Ticks > appointmentEndTime.Ticks))
                    {
                        return true;
                    }
                }
                else
                {
                    //Provera da li je vreme updeta u narednih 24h
                    if (DateTime.Now.AddDays(1).Ticks > app.StartTime.Ticks)
                    {
                        return true;
                    }
                    //Provera da li pomera pregled za datum preko 2 dana kasnije
                    if (app.StartTime.AddDays(2).Ticks < appointment.StartTime.Ticks)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}