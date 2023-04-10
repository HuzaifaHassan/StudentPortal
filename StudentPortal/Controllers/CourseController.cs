using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DbHandler.Model;
using DbHandler.Repositories;
using StudentPortal.Helper;
using StudentPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static StudentPortal.DTO.DTOS;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Helper;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using StudentPortal.DTO;
using DbHandler.Data;


namespace StudentPortal.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly APIHelper helper;
        private readonly IConfiguration _config;
        private readonly IUniqueIdRepository _uniqueId;
        private readonly ILogRepository _logs;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICourseRepository _course;
        private readonly IStudentRepository _student;
        private readonly EnrollCourseRepository _enroll;

        public CourseController(IUniqueIdRepository uniqueId, ICourseRepository course, ILogRepository logs, UserManager<ApplicationUser> userManager,
                                 IConfiguration config, IStudentRepository student, EnrollCourseRepository enroll)
        {

            _course = course;
            _uniqueId = uniqueId;
            _logs = logs;   
            _userManager = userManager; 
            _config = config;
            _student = student;
            _enroll = enroll;
            helper = new APIHelper(student,userManager, config);
        
        }
        [HttpPost]
        [Route("GetCourses")]
        [ProducesResponseType(typeof(ActiveResponse<List<Courses>>), 200)]
        public async Task<IActionResult> GetCourses([FromBody] GetAllCourses DTO)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                if (!TryValidateModel(DTO))
                {
                    if (!ModelState.IsValid)
                    {
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest,null,false);
                    }
                
                }
                _uniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "GetAll", CreatedTime = DateTime.Now });
                var course = _course.GetCourses();
                return await helper.Response("succ-001", Level.Success, course, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.Success, null, false);
            
            }
            catch (Exception ex) 
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.BaseClass, DTO, "", ReturnResponse.BadRequest, ex, false);
            
            
            }
        
        }
        [HttpPost]
        [Route("AddCourses")]
        public async Task<IActionResult> EnrollCourse([FromBody] AddCourses DTO)
        {
            DateTime startTime = DateTime.Now;
            var id = "";
            try
            {
                id = HttpContext.User.Claims.First().Value.ToString();
                if (!TryValidateModel(DTO))
                {
                    if (!ModelState.IsValid)
                    {
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest, null, false);

                    }
                }
                var student = await _userManager.FindByIdAsync(id);
                string _stud = student == null ? "NULL" : student.ToString();
                var Tstud = _student.GetByStudentId(id);
                using (var transaction = _ctx.Database.BeginTransaction())
                {
                    var stCourses = _enroll.GetByid(id);
                    if (stCourses.Count < 0)
                    {
                        List<EnrollCourses> enr = new List<EnrollCourses>();
                        foreach (var StCourse in stCourses)
                        {
                            EnrollCourses _enr = new EnrollCourses();
                            _enr.IsActive = true;
                            _enr.CourseName = DTO.CourseName;
                            _enr.CourseCode = DTO.CourseCode;
                            enr.Add(_enr);

                        }
                        if (enr.Count < 0)
                        {
                            _enroll.AddCourse(enr);
                            _enroll.Save();

                        }

                    }

                    transaction.Commit();
                }
                return await helper.Response("succ-001", Level.Success, "", ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.BaseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }

        }



    }
}
