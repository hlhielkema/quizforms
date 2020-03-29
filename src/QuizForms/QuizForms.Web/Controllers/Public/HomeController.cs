using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models;
using System.Diagnostics;

namespace QuizForms.Web.Controllers.Public
{
    public class HomeController : Controller
    {        
        private readonly IQuizFormsRepository _formsRepository;

        public HomeController(IQuizFormsRepository formsRepository)
        {
            _formsRepository = formsRepository;
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {                                    
            return View(new HomeViewModel()
            {
                VisibleForms = _formsRepository.GetAllVisible()
            });
        }

        [Route("/about")]
        [HttpGet]
        public IActionResult About()
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
