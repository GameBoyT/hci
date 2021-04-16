using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class DoctorRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\doctors.json";
        private List<Doctor> doctors = new List<Doctor>();

        public DoctorRepository()
        {
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    doctors = JsonConvert.DeserializeObject<List<Doctor>>(json);
                }
            }
        }
        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(doctors);
            File.WriteAllText(fileLocation, json);
        }

        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor GetByJmbg(String jmbg)
        {
            return doctors.Find(doctor => doctor.User.Jmbg == jmbg);
        }

        public void Save(Doctor doctor)
        {
            doctors.Add(doctor);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            throw new NotImplementedException();
        }

        public void Update(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}