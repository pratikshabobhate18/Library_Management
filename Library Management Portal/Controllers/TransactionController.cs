using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_Portal.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult IssueBook()
        {
            return View();
        }

        public ActionResult ReturnBook(int id)
        {
            ViewData["Id"] = id;
            return View();
        }
        public ActionResult AddMember()
        {
            return View();
        }
        public ActionResult ShowBookTrans()
        {
            return View();
        }
    }
}