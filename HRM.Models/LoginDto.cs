﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class LoginDto
    {
        public Guid UserName { get; set;}
        public string Password { get; set;}
    }
    public class ResetPasswordDto
    {
        public Guid UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
