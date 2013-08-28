using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAnalyst.Data
{
    public class ESamplesLogRepository : LogRepository, IESamplesLogRepository
    {
        public int GetCountOfSampleOrders(DateTime FromDate, DateTime ToDate)
        {
            throw new NotImplementedException();
        }

        public int GetCountOfSubmitSRFErrors(DateTime FromDate, DateTime ToDate)
        {
            throw new NotImplementedException();
        }
    }
}
