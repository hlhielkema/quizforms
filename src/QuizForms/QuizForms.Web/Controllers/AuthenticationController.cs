using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Models.Account;
using QuizForms.Data.Repositories;
using QuizForms.Web.Models;

namespace QuizForms.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAccountsRepository _accountRepository;

        public AuthenticationController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("sign-in")]
        [HttpGet]
        public IActionResult SignIn(string redirect)
        {            
            return View(new SignInModel() { 
                Redirect = redirect
            });
        }

        [Route("sign-in")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {            
            if (ModelState.IsValid)
            {
                // Validate the credentislas
                bool validCredentials = await _accountRepository.ValidateCredentials(model.Username, model.Password);

                if (validCredentials)
                {
                    // Sign-in the user account                    
                    await HttpContext.SignInAsync(_accountRepository.CreateClaimPrincipal(model.Username));

                    // Redirect to the default page or redirect URL
                    if (string.IsNullOrWhiteSpace(model.Redirect))
                        return RedirectToAction("Index", "Account");
                    else
                        return LocalRedirect(model.Redirect);
                }
                else
                {
                    // Invalid credentials, set model error
                    ModelState.AddModelError(nameof(SignInModel.Password), "Invalid credentials");

                    return View(model);
                }
            }
            else
            {
                // Invalid model state
                return View(model);
            }
        }

        [AllowAnonymous]
        [Route("sign-out")]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}