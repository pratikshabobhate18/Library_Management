using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_Portal.Models
{
    public class forgotPasswordModel
    {
            public string Username { get; set; }
            public string SecurityQuestion { get; set; }
            public string SecurityAnswer { get; set; }
            public string NewPassword { get; set; }
   
    }
}