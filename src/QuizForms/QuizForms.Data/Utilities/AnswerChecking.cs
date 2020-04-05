using QuizForms.Data.Models.Answers;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Models.Questions;
using System.Collections.Generic;

namespace QuizForms.Data.Utilities
{
    /// <summary>
    /// Answer checking utilities
    /// </summary>
    public static class AnswerChecking
    {
        public static ExtendedFormAnswersSet ConstructExtendedFormAnswersSet(FormAnswersSet answerSet, Form form, Dictionary<string, int> scores)
        {
            // Create the model, it will be updated later
            ExtendedFormAnswersSet model = new ExtendedFormAnswersSet(answerSet);

            // Loop through the questions of the form
            foreach (Question question in form.Questions)
            {
                // Increase the total points counter
                model.TotalPoints += question.Points;

                // Get the given answer for the question if any
                string given = null;
                if (answerSet.Answers.ContainsKey(question.Id))
                    given = answerSet.Answers[question.Id];

                // Try to get the existing score;
                // Try to perform an automated check otherwise.
                int? score;
                if (scores != null && scores.ContainsKey(question.Id))
                    score = scores[question.Id];
                else
                    score = TryCheckAnswer(question, given);

                // Add the score to the total points if it's set;
                // Otherwise, increase the manual checking required.
                if (score.HasValue)
                    model.Points += score.Value;
                else
                    model.ManualCheckingRequiredCount++;

                // Add the answer to the list
                model.Answers.Add(new ExtendedAnswer(question, given, score));
            }

            return model;
        }

        public static int? TryCheckAnswer(Question question, string given)
        {
            if (string.IsNullOrWhiteSpace(given))
            {
                // The given answer is empty, this means no points
                return 0;
            }

            if (question.Type == "multiple-choice")
            {
                // The correct answer and given answer need to be exactly the same
                if (given == question.Correct)
                {
                    // Correct                    
                    return question.Points;
                }
                else
                {
                    // Incorrect
                    return 0;
                }
            }

            if (given.ToLower().Trim() == question.Correct.ToLower().Trim())
            {
                // Correct                    
                return question.Points;
            }
            else
            {
                // Can not determine if correct or incorrect.
                // Human checking needed
                return null;
            }
        }
    }
}
