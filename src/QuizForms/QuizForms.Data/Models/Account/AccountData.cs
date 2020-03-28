using Newtonsoft.Json;

namespace QuizForms.Data.Models.Account
{
    /// <summary>
    /// Account data
    /// </summary>
    public sealed class AccountData
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password hash
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; } // hash
    }
}
