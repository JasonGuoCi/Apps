//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SysUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysUser()
        {
            this.SysRole = new HashSet<SysRole>();
            this.MIS_Article = new HashSet<MIS_Article>();
            this.MIS_Article1 = new HashSet<MIS_Article>();
        }
    
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TrueName { get; set; }
        public string Card { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string QQ { get; set; }
        public string EmailAddress { get; set; }
        public string OtherContact { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
        public string Address { get; set; }
        public Nullable<bool> State { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public string Marital { get; set; }
        public string Political { get; set; }
        public string Nationality { get; set; }
        public string Native { get; set; }
        public string School { get; set; }
        public string Professional { get; set; }
        public string Degree { get; set; }
        public string DepId { get; set; }
        public string PosId { get; set; }
        public string Expertise { get; set; }
        public string JobState { get; set; }
        public string Photo { get; set; }
        public string Attach { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRole> SysRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MIS_Article> MIS_Article { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MIS_Article> MIS_Article1 { get; set; }
    }
}
