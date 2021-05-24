using Model;
using Repository.Interfaces;
using System;
using System.IO;


namespace Repository
{
    public class RenovationRepository : GenericRepository<RenovationAppointment>, IRenovationRepository
    {
        public RenovationRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\renovation.json";
            ReadJson();
        }
    }
}
