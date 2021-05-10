using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class PersonVM
    {
        public int IDPers { get; set; }
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Date Of Birth")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Enter Your Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Please Enter Your Educational Level")]
        public string EducationalLevel { get; set; }
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        public string Tel { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
        [Required(ErrorMessage = "Please Enter Your LinkedIn Profile")]
        [DataType(DataType.Url)]
        public string LinkedInProfile { get; set; }
        [Required(ErrorMessage = "Please Enter Your FaceBook Profile")]
        [DataType(DataType.Url)]
        public string FaceBookProfile { get; set; }
        [Required(ErrorMessage = "Please Enter Your C# Corner Profile")]
        [DataType(DataType.Url)]
        public string C_CornerProfile { get; set; }
        [Required(ErrorMessage = "Please Enter Your Twitter Profile")]
        [DataType(DataType.Url)]
        public string TwitterProfile { get; set; }
        public byte[] Profile { get; set; }

        public List<SelectListItem> ListNationality { get; set; }
        public List<SelectListItem> ListEducationalLevel { get; set; }
    }
}