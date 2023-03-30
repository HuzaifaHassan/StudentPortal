using DbHandler.Model;
using DbHandler.Repositories;
using StudentPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static StudentPortal.DTO.DTOS;
using static StudentPortal.Responses.ResponseDTO;
using System.IO;
using System.Net;



namespace StudentPortal.Helper
{
    public class APIHelper : ControllerBase
    {
        private readonly IStudentRepository _student;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;



        public APIHelper(IStudentRepository student, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _student = student;
            _userManager = userManager;
            _config = config;
        }
        public DateTime roundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }

        public async new Task<IActionResult> Response(string msg, Level _lvl, object _obj,ActiveErrorCode _code, DateTime StartTime, ILogRepo )
    }
}
