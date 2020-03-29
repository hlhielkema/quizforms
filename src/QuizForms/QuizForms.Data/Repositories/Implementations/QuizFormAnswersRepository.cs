using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Answers;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuizForms.Data.Repositories.Implementations
{
    public sealed class QuizFormAnswersRepository : QuizFormsBaseRepository, IQuizFormAnswersRepository
    {
        public QuizFormAnswersRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }

        public Guid Create(string formId, string team, Dictionary<string, string> answers)
        {
            // Combine the data into a model
            FormAnswers model = new FormAnswers()
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
    }
}
