using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class PatientStorage
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\patients.json";
        private List<Patient> patients;

        public PatientStorage()
        {
            patients = new List<Patient>();

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    patients = JsonConvert.DeserializeObject<List<Patient>>(json);
                }
            }
        }
        public List<Patient> GetAll()
        {
            return patients;
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(patients);
            File.WriteAllText(fileLocation, json);
        }

        public Patient GetByJmbg(String jmbg)
        {
            return patients.Find(patient => patient.User.Jmbg == jmbg);
        }

        public void Save(Patient patient)
        {
            patients.Add(patient);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            throw new NotImplementedException();
        }

        public void Update(Patient patient)
        {
            throw new NotImplementedException();
        }

    }
}