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
                    Points = null // todo
                }).ToList()
            };
            
            return View(model);
        }

        [HttpGet]
        [Route("{formId}/view/{answersSetId}")]
        public IActionResult View(string formId, Guid answersSetId)
        {

            Form form = _formsRepository.GetById(formId);
            FormAnswerSet answerSet = _answersRepository.Get(formId, answersSetId);
            Dictionary<string, int> scores = _scoresRepository.GetScore(formId, answersSetId);

            List<Answer> answers = new List<Answer>();

            int totalAssignedPoints = 0;
            int totalPoints = 0;
            int manualCheckingRequiredCount = 0;

            foreach (Question question in form.Questions)
            {
                string given = null;
                if (answerSet.Answers.ContainsKey(question.Id))
                    given = answerSet.Answers[question.Id];

                int? score = null;
                if (scores != null && scores.ContainsKey(question.Id))
                    score = scores[question.Id];

                Answer ans = new Answer(question, given, score);
                
                if (!ans.TryAutoAssign())
                    manualCheckingRequiredCount++;

                totalPoints += question.Points;
                if (ans.AssignedPoints.HasValue)
                    totalAssignedPoints += ans.AssignedPoints.Value;

                answers.Add(ans);
            }

            ViewAnswerSetViewModel model = new ViewAnswerSetViewModel()
            {
                FormId = formId,
                AnswersSetId = answersSetId,
                Team = answerSet.Team,
                Answers = answers,
                Points = totalAssignedPoints,
                TotalPoints = totalPoints,
                ManualCheckingRequiredCount = manualCheckingRequiredCount
            };

            return View(model);
        }

        [HttpGet]
        [Route("{formId}/check/{answersSetId}")]
        public IActionResult Check(string formId, Guid answersSetId)
        {
            return View();
        }
    }
}