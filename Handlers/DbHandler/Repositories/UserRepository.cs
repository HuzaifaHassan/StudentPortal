using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public class UserRepository :IUserRepository
    {
        //private readonly ApplicationDbContext _ctx;
        //public UserRepository(ApplicationDbContext ctx) => _ctx = ctx;
        ////public User GetById(string id)
        ////{
        ////    return _ctx.TUsers.Where(s => s.Id == id).FirstOrDefault();
        ////}
        //public User GetByEmail(string email)
        //{
        //    return _ctx.TUsers.Where(s => s.Email == email).FirstOrDefault();
        //}
        //public void Add(User item)
        //{
        //    _ctx.TUsers.Add(item);
        //}
        //public bool Save()
        //{
        //    return _ctx.SaveChanges() >= 0;
        //}
    }
}
