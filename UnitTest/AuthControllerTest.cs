using Castle.Core.Configuration;
using DbHandler.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentPortal.DTO;
using StudentPortal.Helper;
using DbHandler.Data;
using DbHandler.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Moq.Protected;
using Newtonsoft.Json;
using StudentPortal.Responses;
using System.Net;
using System.Text;
using DbHandler.Data;
using DbHandler.Model;
using DbHandler.Repositories;
using System.Security.Claims;
using SimpleInjector;
using FluentValidation;

namespace UnitTest
{
    [TestClass]
    public class AuthControllerTest
    {
        [TestMethod]
        public void Registration_InputFields_Validation()
        {
            // Arrange
            var registration = new StudentDetails
            {
                Id = "123456",
                stId = "student123",
                cstID = "college123",
                CreatedOn = DateTime.Now,
                IsActive = true,
                Name = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                MobileNo = "1234567890",
                IsGraduated = "Yes"
            };

            var validator = new RegistrationValidator();

            // Act
            var result = validator.Validate(registration);

            // Assert
            Assert.IsTrue(result.IsValid);
        }
    }
    public class RegistrationValidator : AbstractValidator<StudentDetails>
    {
        public RegistrationValidator()
        {
            RuleFor(reg => reg.Id);

            RuleFor(reg => reg.stId);

            RuleFor(reg => reg.cstID);

            RuleFor(reg => reg.CreatedOn);

            RuleFor(reg => reg.Name);

            RuleFor(reg => reg.LastName);
              

            RuleFor(reg => reg.Email) ;

            RuleFor(reg => reg.Password);

            RuleFor(reg => reg.MobileNo);

            RuleFor(reg => reg.IsGraduated);
        }
    }
}
