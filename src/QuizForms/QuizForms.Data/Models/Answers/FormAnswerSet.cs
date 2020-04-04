using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Models.Answers
{
    public sealed class FormAnswerSet
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("form")]
        public string Form { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("answers")]
        public Dictionary<string, string> Answers { get; set; }

        [JsonProperty("date-created")]
        public DateTime DateCreated { get; set; } // UTC
    }
}
