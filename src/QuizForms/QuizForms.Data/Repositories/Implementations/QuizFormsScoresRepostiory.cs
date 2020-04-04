using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories.Implementations
{
    public sealed class QuizFormsScoresRepostiory : IQuizFormsScoresRepostiory
    {
        public Dictionary<string, int> GetScore(string formId, Guid answersId)
        {
            throw new NotImplementedException();
        }

        public int? GetTotalScore(string formId, Guid answersId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Guid, int> GetTotalScores(string formId)
        {
            throw new NotImplementedException();
        }

        public void UpdateScore(string formId, Guid answersId, Dictionary<string, int> score)
        {
            throw new NotImplementedException();
        }
    }
}
