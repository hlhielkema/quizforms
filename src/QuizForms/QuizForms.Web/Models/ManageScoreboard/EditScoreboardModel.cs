using QuizForms.Data.Models.Forms;
using QuizForms.Data.Models.Scoreboard;
using System.Collections.Generic;

namespace QuizForms.Web.Models.ManageScoreboard
{
    public class EditScoreboardModel
    {
        public Scoreboard Scoreboard { get; set; }

        public List<FormInfo> Forms { get; set; }
    }
}
