using Newtonsoft.Json;

namespace QuizForms.Data.Models.Forms
{
    public sealed class FormInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }
}
