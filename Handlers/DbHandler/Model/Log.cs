using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace DbHandler.Model
{
    [Table("T_Logs")]
    public class Log
    {
        public string ID { get; set; }
        public string FunctionName { get; set; }
        public string ControllerName { get; set; }
        public string DeviceID { get; set; }
        public string Browser { get; set; }
                                              
        public string IP { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set;}
        public string Exception { get; set; }

        public string Message { get; set; } 
        public string Level { get; set; }
        public string Status { get; set; }

        public string UserID { get; set; }

        public string RequestParameters { get; set; }

        public string RequestResponse { get; set; }

        public string Version { get; set; }


    }
}
