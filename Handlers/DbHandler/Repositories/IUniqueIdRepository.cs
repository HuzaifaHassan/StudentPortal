using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public interface IUniqueIdRepository
    {
        void Add(UniqueID item);
    }
}
