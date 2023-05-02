using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface ICourseDuesRepository
    {
        void AddCourseDues(CourseDues model);
        decimal CalculateTotal();
        public CourseDues GetByid(string id);
        public bool Save();
        public void UpdateCourseDues(CourseDues model);
    }
}
