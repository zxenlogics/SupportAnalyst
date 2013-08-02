using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAnalyst.Model;


namespace SupportAnalyst.Repository
{
    public class PacLogRepository : LogRepository<ESamplesDbContext>
    {
        public PacLogRepository()
        {
            
        }
    }

    public class LogRepository<C> : RepositoryBase, ILogRepository 
        where C: DbContext, new()
    {
        //private C db = null;

        public LogRepository()
        {}

        public LogRepository(C dbContext): 
            base(dbContext)
        {}

        public LogEntry FindById(int key)
        {
            return base.FindById<LogEntry>(key);
        }

        //public LogEntry FindById(int key)
        //{
        //    return new LogEntry()
        //    {
        //        Id = 1568, 
        //        TimeStamp = DateTime.Now, 
        //        LogType = "Debug", 
        //        Logger = "Generic",
        //        Message = @"[(null)] – Method=OnLoad, SessionId=jfpe2mehut1pokqxfdgcfqna, HcpId=1249475, eMailID=esigtest804@mailinator.com, CurrentPage=/requestsamples.aspx, Context.Server.MachineName=W8-DBROWN, QueryString:"
        //    };
        //}

        public List<LogEntry> FindByKeyword(string keyword, int pageIndex, int pageSize)
        {
            
            throw new NotImplementedException();
        }

        public List<LogEntry> FindByKeyword(string keyword, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {        
            throw new NotImplementedException();
        }

        public int Delete(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }
    }
}
