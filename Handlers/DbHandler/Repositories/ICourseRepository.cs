using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbHandler.Repositories
{
    public interface ICourseRepository
    {
        List<Courses> GetCourses();
        Courses GetByCourseName(string courseName);

    }
}
