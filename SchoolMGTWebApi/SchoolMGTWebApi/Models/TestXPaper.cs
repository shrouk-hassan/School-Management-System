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
    
    public partial class TestXPaper
    {
        public int Id { get; set; }
        public int TestXQuestionId { get; set; }
        public int ChoiceId { get; set; }
        public string Answer { get; set; }
        public Nullable<decimal> MarkScored { get; set; }
        public Nullable<int> RegistrationId { get; set; }
    
        public virtual Choice Choice { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual TestXQuestion TestXQuestion { get; set; }
    }
}
