using SchoolBookManagementRecord.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolBookManagementRecord.Controllers
{
    public class StudentUtilityController : Controller
    {
        private string CS = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        #region "View Students"
        // GET: StudentUtility
        [HttpGet]
        public ActionResult ViewStudent()
        {
            List<Student> ltrStudents = new List<Student>();
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

                            objStudent.Id = Convert.ToInt32(rdr["ID"]);
                            objStudent.FirstName = Convert.ToString(rdr["FirstName"]);
                            objStudent.LastName = Convert.ToString(rdr["LastName"]);
                            objStudent.FatherName = Convert.ToString(rdr["FatherName"]);
                            objStudent.MotherName = Convert.ToString(rdr["MotherName"]);
                            objStudent.Address = Convert.ToString(rdr["Address"]);
                            objStudent.Remarks = Convert.ToString(rdr["Remark"]);
                            objStudent.Mobile = Convert.ToString(rdr["Mobile"]);
                            objStudent.Filepath = Convert.ToString(rdr["Filepath"]);

                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objStudent.Gender = gender;
                            }
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objStudent.Class = classname;
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
            return View(ltrStudents);
        }

        #endregion

        #region "Update  View GET"
        public ActionResult UpdateStudent()
        {
            List<Student> ltrStudents = new List<Student>();
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

                            objStudent.Id = Convert.ToInt32(rdr["ID"]);
                            objStudent.FirstName = Convert.ToString(rdr["FirstName"]);
                            objStudent.LastName = Convert.ToString(rdr["LastName"]);
                            objStudent.FatherName = Convert.ToString(rdr["FatherName"]);
                            objStudent.MotherName = Convert.ToString(rdr["MotherName"]);
                            objStudent.Address = Convert.ToString(rdr["Address"]);
                            objStudent.Remarks = Convert.ToString(rdr["Remark"]);
                            objStudent.Mobile = Convert.ToString(rdr["Mobile"]);
                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objStudent.Gender = gender;
                            }
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objStudent.Class = classname;
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
            return View(ltrStudents);
        }
        #endregion

        #region "Changing the Data before calling update SP here"
        public ActionResult UpdateChangeData(int id)
        {
            try
            {
                Student objStudent = null;
                using (var con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateChangeData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            objStudent = new Student
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                FirstName = Convert.ToString(rdr["FirstName"]),
                                LastName = Convert.ToString(rdr["LastName"]),
                                FatherName = Convert.ToString(rdr["FatherName"]),
                                MotherName = Convert.ToString(rdr["MotherName"]),
                                Gender = (GenderType)Enum.Parse(typeof(GenderType), Convert.ToString(rdr["Gender"])),
                                Address = Convert.ToString(rdr["Address"]),
                                Class = (ClassName)Enum.Parse(typeof(ClassName), Convert.ToString(rdr["Class"])),
                                Remarks = Convert.ToString(rdr["Remark"]),
                                Email = Convert.ToString(rdr["Email"]),
                                Mobile = Convert.ToString(rdr["Mobile"])
                            };
                        }
                    }

                }
                if (objStudent == null)
                {
                    return RedirectToAction("ViewStudent");
                }
                return View(objStudent);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion 

        #region"Update Student Post"
        [HttpPost]
        public ActionResult UpdateStudentData(Student std, HttpPostedFileBase File)
        {
            try
            {
                if (File != null && File.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(File.FileName);
                    string FilePathData = Path.Combine(Server.MapPath("~/App_Data/UserImages/"), FileName);
                    File.SaveAs(FilePathData);
                    std.Filepath = FileName;
                }
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("UpdateStudentData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Filepath", std.Filepath);
                    cmd.Parameters.AddWithValue("@FirstName", std.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", std.LastName);
                    cmd.Parameters.AddWithValue("@Class", std.Class);
                    cmd.Parameters.AddWithValue("@Gender", std.Gender);
                    cmd.Parameters.AddWithValue("@FatherName", std.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", std.MotherName);
                    cmd.Parameters.AddWithValue("@Address", std.Address);
                    cmd.Parameters.AddWithValue("@Remark", std.Remarks);
                    cmd.Parameters.AddWithValue("@Email", std.Email);
                    cmd.Parameters.AddWithValue("@Mobile", std.Mobile);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    TempData["Message"] = "User Record " + std.FirstName + " updated successfully!";
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("ViewStudent");
        }
        #endregion

        #region "Create Student POST"
        [HttpPost]
        public ActionResult CreateStudentData(Student pStudent, HttpPostedFileBase File)
        {

           
            try
            {
                if (File != null && File.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(File.FileName);
                    string FilePathData = Path.Combine(Server.MapPath("~/App_Data/UserImages/" ), FileName);
                    File.SaveAs(FilePathData);
                    pStudent.Filepath = FileName;
                }
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("AddStudentMain", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Filepath", pStudent.Filepath);
                    cmd.Parameters.AddWithValue("@FirstName", pStudent.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", pStudent.LastName);
                    cmd.Parameters.AddWithValue("@Class", pStudent.Class);
                    cmd.Parameters.AddWithValue("@Gender", pStudent.Gender);
                    cmd.Parameters.AddWithValue("@FatherName", pStudent.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", pStudent.MotherName);
                    cmd.Parameters.AddWithValue("@Address", pStudent.Address);
                    cmd.Parameters.AddWithValue("@Remark", pStudent.Remarks);
                    cmd.Parameters.AddWithValue("@Email", pStudent.Email);
                    cmd.Parameters.AddWithValue("@Mobile", pStudent.Mobile);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    TempData["Message"] = "User Record " + pStudent.FirstName + " successfully saved!";
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("ViewStudent");

        }
        #endregion


        #region "Add Student"
        public ActionResult AddStudent()
        {
            return View();
        }
        #endregion

        #region "Delete Student"
        public ActionResult DeleteStudent()
        {
            List<Student> ltrStudents = new List<Student>();
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

                            objStudent.Id = Convert.ToInt32(rdr["ID"]);
                            objStudent.FirstName = Convert.ToString(rdr["FirstName"]);
                            objStudent.LastName = Convert.ToString(rdr["LastName"]);
                            objStudent.Remarks = Convert.ToString(rdr["Remark"]);
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objStudent.Class = classname;
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
            return View(ltrStudents);
        }
        public ActionResult DeleteStudentData(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("DeleteStudentByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    TempData["Message"] = "User Record " + id + " is Deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return RedirectToAction("ViewStudent");
        }
        #endregion
    }
}