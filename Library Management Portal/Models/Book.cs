using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_Portal.Models
{
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Stock { get; set; }
        public int totalCopies { get; set; }
        public string Category { get; set; }

    }
}