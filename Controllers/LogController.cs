using SchoolBookManagementRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    ViewBag.Message = "Successfully Login";
                   return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Please Enter Correct Username and Password";
                }
            }
            else
            {
                return RedirectToAction("LogInGet", "Log");
            }
            return RedirectToAction("LogInGet", "Log");
        }
        public ActionResult LogOut()
        {
            return RedirectToAction("LogInGet");
        }
    }
}