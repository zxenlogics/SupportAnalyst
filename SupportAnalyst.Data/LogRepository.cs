using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportAnalyst.Model;

namespace SupportAnalyst.Data
{
    public class LogRepository : GenericRepository<LogEntry>, ILogRepository
    {
        private const int DefaultHoursFromNow = -12;
        private const int DefaultPageIndex = 0;
        private const int DefaultPageSize = 50;

        public LogRepository()
        { }

        public LogRepository(DbContext dbContext) :
            base(dbContext)
        { }

        public IQueryable<LogEntry> FindByKeyword(string keyword)
        {
            return FindByKeyword(keyword, DefaultPageIndex,  DefaultPageSize);
        }

        public IQueryable<LogEntry> FindByKeyword(string keyword, int pageIndex, int pageSize)
        {
            return FindByKeyword(keyword, DateTime.Now, DateTime.Now.AddHours(DefaultHoursFromNow), pageIndex, pageSize);
        }

        public IQueryable<LogEntry> FindByKeyword(string keyword, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            return DataContext.Set<LogEntry>().Where(l => l.Message.Contains(keyword) && l.TimeStamp <= startTime && l.TimeStamp >= endTime);
        }

        public List<LogEntry> FindByCriteria(Criteria criteria)
        {
            return this.Get().Where(criteria.GetExpression()).ToList();
        }
    }
}
