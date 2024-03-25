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
                            byte[] photodata = (byte[])rdr["PhotoStore"];
                            string base64StringPhoto = Convert.ToBase64String(photodata);

                         //   objStudent.PhotoBase64 = base64StringPhoto;

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

        #region "Update Student"
        public ActionResult UpdateStudent()
        {
            return View();
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
                    byte[] photoBytes = null;
                    //if (pStudent.PhotoFile != null && pStudent.PhotoFile.ContentLength > 0)
                    //{
                    //    using (var binaryReader = new BinaryReader(pStudent.PhotoFile.InputStream))
                    //    {
                    //        photoBytes = binaryReader.ReadBytes(pStudent.PhotoFile.ContentLength);
                    //    }
                    //}
                    //cmd.Parameters.AddWithValue("@PhotoStore", pStudent.photoBytes);
                    //cmd.Parameters.AddWithValue("@FirstName", pStudent.FirstName);
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
            catch(Exception ex) 
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
            return View();
        }
        #endregion
    }
}