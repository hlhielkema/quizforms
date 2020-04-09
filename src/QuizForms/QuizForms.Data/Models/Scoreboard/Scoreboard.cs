using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Models.Scoreboard
{
    public sealed class Scoreboard
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("rounds")]
        public List<ScoreboardRound> Rounds { get; set; }

        [JsonProperty("rows")]
        public List<ScoreboardTeam> Rows { get; set; }
    }
}
