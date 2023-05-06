using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public  class CourseDuesRepository :ICourseDuesRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IDbConnection _db;

        public CourseDuesRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public void AddCourseDues(CourseDues model)
        {
            _ctx.TCourseDues.Add(model);
        
        }
        public CourseDues GetByRef(string Ref)
        {
            var resp = _ctx.TCourseDues.Where(x => x.Ref == Ref).FirstOrDefault();
            return resp;
        }
        public decimal CalculateTotal()
        {
            return Convert.ToInt64(_ctx.TCourseDues.Sum(x =>Convert.ToInt64( x.CourseDue)));
        }
        public CourseDues GetByid(string id)
        {
            var resp = _ctx.TCourseDues.Where(x =>  x.id == id).FirstOrDefault();
            return resp;
        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;
        }
        public void UpdateCourseDues(CourseDues model)
        {

            _ctx.TCourseDues.Update(model);
        }
    }
}
