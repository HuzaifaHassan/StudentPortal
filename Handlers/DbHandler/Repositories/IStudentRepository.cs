using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Model;

namespace DbHandler.Repositories
{
    public interface IStudentRepository
    {
        void AddStAddStudentDet(StudentDetails Model);
        void DelStudentDet(StudentDetails Model);
        void UpdateStudentDet(StudentDetails Model);
        StudentDetails GetByStudentId(string StudentId);
        StudentDetails GetActiveNonActiveByStudentId(string StudentId);
        bool Save();
    }
}
