using Newtonsoft.Json;

namespace QuizForms.Data.Models.Scoreboard
{
    public sealed class ScoreboardRound
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
