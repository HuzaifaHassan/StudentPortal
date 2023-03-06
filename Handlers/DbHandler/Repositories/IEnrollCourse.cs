using System;
using System.Collections.Generic;
using System.Text;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public  interface IEnrollCourse
    {
        void AddStdCourses(EnrollCourses model);
        void RemoveCourses(EnrollCourses model);
        EnrollCourses GetByGetBycstId(string Cstid);
        EnrollCourses GetActiveNonActiveBycstId(string Cstid);
        bool Save();
    }
}
