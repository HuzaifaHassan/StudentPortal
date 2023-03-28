using DbHandler.Model;
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

        [Required(ErrorMessage ="ID Required")]
        public string UniqueId { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Student Id Required")]
        public string stId { get; set; }
        [Required(ErrorMessage = "Cstudent Id Required")]
        public string cstID { get; set; }




    }
}
