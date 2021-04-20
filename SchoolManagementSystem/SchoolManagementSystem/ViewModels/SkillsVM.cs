using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class SkillsVM
    {
        [Required(ErrorMessage = "Please Enter Your Skill Name")]
        public string SkillName { get; set; }
    }
}