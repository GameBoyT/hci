using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class DoctorStorage
    {
        private readonly string fileLocation = Directory.GetCurrentDirectory() + "\\doctors.json";
        private List<Doctor> doctors;

        public DoctorStorage()
        {
            doctors = new List<Doctor>();

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    doctors = JsonConvert.DeserializeObject<List<Doctor>>(json);
                }
            }
        }

        public List<Doctor> GetAll()
        {
            return doctors;

        }
        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(doctors);
            File.WriteAllText(fileLocation, json);
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