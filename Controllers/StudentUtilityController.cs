using SchoolBookManagementRecord.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                            objStudent.Remarks = Convert.ToString(rdr["Remarks"]);

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

        #region "Update Student GET"
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
                            objStudent.Remarks = Convert.ToString(rdr["Remarks"]);

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
        #region"Update Student Post"

        public ActionResult UpdateStudentData(int id)
        {
            try
            {
                Student objStudent = null;

                using (var con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateStudentData", con);
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
                                Class = (ClassName)Enum.Parse(typeof(ClassName), Convert.ToString(rdr["Class"]))
                            };
                        }
                    }
                }

                if (objStudent == null)
                {
                    return RedirectToAction("ViewStudent");
                }

                return View("UpdateStudentData", objStudent);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
     
        }
        #endregion

        #region "Create Student POST"
        [HttpPost]
        public ActionResult CreateStudentData(Student pStudent)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("AddStudentMain", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", pStudent.Id);
                    cmd.Parameters.AddWithValue("@FirstName", pStudent.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", pStudent.LastName);
                    cmd.Parameters.AddWithValue("@Class", pStudent.Class);
                    cmd.Parameters.AddWithValue("@Gender", pStudent.Gender);
                    cmd.Parameters.AddWithValue("@FatherName", pStudent.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", pStudent.MotherName);
                    cmd.Parameters.AddWithValue("@Address", pStudent.Address);
                    cmd.Parameters.AddWithValue("@Remarks", pStudent.Remarks);
                    con.Open();
                    cmd.ExecuteNonQuery();
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
                            objStudent.Remarks = Convert.ToString(rdr["Remarks"]);
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