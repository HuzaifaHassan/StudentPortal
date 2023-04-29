using DbHandler.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task StoreGUIDAsync(ApplicationUser user, Guid guid);
        Task<Guid?> GetGUIDAsync(ApplicationUser user);
    }
}
