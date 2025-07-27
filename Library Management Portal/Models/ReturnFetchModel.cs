using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_Portal.Models
{
    public class ReturnFetchModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
    }

}