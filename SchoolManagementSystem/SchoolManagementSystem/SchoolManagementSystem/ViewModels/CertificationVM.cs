using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class CertificationVM
    {
        [Required(ErrorMessage = "Please Enter your Certification Name")]
        public string CertificationName { get; set; }
        [Required(ErrorMessage = "Please Enter your Certification Authority")]
        public string CertificationAuthority { get; set; }
        [Required(ErrorMessage = "Please Select Certification Level")]
        public string LevelCertification { get; set; }
        [Required(ErrorMessage = "Please Select Achievement Date")]
        public Nullable<System.DateTime> FromYear { get; set; }
        public List<SelectListItem> ListOfLevel { get; set; }
    }
}