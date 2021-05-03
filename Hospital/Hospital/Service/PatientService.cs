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

        public string IsPatientBlocked(Patient patient)
        {
            if (patient.CancelationDates.Count > 5)
            {
                patient.Blocked = true;
                patientRepository.Update(patient);
                return "Pacijent je blokiran";
            }
            return "Uspjeno obrisan termin";
        }


        public string AntiTrollCheck(int appointmentId)
        {
            Appointment appointment = appointmentRepository.GetById(appointmentId);
            Patient patient = patientRepository.GetByJmbg(appointment.PatientJmbg);
            List<DateTime> updatedCancelations = patient.CancelationDates;
            List<DateTime> toRemove = new List<DateTime>();
            int counter = 0;
            //izbacuje sva otkazivanja koja su starija od 10 dana
            foreach (DateTime cancelTime in updatedCancelations)
            {
                if (cancelTime < DateTime.Now.AddDays(-10))
                {
                    counter++;
                }
                else break;
            }
            updatedCancelations.RemoveRange(0, counter);
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
    }
}