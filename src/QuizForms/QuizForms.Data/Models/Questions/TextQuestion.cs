using Newtonsoft.Json;

namespace QuizForms.Data.Models.Questions
{
    public sealed class TextQuestion : Question
    {
        public override string Type
            => "text";

        [JsonProperty("placeholder", NullValueHandling = NullValueHandling.Ignore)]
        public string Placeholder { get; set; }  // Optional

        [JsonProperty("correct", NullValueHandling = NullValueHandling.Ignore)]
        public string Correct { get; set; } // Optional

        [JsonProperty("points")]
        public int Points { get; set; }
    }
}
