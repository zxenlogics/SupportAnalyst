using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportAnalyst.Model;

namespace SupportAnalyst.Data
{
    public class PacLogRepository : LogRepository, IPacLogRepository
    {
        public int GetCountOfLoginAttempt(DateTime FromDate, DateTime ToDate)
        {
            throw new NotImplementedException();
        }

        public int GetCountOfFailedRegistration(DateTime FromDate, DateTime ToDate)
        {
            throw new NotImplementedException();
        }

        public int GetCountOfPACIncidence(DateTime FromDate, DateTime ToDate)
        {
            throw new NotImplementedException();
        }
    }
}
