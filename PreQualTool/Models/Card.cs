//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PreQualTool.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Card
    {
        public Card()
        {
            this.Applicants = new HashSet<Applicant>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string PromoMessage { get; set; }
        public Nullable<double> APR { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}
