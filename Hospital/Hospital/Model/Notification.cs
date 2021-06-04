using System;

namespace Model
{
    public class Notification : Entity
    {
        public Notification(int id, string notificationText, string senderJmbg, string receiverJmbg)
        {
            Id = id;
            NotificationText = notificationText;
            SenderJmbg = senderJmbg;
            ReceiverJmbg = receiverJmbg;
            DateTimeOfSending = DateTime.Now;
        }

        public string NotificationText { get; set; }

        public string SenderJmbg { get; set; }

        public string ReceiverJmbg { get; set; }

        public DateTime DateTimeOfSending { get; set; }

    }
}