using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class PatientRepository : GenericUserRepository<Patient>
    {
        public PatientRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\patients.json";
            ReadJson();
        }

        public Patient GetByJmbg(String jmbg)
        {
            ReadJson();
            return _objects.Find(obj => obj.User.Jmbg == jmbg);
        }

        public Patient Delete(String jmbg)
        {
            ReadJson();
            Patient patient = _objects.Find(obj => obj.User.Jmbg == jmbg);
            _objects.Remove(patient);
            WriteToJson();
            return patient;
        }

        public Patient Update(Patient obj)
        {
            ReadJson();
            int index = _objects.FindIndex(obj => obj.User.Jmbg == obj.User.Jmbg);
            _objects[index] = obj;
            WriteToJson();
            return obj;
        }

        public int GenerateNewAnamnesisId()
        {
            ReadJson();
            int maxId = 1;
            foreach (Patient patient in _objects)
            {
                if (patient.MedicalRecord != null && patient.MedicalRecord.Anamnesis != null && patient.MedicalRecord.Anamnesis.Count != 0)
                    maxId = patient.MedicalRecord.Anamnesis.Max(obj => obj.Id);
            }
            return maxId + 1;
        }

    }
}