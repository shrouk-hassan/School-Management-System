using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public partial class WorkExperience
    {
        [Key]
        public int IDExp { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FromYear { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ToYear { get; set; }
        public string Description { get; set; }
        public Nullable<int> IDPers { get; set; }
        public virtual Person Person { get; set; }
    }
}