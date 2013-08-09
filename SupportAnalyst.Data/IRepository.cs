using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAnalyst.Data
{
    public interface IRepository<T> where T: class
    {
        T FindById(int key);
        IQueryable<T> Get();
        //void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);
        //void Delete(int id);
    }
}
    