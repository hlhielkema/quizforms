using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models.Account;
using System.Threading.Tasks;

namespace QuizForms.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/accounts")]
    public class AccountController : Controller
    {
        private readonly IAccountsRepository _accountRepository;

        public AccountController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ManageAccountsViewModel model = new ManageAccountsViewModel()
            {
                Usernames = await _accountRepository.GetAccounts()
            };

            return View(model);
        }

        [HttpPost]
        [Route("update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string action, string id)
        {            
            if (action == "delete")
            {
                await _accountRepository.DeleteAccount(id);
            }

            return LocalRedirect("/admin/accounts");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountRepository.CreateAccount(model.Username, model.Password))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(CreateAccountModel.Username), "De gebruikersnaam is al in gebruik.");
                    return View(model);
                }
            }
            else
            {
                // Invalid model state
                return View(model);
            }
        }

        [HttpGet]
        [Route("update-password/{username}")]
        public IActionResult UpdatePassword(string username)
        {
            UpdatePasswordModel model = new UpdatePasswordModel()
            {
                Username = username
            };

            return View(model);
        }

        [HttpPost]
        [Route("update-password/{username}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel model)
        {
            if (await _accountRepository.ResetPassword(model.Username, model.Password))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(nameof(CreateAccountModel.Username), "Gebruiker niet gevonden.");
                return View(model);
            }
        }
    }
}