using Newtonsoft.Json;
using System;

namespace QuizForms.Data.Models.Contact
{
    public sealed class ContactMessage
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("emailaddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("date-sent")]
        public DateTime DateSent { get; set; } // UTC
    }
}
