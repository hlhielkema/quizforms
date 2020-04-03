using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizForms.Data.Repositories.Abstract
{
    /// <summary>
    /// Accounts repository
    /// </summary>
    public interface IAccountsRepository
    {
        /// <summary>
        /// Get if an account exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>
        ///     true = account exists;
        ///     false = account does not exist
        /// </returns>
        Task<bool> Exists(string username);
        
        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>list with username</returns>
        Task<List<string>> GetAccounts();

        /// <summary>
        /// Create an new account
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">account password</param>
        /// <returns>
        ///     true = account created;
        ///     false = an account with the same username already exists
        /// </returns>
        Task<bool> CreateAccount(string username, string password);

        /// <summary>
        /// Validate account credentials
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">account password</param>
        /// <returns>
        ///     true = credentials are valid;
        ///     false = the username or password is incorrect
        /// </returns>
        Task<bool> ValidateCredentials(string username, string password);

        /// <summary>
        /// Reset the password for an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">new account password</param>
        /// <returns>
        ///     true = the account was found and the password has been updated;
        ///     false = account not found
        /// </returns>
        Task<bool> ResetPassword(string username, string password);

        /// <summary>
        /// Delete an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <returns>
        ///     true = the account was found and deleted;
        ///     false = account not found
        /// </returns>
        Task<bool> DeleteAccount(string username);

        /// <summary>
        /// Create a claim princiapl for an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <returns>claim principal</returns>
        ClaimsPrincipal CreateClaimPrincipal(string username);
    }
}
