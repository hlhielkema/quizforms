using Newtonsoft.Json;
using QuizForms.Data.Models.Questions;
using System.Collections.Generic;

namespace QuizForms.Data.Models.Forms
{
    public sealed class Form
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; } // Optional

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        public List<Question> Questions { get; set; }
    }
}
