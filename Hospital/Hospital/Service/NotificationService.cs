using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class NotificationService
    {
        private Repository.NotificationRepository notificationRepository = new Repository.NotificationRepository();



        public List<Notification> GetAll()
        {
            return notificationRepository.GetAll();
        }

        public Notification GetById(int id)
        {
            return notificationRepository.GetById(id);
        }
        public void Save(Notification notification)
        {
            notificationRepository.Save(notification);
        }

        public void Delete(int id)
        {
            notificationRepository.Delete(id);

        }

        public void Update(Notification notification)
        {
            notificationRepository.Update(notification);
        }


    }
}