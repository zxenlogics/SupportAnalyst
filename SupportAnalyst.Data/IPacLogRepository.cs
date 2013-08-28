using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAnalyst.Data
{
    public interface IPacLogRepository : ILogRepository
    {
        int GetCountOfLoginAttempt(DateTime FromDate, DateTime ToDate);
        int GetCountOfFailedRegistration(DateTime FromDate, DateTime ToDate);
        int GetCountOfPACIncidence(DateTime FromDate, DateTime ToDate);
    }
}
