using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class EducationVM
    {
        public int IDEdu { get; set; }
        [Required(ErrorMessage = "Please Enter your Institute Of University")]
        public string InstituteUniversity { get; set; }
        [Required(ErrorMessage = "Please Enter your Title Of Diploma")]
        public string TitleOfDiploma { get; set; }
        [Required(ErrorMessage = "Please Enter your Degree")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Please Enter Start Year")]
        public Nullable<System.DateTime> FromYear { get; set; }
        [Required(ErrorMessage = "Please Enter End Year")]
        public Nullable<System.DateTime> ToYear { get; set; }
        [Required(ErrorMessage = "Please Enter your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter your Country")]
        public string Country { get; set; }
        public List<SelectListItem> ListOfCountry { get; set; }
        public List<SelectListItem> ListOfCity { get; set; }
    }
}