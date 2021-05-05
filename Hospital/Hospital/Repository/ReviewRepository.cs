using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class ReviewRepository
    {
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\reviews.json";
        private List<Review> reviews = new List<Review>();

        public ReviewRepository()
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
                    reviews = JsonConvert.DeserializeObject<List<Review>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(reviews, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Review> GetAll()
        {
            ReadJson();
            return reviews;
        }

        public Review Save(Review review)
        {
            ReadJson();

            reviews.Add(review);
            WriteToJson();
            return review;
        }

        

    
       
    }
}