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
    
    public partial class WC_MessageResponse
    {
        public string Id { get; set; }
        public string OfficalAccountId { get; set; }
        public Nullable<int> MessageRule { get; set; }
        public Nullable<int> Category { get; set; }
        public string MatchKey { get; set; }
        public string TextContent { get; set; }
        public string ImgTextContext { get; set; }
        public string ImgTextUrl { get; set; }
        public string ImgTextLink { get; set; }
        public string MeidaUrl { get; set; }
        public string MeidaLink { get; set; }
        public bool Enable { get; set; }
        public bool IsDefault { get; set; }
        public string Remark { get; set; }
        public int Sort { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    
        public virtual WC_OfficalAccounts WC_OfficalAccounts { get; set; }
    }
}