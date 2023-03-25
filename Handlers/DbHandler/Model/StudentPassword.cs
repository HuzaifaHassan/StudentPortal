using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbHandler.Model
{
    [Table("t_StudentPassword")]
    public  class StudentPassword
    {
        [Key]
        public int Id { get; set; } 
        public string studentId { get; set; }
        public string passwordHash { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreateTerminal { get; set; }

    }
}
