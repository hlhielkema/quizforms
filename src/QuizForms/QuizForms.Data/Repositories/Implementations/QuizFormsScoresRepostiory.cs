using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizForms.Data.Repositories.Implementations
{
    /// <summary>
    /// Quiz forms scores repository
    /// </summary>
    public sealed class QuizFormsScoresRepostiory : QuizFormsBaseRepository, IQuizFormsScoresRepostiory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public QuizFormsScoresRepostiory(IOptions<QuizFormsSettings> settings)
            : base(settings)
        { }

        /// <summary>
        /// Get the scores for answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>dictionary with a score for each answer</returns>
        public Dictionary<string, int> GetScore(string formId, Guid answersId)
        {
            // Get the path for the scores directory of the round.
            // Return null if it does not exist.
            string formScoresPath = Path.Combine(ScoresPath, formId);
            if (!Directory.Exists(formScoresPath))
                return null;

            // Construct the filename for the new form score
            string filename = Path.Combine(formScoresPath, string.Format("{0}.json", answersId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Deserialize the JSON and return it
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            }
            else
                // File not found, return null
                return null;
        }

        /// <summary>
        /// Get the total score for an answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>the total score or null if not found</returns>
        public int? GetTotalScore(string formId, Guid answersId)
        {
            // Get the path for the scores directory of the round.
            // Return null if it does not exist.
            string formScoresPath = Path.Combine(ScoresPath, formId);
            if (!Directory.Exists(formScoresPath))
                return null;

            // Construct the filename for the new form score
            string filename = Path.Combine(formScoresPath, string.Format("{0}.json", answersId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Deserialize the JSON
                Dictionary<string, int> scores = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

                // Sum the scores to get the total, then return it
                return scores.Sum(x => x.Value);
            }
            else
                // File not found, return null
                return null;
        }

        /// <summary>
        /// Get the total score for all answer sets
        /// </summary>
        /// <param name="formId">form id</param>        
        /// <returns>dictionary with answer sets id's and their total score</returns>
        public Dictionary<Guid, int> GetTotalScores(string formId)
        {
            // Create a dictionary to store the results
            Dictionary<Guid, int> results = new Dictionary<Guid, int>();

            // Get the path for the scores directory of the round.
            // Return null if it does not exist.
            string formScoresPath = Path.Combine(ScoresPath, formId);
            if (!Directory.Exists(formScoresPath))
                return null;

            // Loop through the JSON files in the directory
            foreach (string filename in Directory.GetFiles(formScoresPath, "*.json"))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Extract the answers id from the filename
                Guid answersId = Guid.Parse(Path.GetFileNameWithoutExtension(filename));

                // Deserialize the JSON
                Dictionary<string, int> scores = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

                // Sum the scores to get the total, then return it
                int total = scores.Sum(x => x.Value);

                // Add the total to the results dictionary
                results.Add(answersId, total);
            }

            // Return the dictionary with results
            return results;
        }

        /// <summary>
        /// Update the score for an answer set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <param name="scores">dictionary with a score for each answer</param>
        public void UpdateScore(string formId, Guid answersId, Dictionary<string, int> scores)
        {
            // Get the path for the scores directory of the round.
            // Create it if it does not exist yet.
            string formScoresPath = Path.Combine(ScoresPath, formId);
            if (!Directory.Exists(formScoresPath))
                Directory.CreateDirectory(formScoresPath);

            // Construct the filename for the new form score
            string filename = Path.Combine(formScoresPath, string.Format("{0}.json", answersId));

            // Delete the score if it already exists
            if (File.Exists(filename))
                File.Delete(filename);

            // Serialize the scores
            string json = JsonConvert.SerializeObject(scores);

            // Write the JSON to the new forms answers file
            File.WriteAllText(filename, json);
        }

        /// <summary>
        /// Delete the score for an answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        public bool DeleteScore(string formId, Guid answersId)
        {
            // Get the path for the scores directory of the round.
            // Return false if it does not exist.
            string formScoresPath = Path.Combine(ScoresPath, formId);
            if (!Directory.Exists(formScoresPath))
                return false;

            // Construct the filename for the new form score
            string filename = Path.Combine(formScoresPath, string.Format("{0}.json", answersId));

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
