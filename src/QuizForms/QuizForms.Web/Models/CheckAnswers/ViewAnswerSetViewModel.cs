using System;
using System.Collections.Generic;

namespace QuizForms.Web.Models.CheckAnswers
{
    public class ViewAnswerSetViewModel
    {
        public string FormId { get; set; }

        public Guid AnswersSetId { get; set; }

        public string Team { get; set; }

        public List<Answer> Answers { get; set; }

        public int Points { get; set; }

        public int TotalPoints { get; set; }

        public int ManualCheckingRequiredCount { get; set; }
    }
}
