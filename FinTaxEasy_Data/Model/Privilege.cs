//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinTaxEasy_Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Privilege
    {
        public Privilege()
        {
            this.UserPrivileges = new HashSet<UserPrivilege>();
        }
    
        public int PrivilegeID { get; set; }
        public string PrivilegeName { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> AllowDelete { get; set; }
        public Nullable<bool> AllowUpdate { get; set; }
        public Nullable<bool> ReadOnly { get; set; }
    
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }
    }
}
