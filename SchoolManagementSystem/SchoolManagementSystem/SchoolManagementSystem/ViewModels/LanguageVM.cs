using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class LanguageVM
    {
        [Required(ErrorMessage = "Please Enter Language Name")]
        public string LanguageName { get; set; }
        [Required(ErrorMessage = "Please Select Proficiency")]
        public string Proficiency { get; set; }
        public List<SelectListItem> ListOfProficiency { get; set; }
    }
}