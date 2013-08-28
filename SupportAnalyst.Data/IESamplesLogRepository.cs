using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAnalyst.Data
{
    public interface IESamplesLogRepository
    {
        int GetCountOfSampleOrders(DateTime FromDate, DateTime ToDate);
        int GetCountOfSubmitSRFErrors(DateTime FromDate, DateTime ToDate);
    }
}
