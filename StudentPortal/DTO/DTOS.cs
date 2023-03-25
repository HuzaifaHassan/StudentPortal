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



    }
}
