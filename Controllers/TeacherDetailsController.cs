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
    public class TeacherDetailsController : Controller
    {
        private string CS = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        #region "View Students"
        // GET: StudentUtility
        [HttpGet]
        public ActionResult ViewTeachers()
        {
            List<Student> ltrStudents = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("[AddViewTeachers]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objTeacher = new Student();

                            objTeacher.Id = Convert.ToInt32(rdr["ID"]);
                            objTeacher.FirstName = Convert.ToString(rdr["FirstName"]);
                            objTeacher.LastName = Convert.ToString(rdr["LastName"]);
                            objTeacher.FatherName = Convert.ToString(rdr["FathersName"]);
                            objTeacher.MotherName = Convert.ToString(rdr["MotherName"]);
                            objTeacher.Address = Convert.ToString(rdr["Address"]);
                            objTeacher.Remarks = Convert.ToString(rdr["Remarks"]);
                            objTeacher.Mobile = Convert.ToString(rdr["Mobile"]);
                            objTeacher.Filepath = Convert.ToString(rdr["Filepath"]);
                            objTeacher.DateOfBirth = rdr.GetDateTime(rdr.GetOrdinal("DateOfBirth"));
                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objTeacher.Gender = gender;
                            }
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objTeacher.Class = classname;
                            }
                            ltrStudents.Add(objTeacher);
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
                            Student objTeacher = new Student();

                            objTeacher.Id = Convert.ToInt32(rdr["ID"]);
                            objTeacher.FirstName = Convert.ToString(rdr["FirstName"]);
                            objTeacher.LastName = Convert.ToString(rdr["LastName"]);
                            objTeacher.FatherName = Convert.ToString(rdr["FatherName"]);
                            objTeacher.MotherName = Convert.ToString(rdr["MotherName"]);
                            objTeacher.Address = Convert.ToString(rdr["Address"]);
                            objTeacher.Remarks = Convert.ToString(rdr["Remark"]);
                            objTeacher.Mobile = Convert.ToString(rdr["Mobile"]);
                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objTeacher.Gender = gender;
                            }
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objTeacher.Class = classname;
                            }
                            ltrStudents.Add(objTeacher);
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
                Student objTeacher = null;
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
                            objTeacher = new Student
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
                if (objTeacher == null)
                {
                    return RedirectToAction("ViewStudent");
                }
                return View(objTeacher);
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
        public ActionResult CreateTeacherData(Student pTeacher, HttpPostedFileBase File)
        {
            try
            {
                if (File != null && File.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(File.FileName);
                    string FilePathData = Path.Combine(Server.MapPath("~/App_Data/UserImages/"), FileName);
                    File.SaveAs(FilePathData);
                    pTeacher.Filepath = FileName;
                }
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("AddTeacherMain", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Filepath", pTeacher.Filepath);
                    cmd.Parameters.AddWithValue("@FirstName", pTeacher.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(pTeacher.LastName) ? (object)DBNull.Value : pTeacher.LastName);
                    cmd.Parameters.AddWithValue("@Class", pTeacher.Class);
                    cmd.Parameters.AddWithValue("@Gender", pTeacher.Gender);
                    cmd.Parameters.AddWithValue("@FathersName", pTeacher.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", pTeacher.MotherName);
                    cmd.Parameters.AddWithValue("@Address", pTeacher.Address);
                    cmd.Parameters.AddWithValue("@Remark", pTeacher.Remarks);
                    cmd.Parameters.AddWithValue("@Email", pTeacher.Email);
                    cmd.Parameters.AddWithValue("@Mobile", pTeacher.Mobile);
                    cmd.Parameters.AddWithValue("@DateOfBirth", pTeacher.DateOfBirth);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    TempData["Message"] = "User Record " + pTeacher.FirstName + " successfully saved!";
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("ViewTeachers");
        }
        #endregion


        #region "Add Student"
        public ActionResult AddTeacher()
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
                            Student objTeacher = new Student();

                            objTeacher.Id = Convert.ToInt32(rdr["ID"]);
                            objTeacher.FirstName = Convert.ToString(rdr["FirstName"]);
                            objTeacher.LastName = Convert.ToString(rdr["LastName"]);
                            objTeacher.Remarks = Convert.ToString(rdr["Remark"]);
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objTeacher.Class = classname;
                            }
                            ltrStudents.Add(objTeacher);
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

        #region "EDOC"
        public ActionResult EDoc(int Id)
        {
            Student student = null;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("GetEDOC", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            student = new Student();
                            student.Id = Convert.ToInt32(rdr["ID"]);
                            student.FirstName = Convert.ToString(rdr["FirstName"]);
                            student.LastName = Convert.ToString(rdr["LastName"]);
                            student.FatherName = Convert.ToString(rdr["FatherName"]);
                            student.MotherName = Convert.ToString(rdr["MotherName"]);
                            student.Address = Convert.ToString(rdr["Address"]);
                            student.Remarks = Convert.ToString(rdr["Remark"]);
                            student.Mobile = Convert.ToString(rdr["Mobile"]);
                            student.Filepath = Convert.ToString(rdr["Filepath"]);
                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                student.Gender = gender;
                            }
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                student.Class = classname;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(student);
        }
        #endregion


    }


}