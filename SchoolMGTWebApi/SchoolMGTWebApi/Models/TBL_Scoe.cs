//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolMGTWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_Scoe
    {
        public int ScoreID { get; set; }
        public Nullable<int> cat_id { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> ObtainMark { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual StudentTable StudentTable { get; set; }
        public virtual tbl_category tbl_category { get; set; }
    }
}