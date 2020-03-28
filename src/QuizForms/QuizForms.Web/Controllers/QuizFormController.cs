using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Repositories;

namespace QuizForms.Web.Controllers
{
    [Route("forms")]
    public class QuizFormController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuizFormsRepository _formsRepository;

        public QuizFormController(ILogger<HomeController> logger, IQuizFormsRepository formsRepository)
        {
            _logger = logger;
            _formsRepository = formsRepository;
        }

        [Route("{id}")]
        [HttpGet]        
        public IActionResult Index(string id)
        {            
            Form form = _formsRepository.GetById(id);

            // TODO: error handling
            // Redirect to 404 if the form is hidden or inactive

            return View(form);
        }
    }
}