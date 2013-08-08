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
        IEnumerable<T> Get();
        List<T> FindByKeyword(string keyword, int pageIndex, int pageSize);
        List<T> FindByKeyword(string keyword, DateTime startTime, DateTime endTime, int pageIndex, int pageSize);
        List<T> FindByCriteria(Criteria criteria); 
        int Delete();
        int Delete(DateTime startTime, DateTime endTime);
    }
}
    