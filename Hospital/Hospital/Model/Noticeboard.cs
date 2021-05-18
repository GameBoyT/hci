using System;

namespace Model
{
    public class Noticeboard : Entity
    {
        public Noticeboard(int id, string title, string text, DateTime noticeDate)
        {
            Id = id;
            Title = title;
            Text = text;
            NoticeDate = noticeDate;
        }

        public String Title { get; set; }

        public String Text { get; set; }

        public DateTime NoticeDate { get; set; }
    }
}
