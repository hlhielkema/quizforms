﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizForms.Web.Models.CheckAnswers
{
    public class AnswerSetsViewModel
    {
        public string FormId { get; set; }

        public List<AnswerSetsItemViewModel> Sets { get; set; }
    }
}
