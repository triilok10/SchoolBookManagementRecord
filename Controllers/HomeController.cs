using SchoolBookManagementRecord.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolBookManagementRecord.Controllers
{

    public class HomeController : StudentUtilityController
    {
        private string CS = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
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
            List<Student> ltrStudents = new List<Student>();
            int maleCount = 0;
            int femaleCount = 0;
            int otherCount = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("AddViewStudents", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objStudent = new Student();

                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objStudent.Gender = gender;
                                if (gender == GenderType.Male)
                                {
                                    maleCount++;
                                }
                                else if (gender == GenderType.Female)
                                {
                                    femaleCount++;
                                }
                                else if (gender == GenderType.Other)
                                {
                                    otherCount++;
                                }
                            }

                            ltrStudents.Add(objStudent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            ViewBag.MaleCount = maleCount;
            ViewBag.FemaleCount = femaleCount;
            ViewBag.OtherCount = otherCount;
            return View(ltrStudents);
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
        public ActionResult Departments()
        {
            return View();
        }
    }
}