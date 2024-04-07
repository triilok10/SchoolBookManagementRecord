using SchoolBookManagementRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SchoolBookManagementRecord.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
       public ActionResult LogInGet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Log pLog)
        {
            if (pLog.Username != null && pLog.Password!=null)
            {
                if (pLog.Username == "Trilok" && pLog.Password == "Trilok8058")
                {
                    TempData["Message"] = "Successfully Logged Out!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Please Enter Correct Username and Password";
                    return RedirectToAction("LogInGet", "Log");
                }
            }
            else
            {
                TempData["Message"] = "Invalid User, Please Check Your Details.";
                return RedirectToAction("LogInGet", "Log");
            }
        }
        public ActionResult LogOut()
        {
            return RedirectToAction("LogInGet");
        }
    }
}