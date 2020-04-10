using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace QuizForms.Data.Models.Scoreboard
{
    public sealed class ScoreboardTeam
    {
        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("scores")]
        public Dictionary<string, int> Scores { get; set; }       
    }
}
