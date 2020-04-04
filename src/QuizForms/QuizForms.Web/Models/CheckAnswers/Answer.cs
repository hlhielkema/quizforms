using QuizForms.Data.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizForms.Web.Models.CheckAnswers
{
    public class Answer
    {       
        public string QuestionId { get; private set; }

        public string QuestionType { get; private set; }

        public string QuestionTitle { get; private set; }

        public int QuestionPoints { get; private set; }

        public string Correct { get; private set; }
                
        public string Given { get; private set; }

        public int? AssignedPoints { get; private set; }

        public Answer(Question question, string given, int? assignedPoints)
        {
            QuestionId = question.Id;
            QuestionType = question.Type;
            QuestionTitle = question.Title;
            QuestionPoints = question.Points;
            Correct = question.Correct;
            Given = given;
            AssignedPoints = assignedPoints;
        }

        public bool TryAutoAssign()
        {
            if (!AssignedPoints.HasValue)
            {
                if (string.IsNullOrWhiteSpace(Given))
                {
                    // Incorrect
                    AssignedPoints = 0;

                    //
                    return true;
                }

                if (QuestionType == "multiple-choice")
                {
                    if (Given == Correct)
                    {
                        // Correct
                        AssignedPoints = QuestionPoints;

                        //
                        return true;
                    }
                    else
                    {
                        // Incorrect
                        AssignedPoints = 0;

                        //
                        return true;
                    }
                }
                else
                {

                    if (Given.ToLower().Trim() == Correct.ToLower().Trim())
                    {
                        // Correct
                        AssignedPoints = QuestionPoints;

                        //
                        return true;
                    }
                    else
                    {

                        // Can not determine if correct or incorrect.
                        // Human checking needed

                        return false;

                    }
                }
            }

            return true;
        }
    }
}
