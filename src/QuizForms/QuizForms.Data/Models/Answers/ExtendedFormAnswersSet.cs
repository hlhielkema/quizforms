using System;
using System.Collections.Generic;

namespace QuizForms.Data.Models.Answers
{
    public class ExtendedFormAnswersSet
    {
        public Guid Id { get; private set; }

        public string Form { get; private set; }        

        public string Team { get; private set; }

        public List<ExtendedAnswer> Answers { get; private set; }

        public DateTime DateCreated { get; private set; }

        public int Points { get; set; }

        public int TotalPoints { get; set; }

        public int ManualCheckingRequiredCount { get; set; }

        public ExtendedFormAnswersSet(FormAnswersSet source)
        {
            // Input validation
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // Set the properties
            Form = source.Form;
            Id = source.Id;
            Team = source.Team;
            Answers = new List<ExtendedAnswer>();
            DateCreated = source.DateCreated;
            Points = 0;
            TotalPoints = 0;
            ManualCheckingRequiredCount = 0;
        }
    }
}
