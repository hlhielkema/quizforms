using QuizForms.Data.Models.Forms;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    public interface IQuizFormsRepository
    {
        bool ExistsAndAvailable(string id);

        bool Exists(string id);

        List<FormInfo> GetAllVisible();

        List<FormInfo> GetAll();

        Form GetById(string id);
    }
}
