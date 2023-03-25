using DbHandler.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AppConfig> TAppConfig { get; set; }
        public DbSet<StudentDetails> TStudentDetails { get; set; }
        public DbSet<Courses> TCourses { get; set; }
        public DbSet<EnrollCourses> TEnrollCourses { get; set; }
        public DbSet<StudentPassword> TStudentPasswords { get; set; }
        public DbSet<UniqueID> TUniqueId { get; set; }

    }
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        { 
         var optionsBuilder= new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source=HUZAIFAHASSAN\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True;TrustServerCertificate=True");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
