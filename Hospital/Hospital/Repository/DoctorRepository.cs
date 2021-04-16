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
            if (!File.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }

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
            return doctors.Find(obj => obj.User.Jmbg == jmbg);
        }

        public void Save(Doctor doctor)
        {
            doctors.Add(doctor);
            WriteToJson();
        }

        public void Delete(String jmbg)
        {
            int index = doctors.FindIndex(obj => obj.User.Jmbg == jmbg);
            doctors.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Doctor doctor)
        {
            int index = doctors.FindIndex(obj => obj.User.Jmbg == doctor.User.Jmbg);
            doctors[index] = doctor;
            WriteToJson();
        }
    }
}