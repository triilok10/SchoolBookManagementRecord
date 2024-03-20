using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolBookManagementRecord.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult Accounts() 
        { 
            return View();        
        }
        public ActionResult Students() 
        { 
            return View();
        }
        public ActionResult Teachers() 
        { 
            return View();
        }
        public ActionResult Notices()
        {
            return View();
        }
        public ActionResult Attendance() 
        { 
            return View();
        }
        public ActionResult Classes() 
        { 
            return View();
        }
        public ActionResult Transport()
        {
            return View();
        }
        public ActionResult Departments() 
        {
            return View();
        }
    }
}