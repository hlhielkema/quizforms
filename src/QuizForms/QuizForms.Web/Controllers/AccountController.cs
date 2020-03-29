using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Web.Models;

namespace QuizForms.Web.Controllers
{
    [Authorize]
    [Route("admin/accounts")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new AccountInfoModel()
            {
                Username = User.Identity.Name
            });
        }       
    }
}