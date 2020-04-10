using QuizForms.Data.Models.Scoreboard;
using System.Collections.Generic;

namespace QuizForms.Web.Models.ManageScoreboard
{
    public class ImportRoundModel
    {
        public Scoreboard Scoreboard { get; set; }

        public string RoundId { get; set; }

        public bool AllChecked { get; set; }

        public List<ImportTeamModel> Scores { get; set; }        

        public List<string> ExistingTeams { get; set; }
    }
}
