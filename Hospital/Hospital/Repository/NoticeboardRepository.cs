using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Interfaces;
using System.Text;

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
