using QuizForms.Data.Models.Questions;
using System;
using System.Text;

namespace QuizForms.Data.Models.Answers
{
    public sealed class ExtendedAnswer
    {
        public string QuestionId { get; private set; }

        public string QuestionType { get; private set; }

        public string QuestionTitle { get; private set; }

        public int QuestionPoints { get; private set; }

        public string Correct { get; private set; }

        public string Given { get; private set; }

        public int? AssignedPoints { get; private set; }

        public ExtendedAnswer(Question question, string given, int? assignedPoints)
        {
            // Input validation
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            // Set the properties
            QuestionId = question.Id;
            QuestionType = question.Type;
            QuestionTitle = question.Title;
            QuestionPoints = question.Points;
            Correct = question.Correct;
            Given = given;
            AssignedPoints = assignedPoints;
        }
    }
}
