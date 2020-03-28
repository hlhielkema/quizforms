using QuizForms.Data.Models.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories
{
    public interface IQuizFormsRepository
    {
        List<FormInfo> GetAllVisible();

        List<FormInfo> GetAll();

        Form GetById(string id);
    }
}
