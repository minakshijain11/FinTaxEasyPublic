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
    
    public partial class City
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }
    
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual State State { get; set; }
    }
}
