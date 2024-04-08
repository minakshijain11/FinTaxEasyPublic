using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTaxEasy_Services.Account
{
    class ChangePasswordRequest
    {
        public string Email { get; set; }
        public bool ValidateRequest { get; set; }
        public string NewPasssword { get; set; }
        public string oldPassword { get; set; }

        public ChangePasswordRequest(string email, bool validateRequest,
            string newPassword, string oldPassword="")
        {
            this.Email = email;
            this.ValidateRequest = validateRequest;
            this.NewPasssword = newPassword;
            this.oldPassword = oldPassword;
        }
    }
}
