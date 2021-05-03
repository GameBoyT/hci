using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class NotificationRepository
    {
        //private String fileLocation;
        private List<Notification> _notifications = new List<Notification>();
        private readonly string _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\notifications.json";


        public NotificationRepository()
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
                    _notifications = JsonConvert.DeserializeObject<List<Notification>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(_notifications, Formatting.Indented);
            File.WriteAllText(_fileLocation, json);
        }

        public List<Notification> GetAll()
        {
            return _notifications;
        }

        public Notification GetById(int id)
        {
            return _notifications.Find(obj => obj.Id == id);
        }

        public void Save(Notification notification)
        {
            _notifications.Add(notification);
            WriteToJson();
        }

        public void Delete(int id)
        {
            int index = _notifications.FindIndex(obj => obj.Id == id);
            _notifications.RemoveAt(index);
            WriteToJson();
        }

        public void Update(Notification notification)
        {
            int index = _notifications.FindIndex(obj => obj.Id == notification.Id);
            _notifications[index] = notification;
            WriteToJson();
        }

        public int GenerateNewId()
        {
            ReadJson();
            try
            {
                int maxId = _notifications.Max(obj => obj.Id);
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }
        }

    }
