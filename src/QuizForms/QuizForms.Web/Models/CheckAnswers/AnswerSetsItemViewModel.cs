using System;

namespace QuizForms.Web.Models.CheckAnswers
{
    public class AnswerSetsItemViewModel
    {
        public Guid Id { get; set; }

        public string Team { get; set; }

        public int? Points { get; set; }
    }
}
