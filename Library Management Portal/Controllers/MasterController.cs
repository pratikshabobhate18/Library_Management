using Library_Management_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_Portal.Controllers
{
    public class MasterController : Controller
    {
       
        public  ActionResult Books()
        {

                return View();
                //return RedirectToAction("Booklist");
          //  }
            
        }
        

       
        [HttpGet]
        public ActionResult BookList()
        {


            return View("BookList");
        }

        //[HttpGet]
        public ActionResult Update(int id)
        {
            ViewData["Id"] = id;
            return View();

        }
        public ActionResult MemberList()
        {
            return View("MemberList");
        }
        public ActionResult AddMember()
        {
            return View();
        }



        public ActionResult UpdateMember(int Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

    }
}