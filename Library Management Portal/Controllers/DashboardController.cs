using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Library_Management_Portal.Controllers
{
    public class DashboardController : Controller
    {

		private readonly string connStr;
		public DashboardController()
        {
			connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
		}

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetBookIssues()
        {
            //var result = new List<object>();
            var result = new List<object>();

            //  string connectionString = Configuration.GetConnectionString("DefaultConnection");

            string query = @"
        SELECT 
            CONVERT(date, IssueDate) AS IssueDate,
            COUNT(*) AS IssueCount
        FROM BookIssues
        WHERE IssueDate >= DATEADD(day, -6, GETDATE())
        GROUP BY CONVERT(date, IssueDate)
        ORDER BY IssueDate ASC";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new
                    {
                        date = Convert.ToDateTime(reader["IssueDate"]).ToString("yyyy-MM-dd"),
                        count = Convert.ToInt32(reader["IssueCount"])
                    });
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
            


        }

        [HttpGet]
        public ActionResult GetCategoryBreakdown()
        {
            var result = new List<object>();

            //  string connectionString = Configuration.GetConnectionString("DefaultConnection");

            string query = @"
       SELECT 
    isnull(Category,'Other') As Category ,
    COUNT(*) AS IssueCount
FROM 
    BookIssues as I
    left outer join 
    bookmaster as M
    on I.BookId=m.BookID

GROUP BY 
    Category
ORDER BY 
    IssueCount DESC;";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new
                    {
                        category = reader["Category"].ToString(),
                        count = Convert.ToInt32(reader["IssueCount"])
                    });
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }



    }
}