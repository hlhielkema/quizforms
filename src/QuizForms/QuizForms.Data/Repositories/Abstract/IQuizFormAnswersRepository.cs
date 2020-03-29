using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    public interface IQuizFormAnswersRepository
    {
        Guid Create(string formId, string team, Dictionary<string, string> answers);        
    }
}
