using FinTaxEasy_Data.Model;
using FinTaxEasy_Services.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Net;
using System.Data;
using FinTaxEasy_Services.enums;
using FinTaxEasy_Services.vos;

namespace FinTaxEasy_Services.Account
{
    public partial class CustomerService : ICustomerService
    {
        #region Fields

        private readonly FinTaxEasy_Data.Model.FinTaxEasyDBEntities dbContext;
        private readonly IEncryptionService _encryptionService;

        private FinTaxEasy_Data.Model.RegisteredUser _cachedCustomer;


        #endregion

        #region Ctor
        public CustomerService()
        {
            this.dbContext = new FinTaxEasy_Data.Model.FinTaxEasyDBEntities();
            this._encryptionService = new common.EncryptionService();
        }
        #endregion

        #region Customer
        public virtual List<RegisteredUser> GetAllCustomers()
        {
            var query = dbContext.RegisteredUsers.Where(k => k.IsDeleted == false && k.IsActive == true);
            return query.ToList();
        }


        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A customer</returns>
        public virtual RegisteredUser GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            return dbContext.RegisteredUsers.FirstOrDefault(k => k.UserID == customerId);

        }


        ///<summary>
        ///Delete a customer
        ///</Summary>
        public virtual void DeleteCustomer(RegisteredUser customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            customer.IsDeleted = true;
            customer.IsActive = false;

            customer.DeletedDate = DateTime.UtcNow;

            dbContext.RegisteredUsers.Remove(customer);
            dbContext.SaveChanges();
        }




        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        public virtual RegisteredUser GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in dbContext.RegisteredUsers
                        where c.Email == email
                        orderby c.UserID
                        select c;
            return query.FirstOrDefault();

        }


        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        public virtual RegisteredUser GetCustomerByUserName(string username)
        {
            Console.WriteLine("DB connection " + dbContext.Database.Connection.State);
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in dbContext.RegisteredUsers
                        where c.UserName == username
                        orderby c.UserID
                        select c;
            return query.FirstOrDefault();

        }



        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void InsertCustomer(RegisteredUser customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            customer.Password = _encryptionService.EncryptText(customer.Password);

            dbContext.RegisteredUsers.Add(customer);
            dbContext.SaveChanges();
        }



        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void UpdateCustomer(RegisteredUser customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var registeredUser = dbContext.RegisteredUsers.FirstOrDefault(user => user.UserID == customer.UserID);
            registeredUser.LastLoginDateTime = customer.LastLoginDateTime;
            registeredUser.LastLoginIpAddress = customer.LastLoginIpAddress;

            dbContext.SaveChanges();
        }




        public virtual void ChangePassWord(RegisteredUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var rUser = dbContext.RegisteredUsers.FirstOrDefault(c => c.UserName == user.UserName);
            rUser.Password = user.Password;
            dbContext.SaveChanges();
        }
        
       
        #endregion


        #region Customer roles

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer role collection</returns>
        public virtual List<Role> GetAllCustomerRoles(bool showHidden = false)
        {

            var query = from cr in dbContext.Roles
                        orderby cr.RoleName
                        where (showHidden || cr.IsActive == true)
                        select cr;
            var customerRoles = query.ToList();
            return customerRoles;

        }

        public void InsertCustomerRole(RegisteredUser Role)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region method

        //<summary>
        //Validate customer
        //</summary>
        //<param name="usernameOrEmail"></param>
        //<param name="password">Password</param>
        //<return>Result</return>
        public virtual LoginResultModel ValidateCustomer(string userName, string password)
        {
            LoginResultModel loginResult = new LoginResultModel();
            if (IsValidEmail(userName))
                loginResult.registeredUser = GetCustomerByEmail(userName);
            else
                loginResult.registeredUser = GetCustomerByUserName(userName);


            if (loginResult.registeredUser == null)
                loginResult.customerLoginResult = CustomerLoginResults.CustomerNotExist;
            if (loginResult.registeredUser.IsDeleted == true)
                loginResult.customerLoginResult = CustomerLoginResults.Deleted;
            if (loginResult.registeredUser.IsActive == false || loginResult.registeredUser.IsActive == null)
                loginResult.customerLoginResult = CustomerLoginResults.NotActive;
            string pwd = _encryptionService.EncryptText(password);
            bool isValid = pwd == loginResult.registeredUser.Password;
            if (!isValid)
                loginResult.customerLoginResult = CustomerLoginResults.WrongPassword;
            else
             loginResult.customerLoginResult = CustomerLoginResults.Successful;

            return loginResult;
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421   
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }


        public virtual void SignIn(FinTaxEasy_Data.Model.RegisteredUser customer, bool createPersistentCookie)
        {
            var now = getIndianDateTime(DateTime.UtcNow);
            var nowexpir = now.AddDays(1);
            var ticket = new FormsAuthenticationTicket(
                1,
                customer.Email,
                now,
                nowexpir,
                true,
               customer.Password,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;

            cookie.Expires = ticket.Expiration;

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
            _cachedCustomer = customer;
        }


        public DateTime getIndianDateTime(DateTime dt)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime time2 = TimeZoneInfo.ConvertTimeFromUtc(dt, tz);
            return time2;
        }

   

        public PasswordChangeResult Resetpassword(RegisteredUser user, string newpassword)
        {
            if (user == null)
                throw new ArgumentNullException("request");

            PasswordChangeResult result = new PasswordChangeResult();

            if (user == null)
            {
                result.AddError("EmailNotFound");
                return result;
            }

            user.Password = _encryptionService.EncryptText(newpassword);

            UpdateCustomer(user);

            return result;
        }

        public virtual void SignOut()
        {
            try
            {
                _cachedCustomer = null;
                FormsAuthentication.SignOut();
            }
            catch { }
        }

        public void InsertUserDetails(UserDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException("detail");

            dbContext.UserDetails.Add(detail);
            dbContext.SaveChanges();


        }


        public void UpdateUserDetails(FinTaxEasy_Data.Model.UserDetail detail)
        {
            if (detail != null)
            {
                dbContext.SaveChanges();
            }
        }

        public void DeleteUserDetails(FinTaxEasy_Data.Model.UserDetail detail)
        {
            if (detail != null)
            {
                dbContext.UserDetails.Remove(detail);
                dbContext.SaveChanges();
            }
        }

        #endregion



    }


}
