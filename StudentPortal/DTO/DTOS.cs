﻿using DbHandler.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace StudentPortal.DTO
{
    public class DTOS
    {

    }
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Last Name is Required")]
        public string LastName { get; set; }

        //[Required(ErrorMessage ="ID Required")]
        public string UniqueId { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Student Id Required")]
        //public string stId { get; set; }
        //[Required(ErrorMessage = "Cstudent Id Required")]
        //public string cstID { get; set; }
        public string MobileNo { get; set; }
         public BaseClass BaseClass { get; set; }


    }
    public class BaseClass
    {
        public string IP { get; set; }

        [Required(ErrorMessage = "dto-0001")]
        public string DeviceID { get; set; }

        [Required(ErrorMessage = "err-0002")]
        public string OnBoardingChannel { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Platform { get; set; }
        public string Browser { get; set; }
    
        


    }
    public class GetAllCourses
    {

        public BaseClass BaseClass{get;set;}
        [Required(ErrorMessage = "UniqueId Required")]
        public string UniqueId { get; set; }
    
    }
    public class AddCourses
    { 
       public string CourseName { get; set; }
        public string CourseCode { get; set; }

        public BaseClass BaseClass { get; set; }
    
    }
}
