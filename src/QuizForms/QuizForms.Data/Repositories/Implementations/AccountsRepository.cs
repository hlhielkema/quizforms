using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Account;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Data.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace QuizForms.Data.Repositories.Implementations
{
    /// <summary>
    /// Accounts repository
    /// </summary>
    public sealed class AccountsRepository : QuizFormsBaseRepository, IAccountsRepository
    {
        // Constants
        private string ACCOUNTS_FILENAME = "accounts.json";        
        private const string DEFAULT_USERNAME = "admin";
        private const string DEFAULT_PASSWORD = "admin"; // TODO: Create config option for this

        // Private fields
        private SemaphoreSlim _semaphore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public AccountsRepository(IOptions<QuizFormsSettings> settings)
            : base(settings)
        {
            // Create the samaphore to manage the access to the read and write operations.
            _semaphore = new SemaphoreSlim(1, 1);
        }

        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>list with username</returns>
        public async Task<List<string>> GetAccounts()
        {
            // Asynchronously wait to enter the semaphore
            await _semaphore.WaitAsync();

            try
            {
                // Read the accounts from the disk
                List<AccountData> accounts = ReadFromDisk() ?? new List<AccountData>();

                // Return the usernames of the accounts
                return accounts.Select(x => x.Username).ToList();
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Get if an account exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>
        ///     true = account exists;
        ///     false = account does not exist
        /// </returns>
        public async Task<bool> Exists(string username)
        {
            // Convert the username to lowercase
            username = username.ToLower();

            // Asynchronously wait to enter the semaphore
            await _semaphore.WaitAsync();

            try
            {
                // Read the account from the disk
                List<AccountData> accounts = ReadFromDisk();

                // Check if an account with this usernam exists
                return accounts != null && accounts.Any(x => x.Username == username);
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Create an new account
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">account password</param>
        /// <returns>
        ///     true = account created;
        ///     false = an account with the same username already exists
        /// </returns>
        public async Task<bool> CreateAccount(string username, string password)
        {
            // Convert the username to lowercase
            username = username.ToLower();

            // Asynchronously wait to enter the semaphore
            await _semaphore.WaitAsync();

            try
            {
                // Read the accounts from the disk
                List<AccountData> accounts = ReadFromDisk() ?? new List<AccountData>();

                // Return false if the account already exists
                if (accounts.Any(x => x.Username == username))
                    return false;

                // Add the account
                accounts.Add(new AccountData()
                {
                    Username = username,
                    Password = BCrypt.HashPassword(password, BCrypt.GenerateSalt())
                });

                // Write the updated account list to the disk
                WriteToDisk(accounts);

                // Account created
                return true;
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Validate account credentials
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">account password</param>
        /// <returns>
        ///     true = credentials are valid;
        ///     false = the username or password is incorrect
        /// </returns>
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            // Convert the username to lowercase
            username = username.ToLower();

            // Asynchronously wait to enter the semaphore
            await _semaphore.WaitAsync();

            // Read the accounts from the disk
            List<AccountData> accounts = ReadFromDisk();

            try
            {
                if (accounts == null)
                {
                    // Create the initial account list
                    accounts = new List<AccountData>()
                {
                    new AccountData()
                    {
                        Username = DEFAULT_USERNAME,
                        Password = BCrypt.HashPassword(DEFAULT_PASSWORD, BCrypt.GenerateSalt())
                    }
                };

                    // Write the initial account list to the disk
                    WriteToDisk(accounts);

                    // Accept the default username and password
                    if (username == DEFAULT_USERNAME && password == DEFAULT_PASSWORD)
                        return true;
                }
                else
                {
                    // Try to find the account
                    AccountData account = accounts.FirstOrDefault(x => x.Username == username);

                    if (account != null)
                    {
                        // Validate the password with the BCrypt hash
                        return BCrypt.CheckPassword(password, account.Password);
                    }
                }

                return false;
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }            
        }

        /// <summary>
        /// Reset the password for an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">new account password</param>
        /// <returns>
        ///     true = the account was found and the password has been updated;
        ///     false = account not found
        /// </returns>
        public async Task<bool> ResetPassword(string username, string password)
        {
            // Convert the username to lowercase
            username = username.ToLower();

            // Asynchronously wait to enter the semaphore
            await _semaphore.WaitAsync();

            try
            {
                // Read the account from the disk
                List<AccountData> accounts = ReadFromDisk();

                if (accounts != null)
                {
                    // Try to find the account
                    AccountData account = accounts.FirstOrDefault(x => x.Username == username);
                    
                    if (account != null)
                    {
                        // Update the password
                        account.Password = BCrypt.HashPassword(password, BCrypt.GenerateSalt());

                        // Write the updated account list to the disk
                        WriteToDisk(accounts);

                        return true;
                    }
                }

                return false;
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Delete an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <returns>
        ///     true = the account was found and deleted;
        ///     false = account not found
        /// </returns
        public async Task<bool> DeleteAccount(string username)
        {
            // Convert the username to lowercase
            username = username.ToLower();

            // Asynchronously wait to enter the semaphore
            await _semaphore.WaitAsync();

            try
            {
                // Read the account from the disk
                List<AccountData> accounts = ReadFromDisk();

                if (accounts != null)
                {
                    // Try to remove the account
                    bool found = accounts.RemoveAll(x => x.Username == username) > 0;

                    // Write the new list to the disk if the user was found and deleted
                    if (found)
                    {
                        WriteToDisk(accounts);
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }
        }      

        /// <summary>
        /// Create a claim princiapl for an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <returns>claim principal</returns>
        public ClaimsPrincipal CreateClaimPrincipal(string username)
        {
            // Convert the username to lowercase
            username = username.ToLower();

            IIdentity identity = new QuizFormsIdentity(username);
            return new ClaimsPrincipal(identity);
        }

        /// <summary>
        /// Read the account list from the disk.
        /// </summary>
        /// <returns>lists with accounts</returns>
        private List<AccountData> ReadFromDisk()
        {
            string filename = Path.Combine(AuthorizationPath, ACCOUNTS_FILENAME);
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);

                return JsonConvert.DeserializeObject<List<AccountData>>(json);
            }
            return null;
        }

        /// <summary>
        /// Write the account list to the disk
        /// </summary>
        /// <param name="accounts">lists with accounts</param>
        private void WriteToDisk(List<AccountData> accounts)
        {
            string filename = Path.Combine(AuthorizationPath, ACCOUNTS_FILENAME);
            if (accounts == null)
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
            else
            {
                string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
                File.WriteAllText(filename, json);
            }
        }
    }
}
