using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTaxEasy_Services.Account
{
    class CustomerRegistrationResult
    {
        public IList<string> Errors { get; set; }

        public CustomerRegistrationResult()
        {
            this.Errors = new List<string>();
        }

        public bool Sucess
        {
            get { return this.Errors.Count == 0; }
        }
        public void AddError(string error)
        {
            this.Errors.Add(error);
        }
    }
}
