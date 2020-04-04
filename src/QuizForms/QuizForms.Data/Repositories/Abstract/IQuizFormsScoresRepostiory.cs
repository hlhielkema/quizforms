using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    public interface IQuizFormsScoresRepostiory
    {
        Dictionary<string, int> GetScore(string formId, Guid answersId);

        int? GetTotalScore(string formId, Guid answersId);

        Dictionary<Guid, int> GetTotalScores(string formId);

        void UpdateScore(string formId, Guid answersId, Dictionary<string, int> score);
    }
}
