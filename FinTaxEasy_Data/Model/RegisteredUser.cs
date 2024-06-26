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
    
    public partial class RegisteredUser
    {
        public RegisteredUser()
        {
            this.Addresses = new HashSet<Address>();
            this.UserDetails = new HashSet<UserDetail>();
            this.UserDocuments = new HashSet<UserDocument>();
            this.Roles = new HashSet<Role>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public Nullable<System.DateTime> ActivationDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public Nullable<System.DateTime> LastLoginDateTime { get; set; }
        public string LastLoginIpAddress { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public Nullable<bool> IsSystemUser { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserDocument> UserDocuments { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
