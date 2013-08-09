using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportAnalyst.Model;

namespace SupportAnalyst.Data
{
    public interface ILogRepository : IRepository<LogEntry>
    {
        IQueryable<LogEntry> FindByKeyword(string keyword);
        IQueryable<LogEntry> FindByKeyword(string keyword, int pageIndex, int pageSize);
        IQueryable<LogEntry> FindByKeyword(string keyword, DateTime startTime, DateTime endTime, int pageIndex, int pageSize);
        List<LogEntry> FindByCriteria(Criteria criteria); 
        //int DeleteAll();
        //Delete(DateTime startTime, DateTime endTime);

    }
}
