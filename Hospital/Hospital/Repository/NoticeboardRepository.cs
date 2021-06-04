using Model;
using Repository.Interfaces;
using System;
using System.IO;

namespace Repository
{
    public class NoticeboardRepository : GenericRepository<Noticeboard>, INoticeboardRepository
    {
        public NoticeboardRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\noticeboards.json";
            ReadJson();
        }
    }
}
