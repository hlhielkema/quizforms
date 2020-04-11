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
            EditScoreboardModel model = new EditScoreboardModel()
            { 
                Scoreboard = _scoreboardRepository.Get(id),
                Forms = _formsRepository.GetAllVisible(),
            };
            return View(model);
        }

        [HttpGet]
        [Route("view/{id}")]
        public IActionResult View(Guid id)
        {            
            Scoreboard model = _scoreboardRepository.Get(id);
            return View(model);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            // TODO
            // Post impl with ValidateAntiForgeryToken

            _scoreboardRepository.Delete(id);
            return RedirectToAction("Overview");
        }

        [HttpGet]
        [Route("import/{id}/{formId}")]
        public IActionResult ImportScore(Guid id, string formId)
        {
            var scoreboard = _scoreboardRepository.Get(id);

            ImportRoundModel model = new ImportRoundModel()
            {
                Scoreboard = scoreboard,
                RoundId = formId,
                AllChecked = true,
                Scores = new List<ImportTeamModel>(),
                ExistingTeams = scoreboard.Rows.Select(x => x.Team).ToList()
            };

            foreach(var x in _answersRepository.GetAll(formId))
            {
                int? score = _scoresRepository.GetTotalScore(formId, x.Id);
                if (score.HasValue)
                {
                    model.Scores.Add(new ImportTeamModel()
                    {
                        AnswersId = x.Id,
                        Team = x.Team,
                        Score = score.Value
                    });
                }
                else
                {
                    model.AllChecked = false;
                }
            }

            return View(model);
        }        

        [HttpPost]
        [Route("import/{id}/{formId}")]
        [ValidateAntiForgeryToken]
        public IActionResult ImportScore(Guid id, string formId, [FromBody] ImportRoundSubmitModel model)
        {
            if (ModelState.IsValid)
            {
                List<Guid> keys = new List<Guid>();
                List<string> values = new List<string>();                
                foreach (KeyValuePair<Guid, string> mapping in model.Mappings)
                {
                    if (keys.Contains(mapping.Key))
                        return BadRequest("Duplicate answers id");
                    if (values.Contains(mapping.Value))
                        return BadRequest("Duplicate target team");
                    keys.Add(mapping.Key);
                    values.Add(mapping.Value);
                }
                
                // Get the scoreboard
                Scoreboard scoreboard = _scoreboardRepository.Get(id);

                // Get the scores for the answers sets
                Dictionary<string, int> importedScores = new Dictionary<string, int>();
                foreach (KeyValuePair<Guid, string> mapping in model.Mappings)
                {
                    int? score = _scoresRepository.GetTotalScore(formId, mapping.Key);
                    if (score.HasValue)
                        importedScores.Add(mapping.Value, score.Value);
                    else
                        return BadRequest("Score not found for answers set: " + mapping.Key);
                }

                // Add the missing teams
                foreach (string team in values)
                {
                    if (!scoreboard.Rows.Any(x => x.Team == team))
                    {
                        scoreboard.Rows.Add(new ScoreboardTeam()
                        {
                            Team = team,
                            Scores = new Dictionary<string, int>(),
                        });
                    }
                }
                
                // Add the scores to the teams
                foreach (ScoreboardTeam row in scoreboard.Rows)
                {
                    if (importedScores.ContainsKey(row.Team))
                    {
                        row.Scores[formId] = importedScores[row.Team];
                    }
                    else
                    {
                        if (row.Scores.ContainsKey(formId))
                            row.Scores.Remove(formId);
                    }
                }

                // Remove the rows with no scores
                scoreboard.Rows.RemoveAll(x => x.Scores == null || x.Scores.Count == 0);

                // Sort score from high to low
                scoreboard.Rows = scoreboard.Rows.OrderByDescending(x => x.Scores.Sum(x => x.Value))
                                                 .ToList();

                // Update the scoreboard
                _scoreboardRepository.Update(scoreboard);
           
                // Everything OK, redirect
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