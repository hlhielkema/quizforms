using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Answers;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuizForms.Data.Repositories.Implementations
{
    /// <summary>
    /// Quiz forms answers repository
    /// </summary>
    public sealed class QuizFormAnswersRepository : QuizFormsBaseRepository, IQuizFormAnswersRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public QuizFormAnswersRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }

        /// <summary>
        /// Create a set with answers
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="team">team name</param>
        /// <param name="answers">answers dictionary</param>
        /// <returns>form answers set</returns>
        public Guid Create(string formId, string team, Dictionary<string, string> answers)
        {
            // Combine the data into a model
            FormAnswersSet model = new FormAnswersSet()
            {
                Id = Guid.NewGuid(),
                Form = formId,
                Team = team,
                Answers = answers,
                DateCreated = DateTime.UtcNow
            };

            // Get the path for the answers directory of the round.
            // Create it if it does not exist yet.
            string formAnswersPath = Path.Combine(AnswersPath, formId);
            if (!Directory.Exists(formAnswersPath))
                Directory.CreateDirectory(formAnswersPath);

            // Serialize the data to JSON
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);

            // Construct the filename for the new form answers
            string filename = Path.Combine(formAnswersPath, string.Format("{0}.json", model.Id));

            // Write the JSON to the new forms answers file
            File.WriteAllText(filename, json);

            // Return the unqiue id of the new form answers
            return model.Id;
        }

        /// <summary>
        /// Get all form answers sets
        /// </summary>
        /// <param name="formId">form id</param>
        /// <returns>list with form answer sets</returns>
        public List<FormAnswersSet> GetAll(string formId)
        {
            // Create a list to store the results
            List<FormAnswersSet> results = new List<FormAnswersSet>();

            // Get the path for the answers directory of the round.    
            // Return an empty list if it does not exist.
            string formAnswersPath = Path.Combine(AnswersPath, formId);
            if (!Directory.Exists(formAnswersPath))
                return results; // empty at this point

            // Loop through the JSON files in the directory
            foreach (string filename in Directory.GetFiles(formAnswersPath, "*.json"))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Parse the JSON
                results.Add(JsonConvert.DeserializeObject<FormAnswersSet>(json));
            }

            // Return the list with results
            return results;
        }

        /// <summary>
        /// Get a form answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>form answers set or null if not found</returns>
        public FormAnswersSet Get(string formId, Guid answersId)
        {
            // Construct the expected filename
            string filename = Path.Combine(AnswersPath, formId, string.Format("{0}.json", answersId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Parse the JSON and return it
                return JsonConvert.DeserializeObject<FormAnswersSet>(json);
            }
            else
                // File not found, return null
                return null;
        }

        /// <summary>
        /// Delete a form answers set.
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        public bool Delete(string formId, Guid answersId)
        {
            // Construct the expected filename
            string filename = Path.Combine(AnswersPath, formId, string.Format("{0}.json", answersId));

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
