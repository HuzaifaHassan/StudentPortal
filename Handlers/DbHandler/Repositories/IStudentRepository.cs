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
        void AddStudentDet(StudentDetails Model);
        void DelStudentDet(StudentDetails Model);
        void UpdateStudentDet(StudentDetails Model);
        StudentDetails GetByStudentId(string StudentId);
        StudentDetails GetByCStudentId(string CStudentId);
        StudentDetails GetActiveNonActiveByStudentId(string StudentId);
        StudentDetails GetActiveNonActiveByCStudentId(string CStudentId);
        StudentDetails GetByStudentEmail(string email);

       bool Save();
    }
}
