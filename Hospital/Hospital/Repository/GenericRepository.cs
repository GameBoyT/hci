using Model;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected string _fileLocation;
        protected List<T> _objects = new List<T>();

        public List<T> GetAll()
        {
            ReadJson();
            return _objects;
        }

        public T GetById(int id)
        {
            ReadJson();
            return _objects.Find(obj => obj.Id == id);
        }

        public T Save(T obj)
        {
            ReadJson();
            _objects.Add(obj);
            WriteToJson();
            return obj;
        }

        public T Delete(int id)
        {
            ReadJson();
            T obj = _objects.Find(obj => obj.Id == id);
            _objects.Remove(obj);
            WriteToJson();
            return obj;
        }

        public T Update(T obj)
        {
            ReadJson();
            int index = _objects.FindIndex(obj => obj.Id == obj.Id);
            _objects[index] = obj;
            WriteToJson();
            return obj;
        }

        public int GenerateNewId()
        {
            ReadJson();
            try
            {
                int maxId = _objects.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        protected void ReadJson()
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
                    _objects = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
        }

        protected void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_objects, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }
    }
}
