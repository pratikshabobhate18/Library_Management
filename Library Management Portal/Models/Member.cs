using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_Portal.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }

    }
}