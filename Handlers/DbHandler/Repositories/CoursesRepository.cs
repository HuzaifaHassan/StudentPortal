using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbHandler.Repositories
{
    public class CoursesRepository:ICourseRepository
    {
        private readonly ApplicationDbContext _ctx;
        public CoursesRepository(ApplicationDbContext context)
        {
            _ctx = context;
        
     
        }
        public List<Courses> GetCourses()
        {
            return _ctx.TCourses.Where(x => x.IsActive == true).ToList();
        
        }
        public Courses GetByCourseName(string courseName)
        {
            return _ctx.TCourses.Where(x => x.CourseName == courseName).FirstOrDefault();
        }
        public decimal CalculateTotal()
        {
            return Convert.ToInt64(_ctx.TCourses.Sum(x => Convert.ToInt64(x.CourseFee)));
        }
    }
}
