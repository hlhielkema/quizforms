using Newtonsoft.Json;

namespace QuizForms.Data.Models.Livestream
{
    public sealed class YoutubeLivestream : Livestream
    {
        public override string Type => "youtube";

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
