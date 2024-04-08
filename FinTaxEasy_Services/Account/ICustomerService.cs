using FinTaxEasy_Data.Model;
using FinTaxEasy_Services.enums;
using FinTaxEasy_Services.vos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinTaxEasy_Services.Account
{
    public partial interface ICustomerService
    {
        #region Customers

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void DeleteCustomer(FinTaxEasy_Data.Model.RegisteredUser customer);

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A customer</returns>
        FinTaxEasy_Data.Model.RegisteredUser GetCustomerById(int customerId);

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        FinTaxEasy_Data.Model.RegisteredUser GetCustomerByUserName(string username);

        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        FinTaxEasy_Data.Model.RegisteredUser GetCustomerByEmail(string email);


        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void InsertCustomer(FinTaxEasy_Data.Model.RegisteredUser customer);


        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void UpdateCustomer(FinTaxEasy_Data.Model.RegisteredUser customer);
     

        #endregion

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        PasswordChangeResult Resetpassword(RegisteredUser user, string newpassword);
        
        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        LoginResultModel ValidateCustomer(string usernameorEmail, string password);
        void SignOut();
        // FinTaxEasy_Data.Model.RegisteredUser GetAunthenticatedCustomer();


        ///// <summary>
        ///// Change password
        ///// </summary>
        ///// <param name="request">Request</param>
        ///// <returns>Result</returns>
        //PasswordChangeResult ChangePassword(ChangePasswordRequest request);


        ///// <summary>
        ///// Change password
        ///// </summary>
        ///// <param name="request">Request</param>
        ///// <returns>Result</returns>
        //PasswordChangeResult Resetpassword(FinTaxEasy_Data.Model.RegisteredUser user, string newpassword);
        void ChangePassWord(FinTaxEasy_Data.Model.RegisteredUser user);
        void SignIn(FinTaxEasy_Data.Model.RegisteredUser customer, bool createPersistentCookie);

        void InsertUserDetails(FinTaxEasy_Data.Model.UserDetail detail);
        void UpdateUserDetails(FinTaxEasy_Data.Model.UserDetail detail);
        void DeleteUserDetails(FinTaxEasy_Data.Model.UserDetail detial);
    }
}
