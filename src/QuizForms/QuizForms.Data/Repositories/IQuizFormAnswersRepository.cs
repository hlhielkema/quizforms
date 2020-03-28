using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories
{
    public interface IQuizFormAnswersRepository
    {
        Guid Create(string formId, string team, Dictionary<string, string> answers);        
    }
}
