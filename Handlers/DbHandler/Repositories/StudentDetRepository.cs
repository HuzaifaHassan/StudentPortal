using System;
using System.Collections.Generic;
using System.Text;
using DbHandler.Model;
using DbHandler.Data;
using System.Linq;
using Azure;


namespace DbHandler.Repositories
{
    public class StudentDetRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _ctx;
        public StudentDetRepository(ApplicationDbContext ctx) => _ctx = ctx;
        public void AddStudentDet(StudentDetails Model)
        {
            _ctx.TStudentDetails.Add(Model);


        }
        public void DelStudentDet(StudentDetails Model)
        {
            _ctx.TStudentDetails.Remove(Model);
        }
        public StudentDetails GetByStudentId(string StudentId)
        {
           var resp = _ctx.TStudentDetails.Where(x => x.IsActive == true && x.stId == StudentId).FirstOrDefault();
            return resp;
        
        }
        public StudentDetails GetActiveNonActiveByStudentId(string StudentId)
        {
            var resp = _ctx.TStudentDetails.Where(x => x.stId == StudentId).FirstOrDefault();
            return resp;
        }

        public bool Save()
        {
            return _ctx.SaveChanges()>=0;

        }
        public void UpdateStudentDet(StudentDetails Model)
        {
            _ctx.TStudentDetails.Update(Model);
        
        }
    }
}
