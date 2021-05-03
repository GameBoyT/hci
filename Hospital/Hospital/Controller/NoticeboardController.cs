using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class NoticeboardController
    {
        private Service.NoticeboardService noticeboardService = new Service.NoticeboardService();

        public List<Noticeboard> GetAll()
        {
            return noticeboardService.GetAll();
        }

        public Noticeboard GetById(int id)
        {
            return noticeboardService.GetById(id);
        }

        /*public Noticeboard GetByTitle(String title)
        {
            return noticeboardService.GetByTitle(title);
        }*/

        public void Save(Model.Noticeboard noticeboard)
        {
            noticeboardService.Save(noticeboard);
        }

        public void Delete(int id)
        {
            noticeboardService.Delete(id);
        }

        public void Update(Model.Noticeboard noticeboard)
        {
            noticeboardService.Update(noticeboard);
        }
        public int GenerateNewId()
        {
            return noticeboardService.GenerateNewId();
        }
    }
}
