using DbHandler.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbHandler.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AppConfig> TAppConfig { get; set; }
        public DbSet<StudentDetails> TStudentDetails { get; set; }
        public DbSet<Courses> TCourses { get; set; }
        public DbSet<EnrollCourses> TEnrollCourses { get; set; }

    }
}
