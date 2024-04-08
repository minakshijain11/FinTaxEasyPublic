using FinTaxEasy_Data.Model;
using FinTaxEasy_Services.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinTaxEasy_Services.vos
{
    public class LoginResultModel
    {

        public RegisteredUser registeredUser { get; set; }

        public CustomerLoginResults customerLoginResult { get; set; }
    }
}