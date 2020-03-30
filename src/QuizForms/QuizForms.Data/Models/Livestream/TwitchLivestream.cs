using Newtonsoft.Json;

namespace QuizForms.Data.Models.Livestream
{
    public sealed class TwitchLivestream : Livestream
    {
        public override string Type => "twitch";

        [JsonProperty("channel")]
        public string Channel { get; set; }        
    }
}
