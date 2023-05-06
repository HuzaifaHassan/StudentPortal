using System;
using NUnit.Framework;
using StudentPortal.Controllers;
using static StudentPortal.DTO.DTOS;
using StudentPortal.Models;
using DbHandler.Data;
using DbHandler.Model;
using DbHandler.Repositories;
using Microsoft.AspNetCore.Identity;
using StudentPortal.Helper;
using System.Configuration;
using Castle.Core.Configuration;
using DbHandler.Model;
using DbHandler.Data;
using DbHandler.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using SimpleInjector;
using FluentValidation;

namespace IntegrationTest
{
    [TestClass]
    public class Integration
    {
        [TestMethod]
        public void TestCourseModelIntegration()
        {
            // Arrange
            var EnrollCourses = new EnrollCourses
            {
                Id = 1,
                stId = "ST123",
                CourseCode = "CSE101",
                CourseName = "Introduction to Computer Science",
                fees = "1000",
                IsActive = true
            };

          
            var validator = new RegistrationValidator();

           
            var result = validator.Validate(EnrollCourses);

          
            
        }
    }
    public class RegistrationValidator : AbstractValidator<EnrollCourses>
    {
        public RegistrationValidator()
        {
            RuleFor(reg => reg.Id);

            RuleFor(reg => reg.stId);



            RuleFor(reg => reg.CourseCode);


            RuleFor(reg => reg.CourseName);


            RuleFor(reg => reg.fees);
            RuleFor(reg => reg.IsActive);
        }
    }
}