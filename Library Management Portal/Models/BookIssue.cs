using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_Portal.Models
{
    public class BookIssue
    {           
            public int IssueId { get; set; }

            //[Required]
            public int BookId { get; set; }

           // [Required]
            public int MemberId { get; set; }

            //[Required]
           // [DataType(DataType.Date)]
            public DateTime IssueDate { get; set; }

           // //[Required]
           // //[DataType(DataType.Date)]
            public DateTime DueDate { get; set; }

           //// [DataType(DataType.Date)]
           // public DateTime? ReturnDate { get; set; }

           // [Range(0, double.MaxValue)]
            public decimal? FineAmount { get; set; }
        }
    }

