using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Data;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public class UniqueIdRepository: IUniqueIdRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UniqueIdRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(UniqueID item)
        {
            _ctx.TUniqueId.Add(item);
            _ctx.SaveChanges();
        }
    }
}
