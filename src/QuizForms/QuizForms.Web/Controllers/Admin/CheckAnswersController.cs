using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models.CheckAnswers;
using System;
using System.Linq;

namespace QuizForms.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/check-answers")]
    public class CheckAnswersController : Controller
    {
        private readonly IQuizFormsRepository _formsRepository;
        private readonly IQuizFormAnswersRepository _answersRepository;

        public CheckAnswersController(IQuizFormsRepository formsRepository, IQuizFormAnswersRepository answersRepository)
        {
            _formsRepository = formsRepository;
            _answersRepository = answersRepository;
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
                    Points = null // todo
                }).ToList()
            };
            
            return View(model);
        }

        [HttpGet]
        [Route("{formId}/view/{answersSetId}")]
        public IActionResult View(string formId, Guid answersSetId)
        {
            return View();
        }

        [HttpGet]
        [Route("{formId}/check/{answersSetId}")]
        public IActionResult Check(string formId, Guid answersSetId)
        {
            return View();
        }
    }
}