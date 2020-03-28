using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace QuizForms.Data.Repositories
{
    public abstract class QuizFormsBaseRepository
    {
        public string DataPath { get; private set; }

        public string FormsPath { get; private set; }

        public string AnswersPath { get; private set; }

        public string ScoresPath { get; private set; }

        public string ContactPath { get; private set; }

        // Constants
        private const string DIRECTORY_FORMS = "forms";
        private const string DIRECTORY_ANSWERS = "answers";
        private const string DIRECTORY_SCORES = "scores";
        private const string DIRECTORY_CONTACT = "contact";

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        internal QuizFormsBaseRepository(IOptions<QuizFormsSettings> settings)
        {
            // Set the base path
            DataPath = settings.Value.DataPath;            

            // Validate the settings
            if (string.IsNullOrWhiteSpace(DataPath) || !Directory.Exists(DataPath))
                throw new Exception("Data path from configuration empty or not found.");

            // Create the paths for the child directories
            FormsPath = Path.Combine(DataPath, DIRECTORY_FORMS);
            AnswersPath = Path.Combine(DataPath, DIRECTORY_ANSWERS);
            ScoresPath = Path.Combine(DataPath, DIRECTORY_SCORES);
            ContactPath= Path.Combine(DataPath, DIRECTORY_CONTACT);

            // Create the chil directories that do not exist
            if (!Directory.Exists(FormsPath))
                Directory.CreateDirectory(FormsPath);
            if (!Directory.Exists(AnswersPath))
                Directory.CreateDirectory(AnswersPath);
            if (!Directory.Exists(ScoresPath))
                Directory.CreateDirectory(ScoresPath);
            if (!Directory.Exists(ContactPath))
                Directory.CreateDirectory(ContactPath);
        }
    }
}
