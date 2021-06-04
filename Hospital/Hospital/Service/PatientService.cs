using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service
{
    public class PatientService
    {

        public Repository.PatientRepository patientRepository = new Repository.PatientRepository();
        public Repository.EmployeeRepository employeeRepository = new Repository.EmployeeRepository();
        public Repository.AppointmentRepository appointmentRepository = new Repository.AppointmentRepository();
        NotificationService notificationService = new NotificationService();


        public List<Patient> GetAll()
        {
            return patientRepository.GetAll();
        }

        public Patient GetByJmbg(String jmbg)
        {
            return patientRepository.GetByJmbg(jmbg);
        }

        public void Save(Patient patient)
        {
            List<Patient> patients = patientRepository.GetAll();
            foreach (Patient patient1 in patients)
            {
                if (patient1.User.Jmbg == patient.User.Jmbg)
                {
                    MessageBox.Show("Isti jmbg", "greska");
                    return;
                }

                if (patient1.User.Username == patient.User.Username)
                {
                    MessageBox.Show("Isti username", "greska");
                    return;
                }

            }
            patientRepository.Save(patient);
        }

        public void Delete(String jmbg)
        {
            patientRepository.Delete(jmbg);
        }

        public void Update(Patient patient)
        {
            patientRepository.Update(patient);
        }

        public Anamnesis AddAnamnesis(string jmbg, string name, string type, string description)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            int id = patientRepository.GenerateNewAnamnesisId();
            Anamnesis anamnesis = new Anamnesis(id, type, name, description);
            patient.MedicalRecord.Anamnesis.Add(anamnesis);
            Update(patient);
            return anamnesis;
        }

        internal Referral AddReferral(string patientJmbg, string doctorJmbg, string description)
        {
            Referral referral = new Referral(doctorJmbg, description);
            Patient patient = GetByJmbg(patientJmbg);
            patient.MedicalRecord.Referrals.Add(referral);
            Update(patient);
            return referral;
        }

        public Anamnesis UpdateAnamnesisDescription(string jmbg, int id, string description)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            Anamnesis anamnesis = patient.MedicalRecord.Anamnesis.Find(obj => obj.Id == id);
            anamnesis.Description = description;
            Update(patient);
            return anamnesis;
        }

        public void AddHospitalStay(string jmbg, StaticEquipment bed, DateTime startDateTime, DateTime endDateTime)
        {
            HospitalStay hospitalStay = new HospitalStay(bed, startDateTime, endDateTime);
            Patient patient = GetByJmbg(jmbg);
            patient.MedicalRecord.HospitalStay = hospitalStay;
            Update(patient);
        }

        public void ExtendHospitalStay(string jmbg, DateTime extendedDate)
        {
            Patient patient = GetByJmbg(jmbg);
            patient.MedicalRecord.HospitalStay.EndDateTime = extendedDate;
            Update(patient);
        }

        public void CheckForMedicineNotification(Patient patient)
        {
            List<Prescription> prescriptions = patient.MedicalRecord.Prescription;
            foreach (Prescription p in prescriptions)
            {
                DateTime time = p.StartDate;
                DateTime timeMinusOne = time.AddHours(-1);


                for (int i = 0; i < p.Interval; i++)
                {

                    if (DateTime.Now.TimeOfDay > timeMinusOne.TimeOfDay && DateTime.Now.TimeOfDay < time.TimeOfDay)
                    {
                        String message = p.Medicine.Name + "," + time.TimeOfDay.ToString();
                        //Promjeni ovo 1
                        Notification notification = new Notification(0, message, "1", patient.User.Jmbg);
                        patient.Notifications.Add(notification);
                        patientRepository.Update(patient);
                    }



                    time = time.AddHours(24 / p.Interval);
                    timeMinusOne = time.AddHours(-1);
                }
            }
        }

        private string IsPatientBlocked(Patient patient)
        {
            if (patient.CancelationDates.Count > 5)
            {
                patient.Blocked = true;
                patientRepository.Update(patient);

                return "Pacijent je blokiran";
            }
            return "Uspjeno obrisan termin";
        }
        private int NumberOfCancelationsInPast(List<DateTime> cancelations)
        {
            int counter = 0;
            foreach (DateTime cancelTime in cancelations)
            {
                if (cancelTime < DateTime.Now.AddDays(-10))
                {
                    counter++;
                }
                else break;
            }
            return counter;
        }


        public string AntiTrollCheck(int appointmentId)
        {
            MedicalAppointment appointment = appointmentRepository.GetById(appointmentId);
            Patient patient = patientRepository.GetByJmbg(appointment.PatientJmbg);
            List<DateTime> updatedCancelations = patient.CancelationDates;

            updatedCancelations.RemoveRange(0, NumberOfCancelationsInPast(updatedCancelations));
            updatedCancelations.Add(DateTime.Now);
            patient.CancelationDates = updatedCancelations;

            patientRepository.Update(patient);
            return IsPatientBlocked(patient);

        }

        public Prescription AddPrescription(string jmbg, Medicine medicine, int quantity, string description)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            Prescription prescription = new Prescription(medicine, quantity, description);
            patient.MedicalRecord.Prescription.Add(prescription);
            Update(patient);
            return prescription;
        }

        public List<Anamnesis> GetAllAnamnesisForPatient(string patientJmbg)
        {
            Patient patient = patientRepository.GetByJmbg(patientJmbg);
            return patient.MedicalRecord.Anamnesis;
        }

        public List<PrescriptionDTO> GetAllPresctiptionsForPatient(string patientJmbg)
        {
            Patient patient = patientRepository.GetByJmbg(patientJmbg);
            return PrescriptionsToDTOList(patient.MedicalRecord.Prescription);
        }

        public PrescriptionDTO PrescriptionToDTO(Prescription prescription)
        {
            PrescriptionDTO prescriptionDTO = new PrescriptionDTO(prescription.Medicine.Name,
                                                                    prescription.Interval,
                                                                    prescription.StartDate,
                                                                    prescription.EndDate);
            return prescriptionDTO;
        }

        public List<PrescriptionDTO> PrescriptionsToDTOList(List<Prescription> prescriptions)
        {
            List<PrescriptionDTO> prescriptionDTOs = new List<PrescriptionDTO>();
            foreach (Prescription prescription in prescriptions)
            {
                prescriptionDTOs.Add(PrescriptionToDTO(prescription));
            }
            return prescriptionDTOs;
        }



        public void AddReminder(string jmbg, Reminder reminder)
        {
            if (isReminderValid(reminder))
            {
                Patient patient = patientRepository.GetByJmbg(jmbg);
                reminder.Id = GenerateId(jmbg);

                patient.Reminders.Add(reminder);
                patientRepository.Update(patient);
            }
        }

        public List<Reminder> GetAllReminders(string jmbg)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            return patient.Reminders;
        }

        private int GenerateId(string jmbg)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            List<Reminder> reminders = patient.Reminders;
            try
            {
                int max_id = reminders[0].Id;
                foreach (Reminder reminder in reminders)
                {
                    if (max_id < reminder.Id)
                        max_id = reminder.Id;
                }
                return max_id + 1;
            }
            catch
            {
                return 1;
            }
        }


        public void DeleteReminder(string jmbg, int id)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            List<Reminder> reminders = patient.Reminders;
            reminders.Remove(reminders.Find(obj => obj.Id == id));
            patientRepository.Update(patient);
        }

        public void UpdateReminder(string jmbg, Reminder reminder)
        {
            if (isReminderValid(reminder))
            {
                Patient patient = patientRepository.GetByJmbg(jmbg);
                List<Reminder> reminders = patient.Reminders;

                reminders[reminders.IndexOf(reminders.Find(obj => obj.Id == reminder.Id))] = reminder;
                patient.Reminders = reminders;
                patientRepository.Update(patient);
            }
        }

        private bool isReminderValid(Reminder reminder)
        {
            if (IsReminderInPast(reminder) || IsReminderNotifyTimePast(reminder))
            {
                MessageBox.Show("Reminder is in the past", "Error");
                return false;
            }

            if (reminder.NotifyTime > reminder.Time)
            {
                MessageBox.Show("Notify time should be before reminder!", "error");
                return false;
            }
            return true;

        }

        private bool IsReminderInPast(Reminder reminder)
        {
            if (reminder.Time < DateTime.Now)
                return true;
            else return false;
        }

        private bool IsReminderNotifyTimePast(Reminder reminder)
        {
            if (reminder.NotifyTime < DateTime.Now)
                return true;
            else return false;

        }

        private void RemoveOldReminders(string jmbg)
        {
            Patient patient = patientRepository.GetByJmbg(jmbg);
            List<Reminder> reminders = patient.Reminders;
            reminders.RemoveAll(obj => obj.Time < DateTime.Now);
            patient.Reminders = reminders;
            patientRepository.Update(patient);


        }

        public void CheckForReminder(string jmbg)
        {
            RemoveOldReminders(jmbg);

            Patient patient = patientRepository.GetByJmbg(jmbg);
            List<Reminder> reminders = patient.Reminders;
            foreach (Reminder reminder in reminders)
            {
                if (IsReminderNotifyTimePast(reminder))
                {
                    string message = reminder.Title.ToUpper() + " , " + reminder.Time.ToString("dd:MM:yyyy HH:mm");
                    Notification notification = new Notification(0, message, jmbg, jmbg);
                    patient.Notifications.Add(notification);
                    patientRepository.Update(patient);
                }

            }


        }

    }
}