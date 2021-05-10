using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public partial class Certification
    {
        [Key]
        public int IdCar { get; set; }
        public string CertificationName { get; set; }
        public string CertificationAuthority { get; set; }
        public string LevelCertification { get; set; }
        public Nullable<System.DateTime> FromYear { get; set; }
        public Nullable<int> IdPers { get; set; }
        public virtual Person Person { get; set; }
    }
}