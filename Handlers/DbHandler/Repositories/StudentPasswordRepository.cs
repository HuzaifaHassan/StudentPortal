using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Data;
using DbHandler.Model;
using Microsoft.AspNetCore.Identity;

namespace DbHandler.Repositories
{
    public class StudentPasswordRepository : IStudentPAsswordRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        public StudentPasswordRepository(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        public async Task<bool> Checklast10Passwords(string studentId, string password, int count)
        {
            var stud = await _userManager.FindByIdAsync(studentId);
            var data = _ctx.TStudentPasswords
                .Where(x => x.studentId.Trim() == studentId.Trim())
                .OrderByDescending(x => x.CreatedOn)
                .Take(count)
                .ToList();
            foreach (var item in data)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(stud, item.passwordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    return true;
                }

            }
            return false;
        }
        public void Add(StudentPassword item)
        { 
        _ctx.TStudentPasswords.Add(item);
        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;    
        }

        public Task<bool> CheckLast10Passwords(string studentId, string password, int count)
        {
            throw new NotImplementedException();
        }
    }

}
