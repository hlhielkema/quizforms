using System;

namespace QuizForms.Web.Models.ManageScoreboard
{
    public class ImportTeamModel
    {
        public Guid AnswersId { get; set; }

        public string Team { get; set; }

        public int Score { get; set; }
    }
}
