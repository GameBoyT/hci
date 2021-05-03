using System;

namespace Model
{
    public class Noticeboard
    {
        public Noticeboard(int id, string title, string text, DateTime noticeDate)
        {
            Id = id;
            Title = title;
            Text = text;
            NoticeDate = noticeDate;
        }

        public int Id
        {
            get
            ;
            set
            ;
        }

        public String Title
        {
            get
            ;
            set
            ;
        }

        public String Text
        {
            get
            ;
            set
            ;
        }

        public DateTime NoticeDate
        {
            get
            ;
            set
            ;
        }
    }
}
