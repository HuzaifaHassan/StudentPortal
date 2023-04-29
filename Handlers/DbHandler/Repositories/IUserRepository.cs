using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface IUserRepository
    {
        void Add(User item);
        User GetByEmail(string email);
        bool Save();
    }
}
