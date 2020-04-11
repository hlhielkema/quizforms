using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.QuizForm
{
    public class SubmitQuizFormModel
    {  
        [Required]
        public List<KeyValuePair<string, string>> Answers { get; set; }

        public bool TryExtractAnswers(out string teamname, out Dictionary<string, string> answers, out string error)
        {            
            teamname = null;
            answers = new Dictionary<string, string>();
            error = null;

            foreach (KeyValuePair<string, string> answer in Answers)
            {
                if (answer.Key.Length > 500 || answer.Value.Length > 500)
                {
                    error = "Answer too long";
                    return false;
                }

                if (answers.ContainsKey(answer.Key))
                {
                    error = "Duplicate answer";
                    return false;
                }                    

                if (answer.Key == "Teamname")
                    teamname = answer.Value;
                else
                    answers.Add(answer.Key, answer.Value);
            }

            if (string.IsNullOrWhiteSpace(teamname) || teamname.Length > 50)
            {
                error = "Invalid teamname";
                return false;
            }

            return true;
        }
    }
}
