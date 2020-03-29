using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizForms.Data.Repositories;
using QuizForms.Web.Models;
using System.Diagnostics;

namespace QuizForms.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuizFormsRepository _formsRepository;

        public HomeController(ILogger<HomeController> logger, IQuizFormsRepository formsRepository)
        {
            _logger = logger;
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
