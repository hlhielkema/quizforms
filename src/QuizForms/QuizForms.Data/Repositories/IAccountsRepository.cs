using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizForms.Data.Repositories
{
    public interface IAccountsRepository
    {
        Task<bool> Exists(string username);
        
        Task<List<string>> GetAccounts();

        Task<bool> CreateAccount(string username, string password);

        Task<bool> ValidateCredentials(string username, string password);        

        Task<bool> ResetPassword(string username, string password);

        Task<bool> DeleteAccount(string username);

        ClaimsPrincipal CreateClaimPrincipal(string username);
    }
}
