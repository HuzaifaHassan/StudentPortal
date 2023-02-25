using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public class AppConfigRepository:IAppConfigRepository
    {
        private readonly ApplicationDbContext _ctx;
        public AppConfigRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public IEnumerable<AppConfig> GetActive()
        {
            return _ctx.TAppConfig.Where(s => s.IsActive == true);
        }
        public AppConfig getbykey(string key)
        { 
         var app=_ctx.TAppConfig.Where(s=>s.Key==key).FirstOrDefault();
            return app;
                        
        
        }
    }
}
