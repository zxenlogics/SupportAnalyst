using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SupportAnalyst.Model;

namespace SupportAnalyst.Data
{
    public interface IFluentCriteria
    {
        IFluentCriteria EventType(string logType);
        IFluentCriteria EventStartTime(DateTime startTime);
        IFluentCriteria EventEndTime(DateTime endTime);
        IFluentCriteria MessageKeyword(string keyword);
        Expression<Func<LogEntry, bool>> CriteriaExpression();
    }

    public class Criteria
    {
        public Criteria()
        {
        }

        public string LogType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Keyword { get; set; }

        public Expression<Func<LogEntry, bool>> GetExpression()
        {
           Expression<Func<LogEntry, bool>> filter = l =>            
                l.LogType == LogType && l.TimeStamp >= StartTime && l.TimeStamp <= EndTime &&
                l.Message.Contains(Keyword);
            
            return filter;
        }
    }

    public class QueryCriteria : IFluentCriteria
    {
        private Criteria _criteria;

        public QueryCriteria()
        {
            _criteria = new Criteria();
            ;
        }

        public IFluentCriteria EventType(string logType)
        {
            _criteria.LogType = logType;
            return this;
        }

        public IFluentCriteria EventStartTime(DateTime startTime)
        {
            _criteria.StartTime = startTime;
            return this;
        }

        public IFluentCriteria EventEndTime(DateTime endTime)
        {
            _criteria.EndTime = endTime;
            return this;
        }

        public IFluentCriteria MessageKeyword(string keyword)
        {
            _criteria.Keyword = keyword;
            return this;
        }

        public Expression<Func<LogEntry, bool>> CriteriaExpression()
        {
            return _criteria.GetExpression();
        }
    }


}
