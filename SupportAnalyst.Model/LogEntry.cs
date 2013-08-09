using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAnalyst.Model
{
    public class LogEntry
    {        
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string LogType { get; set; }
        public string Misc { get; set; }
        public string Message { get; set; }
    }
}
