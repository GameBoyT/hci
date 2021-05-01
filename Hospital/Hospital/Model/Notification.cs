using System;

namespace Model
{
    public class Notification
    {
        public Notification(int id, string notificationText, int receiver)
        {
            Id = id;
            NotificationText = notificationText;
            Receiver = receiver;
        }

        public int Id
        {
            get
            ;
            set
            ;
        }

        public String NotificationText
        {
            get
            ;
            set
            ;
        }

        public int Receiver
        {
            get
            ;
            set
            ;
        }

    }
}