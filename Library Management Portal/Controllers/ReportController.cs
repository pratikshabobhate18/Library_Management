using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Library_Management_Portal.Models;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Library_Management_Portal.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        private readonly string connStr;
        public ReportController()
        {
            connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }
        public ActionResult Index()
        {
            List<Reports> reports = new List<Reports>();

            //  string connectionString = Configuration.GetConnectionString("DefaultConnection");

            string query = @"
              SELECT 
   FullName,Title
FROM 
    BookIssues as I
    left outer join 
    MemberMaster as M
     on I.MemberId=m.MemberID
    left outer join BookMaster B
    on i.BookId=b.BookID
   
WHERE 
    IssueDate >= DATEADD(DAY, -7, GETDATE()) ";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Reports report = new Reports
                    {
                        Name = reader["FullName"].ToString(),
                        Title = reader["Title"].ToString()

                    };
                   reports.Add(report);
                }

                return View(reports);
            }
        }
        //public ActionResult ExportToExcel()
        //{
        //    var workbook = new XLWorkbook();
        //    var worksheet = workbook.Worksheets.Add("Report");
        //    worksheet.Cell(1, 1).Value = "ID";
        //    worksheet.Cell(1, 2).Value = "Name";
          

        //    worksheet.Cell(2, 1).Value = 1;
        //    worksheet.Cell(2, 2).Value = "Ravi";
        //    //worksheet.Cell(2, 3).Value = "A";

        //    using (var stream = new MemoryStream())
        //    {
        //        workbook.SaveAs(stream);
        //        return File(stream.ToArray(),
        //            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //            "Report.xlsx");
        //    }
        //}
        ////public ActionResult GenerateStudentReport()
        ////{
        ////    // Sample data
        ////    List<Student> students = new List<Student>
        ////{
        ////    new Student { ID = 1, Name = "Ravi", Grade = "A" },
        ////    new Student { ID = 2, Name = "Meera", Grade = "B" }
        ////};

        ////    // Convert to DataTable
        ////    DataTable dt = new DataTable("Student");
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("Grade");
        //    foreach (var s in students)
        //    {
        //        dt.Rows.Add(s.ID, s.Name, s.Grade);
        //    }

        //    // Setup RDLC Report
        //    LocalReport lr = new LocalReport();
        //    lr.ReportPath = Server.MapPath(""); // Your RDLC file

        //    ReportDataSource rd = new ReportDataSource("StudentDataSet", dt);
        //    lr.DataSources.Add(rd);

        //    // Render report to PDF
        //    string mimeType, encoding, fileNameExtension;
        //    string[] streams;
        //    Warning[] warnings;
        //    byte[] renderedBytes = lr.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return File(renderedBytes, mimeType, "StudentReport.pdf");
        //}
    }

}
