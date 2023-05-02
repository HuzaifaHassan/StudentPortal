using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbHandler.Model
{
    [Table("t_EnrollCourses")]
    public class EnrollCourses
    {
        [Key]
        public int Id { get; set; }
        public string stId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

        public string fees { get; set; }
        public bool IsActive { get; set; }
    }
}
