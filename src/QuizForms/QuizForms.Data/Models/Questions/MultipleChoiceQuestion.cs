using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuizForms.Data.Models.Questions
{
    public sealed class MultipleChoiceQuestion : Question
    {
        public override string Type
            => "multiple-choice";

        [JsonProperty("options")]
        public List<string> Options { get; set; }

        [JsonProperty("correct", NullValueHandling = NullValueHandling.Ignore)]
        public string Correct { get; set; } // Optional

        [JsonProperty("points")]
        public int Points { get; set; }        
    }
}
