using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IGenericRepository<T> where T : Entity
    {
        List<T> GetAll();

        T GetById(int id);
        
        T Save(T entity);
        
        T Update(T entity);
        
        T Delete(int id);
    }
}
