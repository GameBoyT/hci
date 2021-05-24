using Model;
using Repository.Interfaces;
using System;
using System.IO;


namespace Repository
{
    public class MovingStaticRepository : GenericRepository<MovingStatic>, IMovingStaticRepository
    {
        public MovingStaticRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\static.json";
            ReadJson();
        }
    }
}
