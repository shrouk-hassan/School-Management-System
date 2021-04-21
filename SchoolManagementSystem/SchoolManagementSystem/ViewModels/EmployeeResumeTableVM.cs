using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class EmployeeResumeTableVM
    {

        public int EmployeeResumeID { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        public string Nationality { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public string EducationalLevel { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public string Address { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public string Tel { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public string Email { get; set; }
        public string Summary { get; set; }
        public string LinkedInProdil { get; set; }
        public string FaceBookProfil { get; set; }
        public string C_CornerProfil { get; set; }
        public string TwitterProfil { get; set; }
        [Required(ErrorMessage = "{0} Field Is Required")]

        public byte[] Profil { get; set; }
        public Nullable<int> EmployeeID { get; set; }

        public List<SelectListItem> ListNationality { get; set; }
        public List<SelectListItem> ListEducationalLevel { get; set; }
    }
}