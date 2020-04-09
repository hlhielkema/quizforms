using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Models.Scoreboard;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models.ManageScoreboard;

namespace QuizForms.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/scoreboard")]    
    public class ManageScoreboardController : Controller
    {
        private readonly IScoreboardRepository _scoreboardRepository;
        private readonly IQuizFormsRepository _formsRepository;
        private readonly IQuizFormAnswersRepository _answersRepository;
        private readonly IQuizFormsScoresRepostiory _scoresRepository;

        public ManageScoreboardController(IScoreboardRepository scoreboardRepository,
                                      IQuizFormsRepository formsRepository,
                                      IQuizFormAnswersRepository answersRepository,
                                      IQuizFormsScoresRepostiory scoresRepository)
        {
            _scoreboardRepository = scoreboardRepository;
            _formsRepository = formsRepository;
            _answersRepository = answersRepository;
            _scoresRepository = scoresRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Overview()
        {
            List<ScoreboardInfo> scoreboards = _scoreboardRepository.GetAll();

            return View(scoreboards);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateScoreboardModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the rounds
                List<ScoreboardRound> rounds = _formsRepository.GetAllVisible().Select(x => new ScoreboardRound() {
                    Id = x.Id,
                    Title = x.Title,
                }).ToList();

                // Create the scoreboard
                _scoreboardRepository.Create(model.Title, rounds);

                return RedirectToAction("Overview");
            }
            else
            {
                // Invalid model state
                return View(model);
            }
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            Scoreboard model = _scoreboardRepository.Get(id);
            return View(model);
        }
    }
}