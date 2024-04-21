using Newtonsoft.Json;
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

        public JsonResult ChartsViewStudent()
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

                int totalCount = maleCount + femaleCount + otherCount;

                if (totalCount == 0)
                {
                    return Json(new { error = "Total count of students is zero." });
                }

                double malePercentage = (double)maleCount / totalCount * 100;
                double femalePercentage = (double)femaleCount / totalCount * 100;
                double otherPercentage = (double)otherCount / totalCount * 100;

                var result = new
                {
                    malePercentage = malePercentage,
                    femalePercentage = femalePercentage,
                    otherPercentage = otherPercentage
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public JsonResult ChartsViewTeacher()
        {
            List<Student> ltrStudents = new List<Student>();
            int maleCount = 0;
            int femaleCount = 0;
            int otherCount = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("AddViewTeachers", con);
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

                int totalCount = maleCount + femaleCount + otherCount;

                if (totalCount == 0)
                {
                    return Json(new { error = "Total count of teachers is zero." });
                }

                double malePercentage = (double)maleCount / totalCount * 100;
                double femalePercentage = (double)femaleCount / totalCount * 100;
                double otherPercentage = (double)otherCount / totalCount * 100;

                var result = new
                {
                    malePercentage = malePercentage,
                    femalePercentage = femalePercentage,
                    otherPercentage = otherPercentage
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public ActionResult BirthdayTeacher()
        {
            try
            {
                List<Student> upcomingBirthdays = new List<Student>(); // Initialize the list
                using (SqlConnection connection = new SqlConnection(CS))
                {
                    string query = @"
                DECLARE @CurrentDate DATETIME = GETDATE();
                SELECT 
                    FirstName,
                    LastName,
                    DateofBirth
                FROM 
                    TeacherDetails
                WHERE 
                    DATEADD(YEAR, DATEDIFF(YEAR, DateofBirth, @CurrentDate), DateofBirth) BETWEEN @CurrentDate AND DATEADD(DAY, 30, @CurrentDate)
                ORDER BY 
                    DateofBirth;
            ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.DateOfBirth = Convert.ToDateTime(reader["DateofBirth"]);

                        upcomingBirthdays.Add(student);
                    }

                    reader.Close();
                }

                return PartialView("BirthdayTeacherPartial", upcomingBirthdays);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }



    }
}