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
    
    public partial class ClassSubjectTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassSubjectTable()
        {
            this.ExamMarksTables = new HashSet<ExamMarksTable>();
            this.TimeTblTables = new HashSet<TimeTblTable>();
        }
    
        public int ClassSubjectID { get; set; }
        public int ClassID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> cat_id { get; set; }
    
        public virtual tbl_category tbl_category { get; set; }
        public virtual ClassTable ClassTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamMarksTable> ExamMarksTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTblTable> TimeTblTables { get; set; }
    }
}