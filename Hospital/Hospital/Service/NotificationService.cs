using DTO;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class NotificationService
    {
        private Repository.NotificationRepository notificationRepository = new Repository.NotificationRepository();
        private Repository.EmployeeRepository employeeRepository = new Repository.EmployeeRepository();
        private Repository.PatientRepository patientRepository = new Repository.PatientRepository();

        public List<Notification> GetAll()
        {
            return notificationRepository.GetAll();
        }

        public Notification GetById(int id)
        {
            return notificationRepository.GetById(id);
        }

        public void Save(Notification notification)
        {
            notificationRepository.Save(notification);
        }

        public void NotifyAppointmentCreation(AppointmentDTO appointment)
        {
            string text = "Zakazan je pregled za pacijenta " + appointment.PatientFirstName + " " + appointment.PatientLastName +
                " koji ima zakazan pregled kod lekara " + appointment.DoctorLastName + " trajanje pregleda je " + appointment.DurationInMinutes + " minuta, a datum je " + appointment.StartTime.ToString();


            if (appointment.ModifiedByJmbg != appointment.DoctorJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification doctorNotification = new Notification(id, text, appointment.ModifiedByJmbg, appointment.DoctorJmbg);
                AddNotificationToEmployee(doctorNotification);
            }
            if (appointment.ModifiedByJmbg != appointment.PatientJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification patientNotification = new Notification(id, text, appointment.ModifiedByJmbg, appointment.PatientJmbg);
                AddNotificationToPatient(patientNotification);
            }
        }


        public void NotifyAppointmentUpdate(AppointmentDTO appointment, string senderJmbg)
        {
            string text = "Izmenjen je pregled za pacijenta " + appointment.PatientFirstName + " " + appointment.PatientLastName +
                " koji ima zakazan pregled kod lekara " + appointment.DoctorLastName + " trajanje pregleda je " + appointment.DurationInMinutes + " minuta, a datum je " + appointment.StartTime.ToString();

            if (senderJmbg != appointment.DoctorJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification doctorNotification = new Notification(id, text, senderJmbg, appointment.DoctorJmbg);
                AddNotificationToEmployee(doctorNotification);
            }
            if (senderJmbg != appointment.PatientJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification patientNotification = new Notification(id, text, senderJmbg, appointment.PatientJmbg);
                AddNotificationToPatient(patientNotification);
            }
        }

        public void NotifyAppointmentUpdate(AppointmentDTO appointment)
        {
            string text = "Izmenjen je pregled za pacijenta " + appointment.PatientFirstName + " " + appointment.PatientLastName +
                " koji ima zakazan pregled kod lekara " + appointment.DoctorLastName + " trajanje pregleda je " + appointment.DurationInMinutes + " minuta, a datum je " + appointment.StartTime.ToString();

            if (appointment.ModifiedByJmbg != appointment.DoctorJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification doctorNotification = new Notification(id, text, appointment.ModifiedByJmbg, appointment.DoctorJmbg);
                AddNotificationToEmployee(doctorNotification);
            }
            if (appointment.ModifiedByJmbg != appointment.PatientJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification patientNotification = new Notification(id, text, appointment.ModifiedByJmbg, appointment.PatientJmbg);
                AddNotificationToPatient(patientNotification);
            }
        }

        public void NotifyAppointmentDeletion(AppointmentDTO appointment, string senderJmbg)
        {
            string text = "Obrisan pregled za pacijenta " + appointment.PatientFirstName + " " + appointment.PatientLastName +
                " koji ima zakazan pregled kod lekara " + appointment.DoctorLastName + " trajanje pregleda je " + appointment.DurationInMinutes + " minuta, a datum je " + appointment.StartTime.ToString();

            if (senderJmbg != appointment.DoctorJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification doctorNotification = new Notification(id, text, senderJmbg, appointment.DoctorJmbg);
                AddNotificationToEmployee(doctorNotification);
            }
            if (senderJmbg != appointment.PatientJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification patientNotification = new Notification(id, text, senderJmbg, appointment.PatientJmbg);
                AddNotificationToPatient(patientNotification);
            }
        }

        public void NotifyAppointmentDeletion(AppointmentDTO appointment)
        {
            string text = "Obrisan pregled za pacijenta " + appointment.PatientFirstName + " " + appointment.PatientLastName +
                " koji ima zakazan pregled kod lekara " + appointment.DoctorLastName + " trajanje pregleda je " + appointment.DurationInMinutes + " minuta, a datum je " + appointment.StartTime.ToString();

            if (appointment.ModifiedByJmbg != appointment.DoctorJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification doctorNotification = new Notification(id, text, appointment.ModifiedByJmbg, appointment.DoctorJmbg);
                AddNotificationToEmployee(doctorNotification);
            }
            if (appointment.ModifiedByJmbg != appointment.PatientJmbg)
            {
                int id = notificationRepository.GenerateNewId();
                Notification patientNotification = new Notification(id, text, appointment.ModifiedByJmbg, appointment.PatientJmbg);
                AddNotificationToPatient(patientNotification);
            }
        }

        private void AddNotificationToEmployee(Notification notification)
        {
            Employee employee = employeeRepository.GetByJmbg(notification.ReceiverJmbg);
            employee.Notifications.Add(notification);
            employeeRepository.Update(employee);
            Save(notification);
        }

        public void AddNotificationToPatient(Notification notification)
        {
            Patient patient = patientRepository.GetByJmbg(notification.ReceiverJmbg);
            patient.Notifications.Add(notification);
            patientRepository.Update(patient);
            Save(notification);
        }

        public void Delete(int id)
        {
            notificationRepository.Delete(id);
        }

        public void Update(Notification notification)
        {
            notificationRepository.Update(notification);
        }

        public int GenerateNewId()
        {
            return notificationRepository.GenerateNewId();
        }
    }
}