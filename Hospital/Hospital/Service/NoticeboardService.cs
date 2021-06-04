using Model;
using System.Collections.Generic;

namespace Service
{
    public class NoticeboardService
    {

        private Repository.NoticeboardRepository noticeboardRepository = new Repository.NoticeboardRepository();

        public List<Noticeboard> GetAll()
        {
            return noticeboardRepository.GetAll();
        }

        public Noticeboard GetById(int id)
        {
            return noticeboardRepository.GetById(id);
        }

        public void Save(Model.Noticeboard noticeboard)
        {
            noticeboardRepository.Save(noticeboard);
        }

        public void Delete(int id)
        {
            noticeboardRepository.Delete(id);

        }

        public void Update(Model.Noticeboard noticeboard)
        {
            noticeboardRepository.Update(noticeboard);
        }

        public int GenerateNewId()
        {
            return noticeboardRepository.GenerateNewId();
        }
    }
}
