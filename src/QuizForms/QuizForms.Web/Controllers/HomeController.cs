using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizForms.Data.Managers;
using QuizForms.Data.Models.Forms;
using QuizForms.Web.Models;

namespace QuizForms.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Temp for testing
            string dir = @"D:\GitHub Workspace\quizforms\data\forms";
            List<FormInfo> forms = QuizFormManager.GetFormsFromDirectory(dir);

            //Form firstForm = QuizFormManager.GetFormByid(dir, forms[0].Id);

            HomeViewModel model = new HomeViewModel()
            {
                AvailableForms = forms.Where(x => !x.Hidden).ToList()
            };

            return View(model);
        }

        [Route("twitch")]
        [HttpGet]
        public IActionResult Twitch()
        {            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
