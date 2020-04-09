using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Scoreboard;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuizForms.Data.Repositories.Implementations
{
    /// <summary>
    /// Scoreboard repository
    /// </summary>
    public sealed class ScoreboardRepository : QuizFormsBaseRepository, IScoreboardRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public ScoreboardRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }

        /// <summary>
        /// Get a scoreboard
        /// </summary>
        /// <param name="scoreboardId">scoreboard id</param>
        /// <returns>scoreboard or null if not found</returns>
        public Scoreboard Get(Guid scoreboardId)
        {
            // Construct the filename of the (existing) contact scoreboard file
            string filename = Path.Combine(ScoreboardPath, string.Format("{0}.json", scoreboardId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Deserialize the scoreboard
                Scoreboard scoreboard = JsonConvert.DeserializeObject<Scoreboard>(json);

                // Return the scoreboard
                return scoreboard;
            }

            // Return null if the contact scoreboard does not exist
            return null;
        }

        /// <summary>
        /// Get all scoreboards
        /// </summary>
        /// <returns>scoreboards list</returns>
        public List<ScoreboardInfo> GetAll()
        {
            // Create a list for the scoreboards
            List<ScoreboardInfo> scoreboards = new List<ScoreboardInfo>();

            // Loop through the JSON files in the contact scoreboard directory
            foreach (string filename in Directory.GetFiles(ScoreboardPath, "*.json"))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Deserialize the scoreboard
                Scoreboard scoreboard = JsonConvert.DeserializeObject<Scoreboard>(json);

                // Add the scoreboard to the results list
                scoreboards.Add(new ScoreboardInfo()
                {
                    Id = scoreboard.Id,
                    Title = scoreboard.Title
                });
            }

            // Return the list with scoreboards
            return scoreboards;
        }

        /// <summary>
        /// Create a scoreboard
        /// </summary>
        /// <param name="title">scoreboard title</param>
        /// <param name="rounds">rounds list</param>
        /// <returns>scoreboard id</returns>
        public Guid Create(string title, List<ScoreboardRound> rounds)
        {
            // Combine the data into a model
            Scoreboard scoreboard = new Scoreboard()
            {
                Id = Guid.NewGuid(),
                Title = title,                
                Rounds = rounds,
                Rows = new List<ScoreboardTeam>()
            };

            // Serialize the data to JSON
            string json = JsonConvert.SerializeObject(scoreboard, Formatting.Indented);

            // Construct the filename for the new contact scoreboard
            string filename = Path.Combine(ScoreboardPath, string.Format("{0}.json", scoreboard.Id));

            // Write the JSON to the new contact scoreboard file
            File.WriteAllText(filename, json);

            // Return the unqiue id of the new scoreboard
            return scoreboard.Id;
        }

        /// <summary>
        /// Update a scoreboard
        /// </summary>
        /// <param name="scoreboard">scoreboard</param>
        public void Update(Scoreboard scoreboard)
        {
            // Serialize the data to JSON
            string json = JsonConvert.SerializeObject(scoreboard, Formatting.Indented);

            // Construct the filename for the new contact scoreboard
            string filename = Path.Combine(ScoreboardPath, string.Format("{0}.json", scoreboard.Id));

            // Write the JSON to the new contact scoreboard file
            File.WriteAllText(filename, json);
        }

        /// <summary>
        /// Delete a scoreboard
        /// </summary>
        /// <param name="scoreboardId">scoreboard id </param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        public bool Delete(Guid scoreboardId)
        {
            // Construct the expected filename
            string filename = Path.Combine(ScoreboardPath, string.Format("{0}.json", scoreboardId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Delete the file
                File.Delete(filename);

                // File found and deleted, return true
                return true;
            }
            else
                // File not found, return false
                return false;
        }
    }
}
