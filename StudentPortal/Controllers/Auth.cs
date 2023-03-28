using System;
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

        public Auth(ApplicationDbContext ctx, IStudentRepository studentRep, ICourseRepository courserep, IEnrollCourse enroll,SignInManager<ApplicationUser> signInManager,
                     UserManager<ApplicationUser> userManager, IUniqueIdRepository uniqueId)
        {
            _ctx = ctx;
            _studentRep = studentRep;
            _courserep = courserep;
            _enroll = enroll;
            _signInManager = signInManager;
            _userManager = userManager;
            _UniqueId = uniqueId;
        }

    }
}
