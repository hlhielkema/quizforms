using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models
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

            if (string.IsNullOrWhiteSpace(teamname))
            {
                error = "Invalid teamname";
                return false;
            }

            return true;
        }
    }
}
