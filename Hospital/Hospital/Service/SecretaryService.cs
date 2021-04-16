using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class SecretaryService
    {
        public Repository.SecretaryRepository secretaryRepository = new Repository.SecretaryRepository();


        public List<Secretary> GetAll()
        {
            return secretaryRepository.GetAll();
        }

        public Model.Secretary GetByJmbg(String jmbg)
        {
            return GetByJmbg(jmbg);
        }

        public void Save(Model.Secretary secretary)
        {
            secretaryRepository.Save(secretary);
        }

        public void Delete(String jmbg)
        {
            secretaryRepository.Delete(jmbg);
        }

        public void Update(Model.Secretary secretary)
        {
            secretaryRepository.Update(secretary);
        }


    }
}