using Model;
using Repository.Interfaces;
using System;
using System.IO;

namespace Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\notifications.json";
            ReadJson();
        }
    }
}
