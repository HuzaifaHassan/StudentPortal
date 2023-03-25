using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface IStudentPAsswordRepository
    {
        Task<bool> CheckLast10Passwords(string studentId, string password, int count);
        void Add(StudentPassword item);
        bool Save();
    }
}
