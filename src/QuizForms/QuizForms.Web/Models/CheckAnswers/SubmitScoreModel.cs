using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizForms.Web.Models.CheckAnswers
{
    public class SubmitScoreModel
    {
        [Required]
        public List<KeyValuePair<string, int>> Scores { get; set; }
    }
}
