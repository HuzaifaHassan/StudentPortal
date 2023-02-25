using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface IAppConfigRepository
    {
        IEnumerable<AppConfig> GetActive();
        AppConfig getbykey(string key);
        
    }
}
