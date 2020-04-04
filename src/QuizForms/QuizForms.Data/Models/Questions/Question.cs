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

        [JsonProperty("correct", NullValueHandling = NullValueHandling.Ignore)]
        public string Correct { get; set; } // Optional  

        public virtual string GetOptionsDescription()
            => "-";
    }
}
