using Newtonsoft.Json;
using QuizForms.Data.Deserializers;
using System;

namespace QuizForms.Data.Models.Livestream
{
    [JsonConverter(typeof(LivestreamDeserializer))]
    public abstract class Livestream
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("type")]
        public abstract string Type { get; }

        [JsonProperty("title")]
        public string Title { get; set; }        
    }
}
