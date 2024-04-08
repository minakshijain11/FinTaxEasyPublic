using FinTaxEasy_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTaxEasy_Services.Account
{
    class CustomerRegistrationRequest
    {

        public RegisteredUser Customer { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        
        public CustomerRegistrationRequest(RegisteredUser customer, string email, string username, string password, bool isApproved = true )
        {
            this.Customer = customer;
            this.UserName = username;
            this.Email = email;
            this.Password = password;
            this.IsApproved = isApproved;
        }
    }
}
