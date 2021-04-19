using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class PatientRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\patients.json";
        private List<Patient> patients = new List<Patient>();

        public PatientRepository()
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
                    patients = JsonConvert.DeserializeObject<List<Patient>>(json);
                }
            }
        }
        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(patients);
            File.WriteAllText(fileLocation, json);
        }

        public List<Patient> GetAll()
        {
            return patients;
        }

        public Patient GetByJmbg(String jmbg)
        {
            return patients.Find(obj => obj.User.Jmbg == jmbg);
        }

        public void Save(Patient patient)
        {
            patients.Add(patient);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            int index = patients.FindIndex(obj => obj.User.Jmbg == jmbg);
            patients.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Patient patient)
        {
            int index = patients.FindIndex(obj => obj.User.Jmbg == patient.User.Jmbg);
            patients[index] = patient;
            WriteToJson();
        }

        public int GenerateNewAnamnesisId()
        {
            int maxId = 1;
            foreach (Patient patient in patients)
            {
                if (patient.MedicalRecord != null && patient.MedicalRecord.Anamnesis != null)
                    maxId = patient.MedicalRecord.Anamnesis.Max(obj => obj.Id);
            }
            return maxId + 1;
        }

    }
}