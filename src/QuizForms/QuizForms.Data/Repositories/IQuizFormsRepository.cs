using QuizForms.Data.Models.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories
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
