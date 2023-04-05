﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DbHandler.Data;
using DbHandler.Model;
using Microsoft.EntityFrameworkCore;

using System.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using DbHandler.Repositories;
using System.Reflection;
using Microsoft.OpenApi.Models;
using DbHandler.Repositories;
using Microsoft.Data.SqlClient;

namespace StudentPortal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
               
        }
        public void ConfigureServices(IServiceCollection services)
        {
          //  var connection = "";
            var connection = "Data Source=HUZAIFAHASSAN\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(connection);
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(connection, sqlServerOptionsAction=>
                  {
                      sqlServerOptionsAction.CommandTimeout(3600);
                  });
            });

            services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                
                sqlServerOptions => sqlServerOptions.CommandTimeout(3600));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;

            })
        .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            #region Cors
            services.AddCors(options =>
            {
                   options.AddPolicy("AllowAll",
                     builder =>
                     {
                         builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                     });
            });
            #endregion

            #region Customservices
            services.AddScoped<IAppConfigRepository, AppConfigRepository>();
            services.AddScoped<ICourseRepository, CoursesRepository>();
            services.AddScoped<IStudentRepository, StudentDetRepository>();
            services.AddScoped<IEnrollCourse, EnrollCourseRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUniqueIdRepository, UniqueIdRepository>();
            #endregion
            services.AddHttpClient();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title="Student Portal",
                    Version="V1",
                    Description="Microservice for StudentPortal",

                });

            });
            
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            }); services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            // app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));

        }

    }
}
