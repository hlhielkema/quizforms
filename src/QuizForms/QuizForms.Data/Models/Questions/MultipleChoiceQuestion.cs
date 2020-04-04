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

        public override string GetOptionsDescription()
            => string.Join(", ", Options);
    }
}
