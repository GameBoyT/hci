using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class SecretaryController
    {
        private Service.SecretaryService secretaryService = new Service.SecretaryService();

        public List<Secretary> GetAll()
        {
            return secretaryService.GetAll();
        }

        public Model.Secretary GetByJmbg(String jmbg)
        {
            return GetByJmbg(jmbg);
        }

        public void Save(Model.Secretary secretary)
        {
            secretaryService.Save(secretary);
        }

        public void Delete(String jmbg)
        {
            secretaryService.Delete(jmbg);
        }

        public void Update(Model.Secretary secretary)
        {
            secretaryService.Update(secretary);
        }


    }
}