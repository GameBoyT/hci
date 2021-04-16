using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class DirectorController
    {

        private Service.DirectorService directorService = new Service.DirectorService();


        public List<Director> GetAll()
        {
            return directorService.GetAll();
        }

        public Model.Director GetByJmbg(String jmbg)
        {
            return directorService.GetByJmbg(jmbg);
        }

        public void Save(Model.Director director)
        {
            directorService.Save(director);
        }

        public void Delete(String jmbg)
        {
            directorService.Delete(jmbg);
        }

        public void Update(Model.Director director)
        {
            directorService.Update(director);
        }



    }
}