using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolBookManagementRecord.Models
{
    public class Student
    {
        internal object photoBytes;

        [Required(ErrorMessage ="Please Enter the SR NO")]
        [MaxLength(4)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter your FirstName")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage ="Please Select your Class")]
        public ClassName Class { get; set; }

        [Required(ErrorMessage ="Please Select your Gender")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage ="Please Enter your Address")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Please Enter your Father's Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage ="Please Enter your PhoneNumber")]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Please Enter your Mother's Name")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Please Enter the Remarks")]
        public string Remarks { get; set; }

       [Required(ErrorMessage = "Please select a file.")]
       public HttpPostedFileBase PhotoFile { get; set; }
        public string PhotoBase64 { get; internal set; }

    }
    public enum ClassName
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eight,
        Nineth,
        Tenth,
        Eleventh,
        Twelveth
    }
    public enum GenderType
    {
        Male,
        Female,
        Other
    }
}