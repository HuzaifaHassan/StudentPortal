﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Model
{
    [Table("T_Users")]
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
         public string Password { get; set; }
        public Guid GUID { get; set; }
    }
}
