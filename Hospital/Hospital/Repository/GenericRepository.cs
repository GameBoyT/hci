using Model;
using Newtonsoft.Json;
using Repository.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public T Save(T entity)
        {
            ReadJson();
            _objects.Add(entity);
            WriteToJson();
            return entity;
        }

        public T Delete(int id)
        {
            ReadJson();
            T entity = _objects.Find(obj => obj.Id == id);
            _objects.Remove(entity);
            WriteToJson();
            return entity;
        }

        public T Update(T entity)
        {
            ReadJson();
            int index = _objects.FindIndex(obj => obj.Id == entity.Id);
            _objects[index] = entity;
            WriteToJson();
            return entity;
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

            using StreamReader r = new StreamReader(_fileLocation);
            string json = r.ReadToEnd();
            if (json != "")
            {
                _objects = JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        protected void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_objects, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }
    }
}
