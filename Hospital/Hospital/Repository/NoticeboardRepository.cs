using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repository
{
    public class NoticeboardRepository
    {

        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\noticeboards.json";
        private List<Noticeboard> _noticeboards = new List<Noticeboard>();

        public NoticeboardRepository()
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
                    _noticeboards = JsonConvert.DeserializeObject<List<Noticeboard>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_noticeboards, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public int GenerateNewId()
        {
            ReadJson();
            try
            {
                int maxId = _noticeboards.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Noticeboard> GetAll()
        {
            return _noticeboards;
        }

        public Noticeboard GetById(int id)
        {
            return _noticeboards.Find(obj => obj.Id == id);
        }

        public void Save(Model.Noticeboard noticeboard)
        {
            _noticeboards.Add(noticeboard);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = _noticeboards.FindIndex(obj => obj.Id == id);
            _noticeboards.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Model.Noticeboard noticeboard)
        {
            int index = _noticeboards.FindIndex(obj => obj.Id == noticeboard.Id);
            _noticeboards[index] = noticeboard;
            WriteToJson();
        }
    }
}
