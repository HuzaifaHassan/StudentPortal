using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Data;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public class LogRepository: ILogRepository
    {

        private readonly ApplicationDbContext _ctx;

        public LogRepository(ApplicationDbContext context)
        { 
         _ctx = context;
        
        }
        public void Add(Log model)
        { 
         _ctx.TLog.Add(model);
        }
        public void WriteInnerLogs(ILogRepository _innerLog, string baseUrl, string url, DateTime? _StartTime,DateTime? _EndTime, string StudentId, string requestbody, string RequestResponse)
        {
            try
            {
                Log innerlog = new Log();
                innerlog.ID=Guid.NewGuid().ToString();
                innerlog.ControllerName = baseUrl;
                innerlog.FunctionName= url;
                innerlog.StartTime = _StartTime;
                innerlog.EndTime = _EndTime;
                innerlog.UserID = StudentId;
                innerlog.RequestParameters = requestbody;   
                innerlog.RequestResponse = RequestResponse;
                innerlog.Message = "inner Log";
                Add(innerlog);
                Save();


            }
            catch (Exception ex)
            { 
             
            }
        
        
        
        }
        public void InnerLogs(ILogRepository _innerLog, string testMode, string baseUrl, string url, DateTime? StartTime, DateTime? EndTime, string studentId, string RequestBody, string RequestResponse)
        {
            if (testMode == "1")
            {
                WriteInnerLogs(_innerLog, baseUrl, url, StartTime, EndTime, studentId, RequestBody, RequestResponse);
            }
        }
        public bool Save()
        {
            return _ctx.SaveChanges() > 0;
        }

    }
}
