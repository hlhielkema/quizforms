using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Models.Answers;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models.CheckAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using QuizForms.Data.Models.Questions;
using QuizForms.Data.Utilities;

namespace QuizForms.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/check-answers")]
    public class CheckAnswersController : Controller
    {
        private readonly IQuizFormsRepository _formsRepository;
        private readonly IQuizFormAnswersRepository _answersRepository;
        private readonly IQuizFormsScoresRepostiory _scoresRepository;

        public CheckAnswersController(IQuizFormsRepository formsRepository, 
                                      IQuizFormAnswersRepository answersRepository,
                                      IQuizFormsScoresRepostiory scoresRepository)
        {
            _formsRepository = formsRepository;
            _answersRepository = answersRepository;
            _scoresRepository = scoresRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Overview()
        {            
            AnswersFormOverviewViewModel model = new AnswersFormOverviewViewModel()
            { 
                Forms = _formsRepository.GetAll()
            };

            return View(model);
        }

        [HttpGet]
        [Route("{formId}")]
        public IActionResult Index(string formId)
        {
            

            AnswerSetsViewModel model = new AnswerSetsViewModel()
            {
                FormId = formId,
                Sets = _answersRepository.GetAll(formId).Select(x => new AnswerSetsItemViewModel()
                {
                    Id = x.Id,
                    Team = x.Team,
                    Points = _scoresRepository.GetTotalScore(formId, x.Id)
                }).ToList()
            };
            
            return View(model);
        }

        [HttpGet]
        [Route("{formId}/view/{answersSetId}")]
        public IActionResult View(string formId, Guid answersSetId)
        {
            // Try to get the quiz form
            Form form = _formsRepository.GetById(formId);
            if (form == null)
                return NotFound();

            // Try to get the answers set
            FormAnswersSet answerSet = _answersRepository.Get(formId, answersSetId);
            if (answerSet == null)
                return NotFound();

            // Try to get the existing scores
            Dictionary<string, int> scores = _scoresRepository.GetScore(formId, answersSetId);

            // Combine the data into a extended form answers model
            ExtendedFormAnswersSet model = AnswerChecking.ConstructExtendedFormAnswersSet(answerSet, form, scores);

            return View(model);
        }

        [HttpGet]
        [Route("{formId}/check/{answersSetId}")]
        public IActionResult Check(string formId, Guid answersSetId)
        {
            // Try to get the quiz form
            Form form = _formsRepository.GetById(formId);
            if (form == null)
                return NotFound();

            // Try to get the answers set
            FormAnswersSet answerSet = _answersRepository.Get(formId, answersSetId);
            if (answerSet == null)
                return NotFound();

            // Try to get the existing scores
            Dictionary<string, int> scores = _scoresRepository.GetScore(formId, answersSetId);

            // Combine the data into a extended form answers model
            ExtendedFormAnswersSet model = AnswerChecking.ConstructExtendedFormAnswersSet(answerSet, form, scores);

            return View(model);
        }

        [Route("{formId}/check/{answersSetId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult Check(string formId, Guid answersSetId, [FromBody] SubmitScoreModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Check if form and answers exist

                Dictionary<string, int> scores = model.Scores.ToDictionary(x => x.Key, x => x.Value);
                _scoresRepository.UpdateScore(formId, answersSetId, scores);

                // Success, return 200 status
                return Ok();
            }
            else
            {
                // Invalid model, return 400 status
                return BadRequest("Invalid model data");
            }
        }
    }
}