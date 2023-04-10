using System;
using System.Collections.Generic;
using System.Text;
using DbHandler.Model;
using DbHandler.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace DbHandler.Repositories
{
    public  class EnrollCourseRepository:IEnrollCourse
    {
        private readonly ApplicationDbContext _ctx;
        public void AddStdCourses(EnrollCourses model)
        {
            _ctx.TEnrollCourses.Add(model);
        
        }
        public void RemoveCourse(EnrollCourses model)
        {
            _ctx.TEnrollCourses.Remove(model);
        }
        public List<EnrollCourses> GetByid(string StuId)
        {
            return _ctx.TEnrollCourses.Where(s => s.cstId == StuId && s.IsActive == true).ToList();
        }
        public void AddCourse(List<EnrollCourses> model)
        {
            _ctx.TEnrollCourses.AddRange(model);
        }
        public EnrollCourses GetBycstId(string Cstid)
        {
            var response = _ctx.TEnrollCourses.Where(x => x.IsActive == true &&
                                    x.cstId == Cstid).FirstOrDefault();
            return response;
        }
        public EnrollCourses GetActiveNonActiveBycstId(string Cstid)
        {
            var response = _ctx.TEnrollCourses.Where(x => x.cstId == Cstid).FirstOrDefault();
            return response;
        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;
        }
    }
}
