using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Model
{
    [Table("t_CDues")]
    public class CourseDues
    {
        [Key]
        public string id { get; set; }
        public string cstid { get; set; }
        public string? CourseDue { get; set; }
        
        public string Ref { get; set; }
        public bool IsPaid { get; set; }
    }
}
