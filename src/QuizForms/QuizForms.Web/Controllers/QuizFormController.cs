using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Repositories;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models;
using System.Collections.Generic;

namespace QuizForms.Web.Controllers
{    
    [Route("forms")]
    public class QuizFormController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuizFormsRepository _formsRepository;
        private readonly IQuizFormAnswersRepository _answersRepository;

        public QuizFormController(ILogger<HomeController> logger, IQuizFormsRepository formsRepository, IQuizFormAnswersRepository answersRepository)
        {
            _logger = logger;
            _formsRepository = formsRepository;
            _answersRepository = answersRepository;
        }

        [Route("{id}")]
        [HttpGet]        
        public IActionResult Index(string id)
        {
            if (_formsRepository.ExistsAndAvailable(id))
            {
                Form form = _formsRepository.GetById(id);

                // TODO: error handling
                // Redirect to 404 if the form is hidden or inactive

                return View(form);
            }
            else
                return NotFound();
        }

        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string id, [FromBody] SubmitQuizFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (_formsRepository.ExistsAndAvailable(id))               
                {
                    // Try to extract the answers from the model data
                    if (model.TryExtractAnswers(out string teamname, out Dictionary<string, string> answers, out string error))
                    {
                        // TODO:
                        // Validate values with the form

                        // Store the answers
                        _answersRepository.Create(id, teamname, answers);

                        // Success, return 200 status
                        return Ok();
                    }
                    else
                    {
                        // Invalid model, return 400 status
                        return BadRequest(error);
                    }
                }                    
                else
                {
                    return NotFound("The form does not exists or it's not available.");
                }
            }
            else
            {
                // Invalid model, return 400 status
                return BadRequest("Invalid model data");
            }   
        }
    }
}