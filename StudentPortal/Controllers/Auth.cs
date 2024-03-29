﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Data;
using DbHandler.Model;
using DbHandler.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using static StudentPortal.DTO.DTOS;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using StudentPortal.Responses;
using StudentPortal.Models;
using StudentPortal.DTO;
using StudentPortal.Helper;
using System.Transactions;
using Moq;

namespace StudentPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IStudentRepository _studentRep;
        private readonly ICourseRepository _courserep;
        private readonly IEnrollCourse _enroll;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUniqueIdRepository _UniqueId;
        private readonly APIHelper _helper;
        private readonly ILogRepository _logs;
        private readonly IConfiguration _configuration;

     

        //private Mock<Castle.Core.Configuration.IConfiguration> mockConfigRep;

        public Auth(ApplicationDbContext ctx, IStudentRepository studentRep, ICourseRepository courserep, IEnrollCourse enroll, SignInManager<ApplicationUser> signInManager,
                     UserManager<ApplicationUser> userManager, IUniqueIdRepository uniqueId, ILogRepository logs, IConfiguration configuration)
        {
            _ctx = ctx;
            _studentRep = studentRep;
            _courserep = courserep;
            _enroll = enroll;
            _signInManager = signInManager;
            _userManager = userManager;
            _UniqueId = uniqueId;
            _logs = logs;
            _helper = new APIHelper(studentRep, userManager, configuration);
            _configuration = configuration;
            
        }

     
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ActiveResponse<StudentDetails>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            DateTime _startTime = DateTime.Now;
            try
            {
                //var user = _studentRep.GetByStudentEmail(DTO.Email);
                var user = _studentRep.GetByStudentId(DTO.Id);
                if (user == null )
                {
                    return await _helper.Response("err-001", Level.Error, "Invalid email or password", ActiveErrorCode.Failed, _startTime, _logs, HttpContext, _configuration, DTO.BaseClass, DTO, "", ReturnResponse.Unauthorized, null, false);
                }

                return await _helper.Response("suc-001", Level.Success, user.Id, ActiveErrorCode.Success, _startTime, _logs, HttpContext, _configuration, DTO.BaseClass, DTO, user.Id, ReturnResponse.Success, null, true);
            }
            catch (Exception ex)
            {
                return await _helper.Response("err-001", Level.Error, ex.Message, ActiveErrorCode.Failed, _startTime, _logs, HttpContext, _configuration, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest, ex, false);
            }
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ActiveResponse<RegistraiontObject>), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO DTO)
        {
            DateTime _startTime = DateTime.Now;
            var name = "";
            bool exist = false;
            var id = "";
            try
            {
                var jso = JsonConvert.SerializeObject(DTO);

                var forLog = JsonConvert.DeserializeObject<RegisterDTO>(jso);
                if (!TryValidateModel(DTO))
                {
                    //if (!ModelState.IsValid)
                    //{
                    return await _helper.Response("err-Model", Level.Success, _helper.GetErrors(ModelState), ActiveErrorCode.Failed, _startTime, _logs, HttpContext, _configuration, null, forLog, "", ReturnResponse.BadRequest, null, false);    //}

                }
                _UniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "Register", CreatedTime = _startTime });
                var transaction = _ctx.Database.BeginTransaction();
                var stid = Guid.NewGuid().ToString();
                Random rnd = new Random();
                int stdid1 = rnd.Next(10, 99);
                int stdid2 = rnd.Next(1, 9);
                int stdid3 = rnd.Next(1, 7);
                var _stdid = stdid1.ToString() + stdid2.ToString() + stdid3.ToString();
                var _cstudentId="c" + stdid2.ToString() + stdid3.ToString();
                var addStudent= new StudentDetails
                    {
                      Id=stid,
                      stId=_stdid,
                      cstID=_cstudentId,
                      CreatedOn=DateTime.Now,
                      IsActive=true,
                      Name=DTO.Name,
                      LastName=DTO.LastName,
                      Email=DTO.Email,
                      Password=DTO.Password,
                      MobileNo=DTO.MobileNo,
                     IsGraduated=""
                    
                    };
                var financeDTO = new StudentDetails
                {
                    Id = addStudent.Id,
                    stId = addStudent.stId,
                    cstID = addStudent.cstID,
                    CreatedOn = addStudent.CreatedOn,
                    IsActive = addStudent.IsActive,
                    Name = addStudent.Name,
                    LastName = addStudent.LastName,
                    Email = addStudent.Email,
                    Password = addStudent.Password,
                    MobileNo = addStudent.MobileNo,
                    IsGraduated=""
                   // IsGraduated = null, // this property is not set in the Student Portal, so set it to null
                   // BaseClass = DTO.BaseClass
                };

                // Send request to finance portal
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7007/"); // replace with the correct base URL of the finance portal
                var requestUri = "api/Auth/Register";
                var requestBody = new StringContent(JsonConvert.SerializeObject(financeDTO), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, requestBody);

                // Check response status
                var content = await response.Content.ReadAsStringAsync();
                var financeResponse = JsonConvert.DeserializeObject<ActiveResponse<RegisterDTO>>(content);
                _studentRep.AddStudentDet(addStudent);
                _studentRep.Save();


                transaction.Commit();
                if (response.IsSuccessStatusCode)
                {
                    return await _helper.Response("suc-001", Level.Success, stid, ActiveErrorCode.Success, _startTime, _logs, HttpContext, _configuration, null, forLog, _cstudentId, ReturnResponse.Success, null, true);


                }

                
                return await _helper.Response("suc-001", Level.Success, stid, ActiveErrorCode.Success, _startTime, _logs, HttpContext, _configuration, null, forLog, _cstudentId, ReturnResponse.Success, null, true);


            }
            catch (Exception ex)
            {

                var jso = JsonConvert.SerializeObject(DTO);

                var forLog = JsonConvert.DeserializeObject<RegisterDTO>(jso);
                
                return await _helper.Response("ex-0001", Level.Error, null, ActiveErrorCode.Failed, _startTime, _logs, HttpContext, null, null, forLog, "", ReturnResponse.BadRequest, ex, false);
                
            }


        }
        [HttpPost]
        [Route("GetStudentDetails")]
        [ProducesResponseType(typeof(ActiveResponse<StudentDetails>), 200)]

        public async Task<IActionResult> GetStudentDetails([FromBody] GetDet DTO)
        {
            var id = "";
            DateTime startTime = DateTime.Now;
            try
            {
                id = DTO.id;
                if (!TryValidateModel(DTO))
                {
                    if (!ModelState.IsValid)
                    {
                        return await _helper.Response("err-Model", Level.Success, _helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _configuration, DTO.baseClass, DTO, "", ReturnResponse.BadRequest, null, false);
                    }

                }

                //  _uniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "GetAll", CreatedTime = DateTime.Now });
                var student = _studentRep.GetByStudentId(id);
               // if(student.IsGraduated!="")
               // {
                    //var addGrad = new StudentDetails
                    //{ 
                    // IsGraduated=DTO.IsGraduated
                    
                    //};
                    //_studentRep.UpdateStudentDet(addGrad);
                    //_studentRep.Save();
                    return await _helper.Response("succ-001", Level.Success, student, ActiveErrorCode.Success, startTime, _logs, HttpContext, _configuration, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

              //  }


              //  return await _helper.Response("succ-001", Level.Success, student, ActiveErrorCode.Success, startTime, _logs, HttpContext, _configuration, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await _helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _configuration, DTO?.baseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }

        }
        
    }
    
}
