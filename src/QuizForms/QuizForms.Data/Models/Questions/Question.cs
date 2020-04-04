using Newtonsoft.Json;
using QuizForms.Data.Deserializers;

namespace QuizForms.Data.Models.Questions
{
    [JsonConverter(typeof(QuestionDeserializer))]
    public abstract class Question
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("type")]
        public abstract string Type { get; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("correct")]
        public string Correct { get; set; }

        public virtual string GetOptionsDescription()
            => "-";
    }
}
