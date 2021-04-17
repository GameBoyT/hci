using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class DirectorRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\director.json";
        private List<Director> directors = new List<Director>();

        public DirectorRepository()
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
                    directors = JsonConvert.DeserializeObject<List<Director>>(json);
                }
            }
        }

        public List<Director> GetAll()
        {
            return directors;
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(directors);
            File.WriteAllText(fileLocation, json);
        }

        public Director GetByJmbg(string jmbg)
        {
            return directors.Find(obj => obj.User.Jmbg == jmbg);
        }

      

        public void Save(Director director)
        {
            directors.Add(director);
            WriteToJson();
        }

        public void Delete(string jmbg)
        {
            int index = directors.FindIndex(obj => obj.User.Jmbg == jmbg);
            directors.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Director director)
        {
            int index = directors.FindIndex(obj => obj.User.Jmbg == director.User.Jmbg);
            directors[index] = director;
            WriteToJson();
        }

    }
}