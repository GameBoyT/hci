using Model;
using System;
using System.Collections.Generic;


namespace Service
{
    public class DirectorService
    {
        private Repository.DirectorRepository directorRepository = new Repository.DirectorRepository();

        public List<Director> GetAll()
        {
            return directorRepository.GetAll();
        }

        public Model.Director GetByJmbg(String jmbg)
        {
            return directorRepository.GetByJmbg(jmbg);
        }

        public void Save(Model.Director director)
        {
            directorRepository.Save(director);
        }

        public void Delete(String jmbg)
        {
            directorRepository.Delete(jmbg);
        }

        public void Update(Model.Director director)
        {
            directorRepository.Update(director);
        }


    }
}