using Model;
using System.Collections.Generic;

namespace Controller
{
    public class NotificationController
    {
        private Service.NotificationService notificationService = new Service.NotificationService();

        public List<Notification> GetAll()
        {
            return notificationService.GetAll();
        }

        public Notification GetById(int id)
        {
            return notificationService.GetById(id);
        }
        public void Save(Notification notification)
        {
            notificationService.Save(notification);
        }

        public void Delete(int id)
        {
            notificationService.Delete(id);

        }

        public void Update(Notification notification)
        {
            notificationService.Update(notification);
        }

        public int GenerateNewId()
        {
            return notificationService.GenerateNewId();
        }

    }
}