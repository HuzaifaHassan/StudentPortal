using System;
using System.Collections.Generic;
using System.Text;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public  interface IEnrollCourse
    {
        void AddStdCourses(EnrollCourses model);
        void RemoveCourse(EnrollCourses model);
        List<EnrollCourses> GetByid(string StuId);
        void AddCourse(List<EnrollCourses> model);
        EnrollCourses GetBycstId(string Cstid);
        EnrollCourses GetActiveNonActiveBycstId(string Cstid);
        bool Save();
    }
}
