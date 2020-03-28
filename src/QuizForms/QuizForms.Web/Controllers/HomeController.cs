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

        public IActionResult Index()
        {                                    
            return View(new HomeViewModel()
            {
                AvailableForms = _formsRepository.GetAllVisible()
            });
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
