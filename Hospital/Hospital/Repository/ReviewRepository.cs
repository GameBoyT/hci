using Model;
using Repository.Interfaces;
using System;
using System.IO;

namespace Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository()
        {
            _fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\reviews.json";
            ReadJson();
        }
    }
}