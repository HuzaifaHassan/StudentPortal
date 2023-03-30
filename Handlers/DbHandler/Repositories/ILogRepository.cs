using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public interface ILogRepository
    {
        void Add(Log model);
        void InnerLogs(ILogRepository _innerLog, string testMode, string baseUrl, string url, DateTime? Startime, DateTime? EndTime, string StudentId, string RequestBody, string RequestResponse);
        void WriteInnerLogs(ILogRepository _innerLog, string baseUrl, string url, DateTime? Startime, DateTime? EndTime, string StudentId, string RequestBody, string RequestResponse);
        bool Save();

    }
}
