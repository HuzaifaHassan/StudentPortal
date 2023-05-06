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
using System.Net;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;

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
        private readonly IEnrollCourse _enroll;
        private readonly ICourseDuesRepository _coursedues;

        public CourseController(IUniqueIdRepository uniqueId, ICourseRepository course, ILogRepository logs, UserManager<ApplicationUser> userManager,
                                 IConfiguration config, IStudentRepository student, IEnrollCourse enroll, ICourseDuesRepository coursedues)
        {

            _course = course;
            _uniqueId = uniqueId;
            _logs = logs;
            _userManager = userManager;
            _config = config;
            _student = student;
            _enroll = enroll;
            helper = new APIHelper(student, userManager, config);
            _coursedues = coursedues;
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
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest, null, false);
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
        [Route("GetEnrolledCourses")]
        [ProducesResponseType(typeof(ActiveResponse<List<Courses>>), 200)]

        public async Task<IActionResult> ViewEnrolledCourses([FromBody] ViewCourses DTO)
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
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.BadRequest, null, false);
                    }

                }

                //  _uniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "GetAll", CreatedTime = DateTime.Now });
                var course = _enroll.GetByid(id);
                return await helper.Response("succ-001", Level.Success, course, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.baseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }







        }
        [HttpPost]
        [Route("GetCourseDues")]
        [ProducesResponseType(typeof(ActiveResponse<List<Courses>>), 200)]
        public async Task<IActionResult> GetCourseDues([FromBody] AddCourseFee DTO)
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
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.BadRequest, null, false);
                    }

                }

                //  _uniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "GetAll", CreatedTime = DateTime.Now });
                var dues = _coursedues.GetByid(id);
                return await helper.Response("succ-001", Level.Success, dues, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.baseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }



        }

        [HttpPost]
        [Route("AddCourses")]
        public async Task<IActionResult> EnrollCourse([FromBody] AddCourses DTO)
        {
            DateTime startTime = DateTime.Now;
            var _id = "";
            try
            {
                // var user= await _userManager.FindByIdAsync(id);
                _id = DTO.id;
                //  id = "39d19764-a330-4ee2-8ac8-008d15716342";
                if (!TryValidateModel(DTO))
                {
                    if (!ModelState.IsValid)
                    {
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest, null, false);

                    }
                }
                var sstudent = await _userManager.FindByIdAsync(_id);
               // string _stud = sstudent == null ? "NULL" : sstudent.ToString();
              //  var Tstud = _student.GetByStudentId(_id);
             //   using (var transaction = _ctx.Database.BeginTransaction())
               // {
                    var stCourses = _enroll.GetByid(_id);
                    if (stCourses.Count <= 0 || stCourses.Count >=0)
                    {
                        List<EnrollCourses> enr = new List<EnrollCourses>();
                        //  foreach (var StCourse in stCourses)
                        //{
                        EnrollCourses _enr = new EnrollCourses();
                        _enr.IsActive = true;
                        _enr.CourseName = DTO.CourseName;
                        _enr.CourseCode = DTO.CourseCode;
                        _enr.fees = DTO.CourseFee;
                        _enr.stId = _id;
                        enr.Add(_enr);

                        //}
                        //if (enr.Count <= 0)
                        // {
                        _enroll.AddCourse(enr);
                        _enroll.Save();

                        //}

                  //  }

                    //transaction.Commit();
                }
                return await helper.Response("succ-001", Level.Success, _id, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.BaseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }

        }
        [HttpPost]
        [Route("AddCourseFee")]
        [ProducesResponseType(typeof(ActiveResponse<List<Courses>>), 200)]

        public async Task<IActionResult> AddCourseFees([FromBody] AddCourseFee DTO)
        {
            var id = "";
            DateTime startTime = DateTime.Now;
            var _ref="";
            try
            {
                id = DTO.id;
                if (!TryValidateModel(DTO))
                {
                    if (!ModelState.IsValid)
                    {
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.BadRequest, null, false);
                    }

                }

                //  _uniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "GetAll", CreatedTime = DateTime.Now });
                var course = _enroll.GetByid(id);
                var total = _course.CalculateTotal();
                var Reff = Guid.NewGuid().ToString();
                _ref = "#" + Reff;
                var AddCourseFess = new CourseDues 
                {
                 id=DTO.id,
                 CourseDue=total.ToString(),
                 Ref=_ref,
                 IsPaid=DTO.IsPaid
                };
                var financeDTO = new CourseDues
                {
                  id=DTO.id,
                  CourseDue=total.ToString(),
                  cstid=DTO.cstid,
                  Ref=_ref,
                  IsPaid=false
                    // IsGraduated = null, // this property is not set in the Student Portal, so set it to null
                    // BaseClass = DTO.BaseClass
                };

                // Send request to finance portal
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7007/"); // replace with the correct base URL of the finance portal
                var requestUri = "api/Auth/GetCourseDues";
                var requestBody = new StringContent(JsonConvert.SerializeObject(financeDTO), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, requestBody);

                // Check response status
                var content = await response.Content.ReadAsStringAsync();
                var financeResponse = JsonConvert.DeserializeObject<ActiveResponse<AddCourseFee>>(content);
                


              
             
                _coursedues.AddCourseDues(AddCourseFess);
                
                _coursedues.Save();
                if (response.IsSuccessStatusCode)
                {
                    return await helper.Response("succ-001", Level.Success, _ref, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);


                }

                return await helper.Response("succ-001", Level.Success, id, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.baseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }







        }
        [HttpPost]
        [Route("PayCourseFee")]
        [ProducesResponseType(typeof(ActiveResponse<List<Courses>>), 200)]

        public async Task<IActionResult> PayCourseFees([FromBody] AddCourseFee DTO)
        {
            var id = "";
            DateTime startTime = DateTime.Now;
            var _ref = "";
            try
            {
                id = DTO.id;
                if (!TryValidateModel(DTO))
                {
                    if (!ModelState.IsValid)
                    {
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.BadRequest, null, false);
                    }

                }

                //  _uniqueId.Add(new DbHandler.Model.UniqueID { Id = DTO.UniqueId, Route = "GetAll", CreatedTime = DateTime.Now });
                var course = _enroll.GetByid(id);
                var total = _course.CalculateTotal();
                var _getInfo = _coursedues.GetByid(id);
                if (_getInfo != null)
                {
                    //   var Reff = Guid.NewGuid().ToString();
                    //  _ref = "#" + Reff;
                    var AddCourseFess = new CourseDues
                    {
                        id = DTO.id,
                        CourseDue = total.ToString(),
                        cstid = DTO.cstid,
                        Ref = _ref,
                        IsPaid = DTO.IsPaid
                    };
                    var financeDTO = new CourseDues
                    {
                        id = DTO.id,
                        CourseDue = total.ToString(),
                        cstid = DTO.cstid,
                        Ref = DTO.Ref,
                        IsPaid = false
                        // IsGraduated = null, // this property is not set in the Student Portal, so set it to null
                        // BaseClass = DTO.BaseClass
                    };

                    // Send request to finance portal
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7007/"); // replace with the correct base URL of the finance portal
                    var requestUri = "api/Auth/GetCourseDues";
                    var requestBody = new StringContent(JsonConvert.SerializeObject(financeDTO), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(requestUri, requestBody);

                    // Check response status
                    var content = await response.Content.ReadAsStringAsync();
                    var financeResponse = JsonConvert.DeserializeObject<ActiveResponse<AddCourseFee>>(content);





                    _coursedues.UpdateCourseDues(AddCourseFess);

                    _coursedues.Save();
                    if (response.IsSuccessStatusCode)
                    {
                        return await helper.Response("succ-001", Level.Success, id, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);


                    }
                }

                return await helper.Response("succ-001", Level.Success, id, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.baseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }







        }
        [HttpPost]
        [Route("GettReference")]
        [ProducesResponseType(typeof(ActiveResponse<List<Courses>>), 200)]

        public async Task<IActionResult> GetRef([FromBody] AddCourseFee DTO)
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
                        return await helper.Response("err-Model", Level.Success, helper.GetErrors(ModelState), ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.BadRequest, null, false);
                    }

                }

                var getRef = _coursedues.GetByid(id);

              


                return await helper.Response("succ-001", Level.Success, getRef, ActiveErrorCode.Success, startTime, _logs, HttpContext, _config, DTO.baseClass, DTO, "", ReturnResponse.Success, null, false);

            }
            catch (Exception ex)
            {
                return await helper.Response("ex-0003", Level.Error, null, ActiveErrorCode.Failed, startTime, _logs, HttpContext, _config, DTO?.baseClass, DTO, "", ReturnResponse.BadRequest, ex, false);


            }







        }




    }
}
