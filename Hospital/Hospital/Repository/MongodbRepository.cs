//using Model;
//using MongoDB.Driver;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace Repository
//{
//    public class Repository
//    {
//        private IMongoDatabase db;

//        public Repository()
//        {
//            var client = new MongoClient();
//            db = client.GetDatabase("Hospital");
//        }


//        public void Save<T>(string table, T obj)
//        {
//            var collection = db.GetCollection<T>(table);
//            collection.InsertOne(obj);
//        }

//    }
//}