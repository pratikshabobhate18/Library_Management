using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_Portal.Models
{
    public class userModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }


    }
}