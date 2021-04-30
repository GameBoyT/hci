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
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\patients.json";
        private List<Patient> _patients = new List<Patient>();

        public PatientRepository()
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
                    _patients = JsonConvert.DeserializeObject<List<Patient>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_patients, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Patient> GetAll()
        {
            ReadJson();
            return _patients;
        }

        public Patient GetByJmbg(String jmbg)
        {
            ReadJson();
            return _patients.Find(obj => obj.User.Jmbg == jmbg);
        }

        public void Save(Patient patient)
        {
            ReadJson();
            _patients.Add(patient);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            ReadJson();
            int index = _patients.FindIndex(obj => obj.User.Jmbg == jmbg);
            _patients.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Patient patient)
        {
            ReadJson();
            int index = _patients.FindIndex(obj => obj.User.Jmbg == patient.User.Jmbg);
            _patients[index] = patient;
            WriteToJson();
        }

        public int GenerateNewAnamnesisId()
        {
            ReadJson();
            int maxId = 1;
            foreach (Patient patient in _patients)
            {
                if (patient.MedicalRecord != null && patient.MedicalRecord.Anamnesis != null && patient.MedicalRecord.Anamnesis.Count != 0)
                    maxId = patient.MedicalRecord.Anamnesis.Max(obj => obj.Id);
            }
            return maxId + 1;
        }

    }
}