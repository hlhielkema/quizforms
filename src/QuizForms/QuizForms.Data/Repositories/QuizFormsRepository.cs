using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Models.Questions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace QuizForms.Data.Repositories
{
    public sealed class QuizFormsRepository : QuizFormsBaseRepository, IQuizFormsRepository
    {
        public QuizFormsRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }

        public List<FormInfo> GetAllVisible()
        {
            List<FormInfo> results = new List<FormInfo>();
            foreach (string filename in Directory.GetFiles(FormsPath))
            {
                string contents = File.ReadAllText(filename);
                FormInfo form = JsonConvert.DeserializeObject<FormInfo>(contents);
                if (!form.Hidden)
                    results.Add(form);
            }
            return results;
        }

        public List<FormInfo> GetAll()
        {
            List<FormInfo> results = new List<FormInfo>();
            foreach (string filename in Directory.GetFiles(FormsPath))
            {
                string contents = File.ReadAllText(filename);
                FormInfo form = JsonConvert.DeserializeObject<FormInfo>(contents);
                results.Add(form);
            }
            return results;
        }

        public Form GetById(string id)
        {
            foreach (string filename in Directory.GetFiles(FormsPath))
            {
                string contents = File.ReadAllText(filename);
                Form form = JsonConvert.DeserializeObject<Form>(contents);
                if (form.Id == id)
                {
                    ValidateAndCorrectForm(form);
                    return form;
                }
            }
            return null;
        }

        private void ValidateAndCorrectForm(Form form)
        {
            foreach (Question question in form.Questions)
            {
                if (string.IsNullOrWhiteSpace(question.Id))
                    question.Id = question.Title.Replace(" ", "").ToLower();

                if (question is TextQuestion textQuestion)
                {
                    if (string.IsNullOrWhiteSpace(textQuestion.Placeholder))
                        textQuestion.Placeholder = textQuestion.Title;
                }
            }
        }

        public bool ExistsAndAvailable(string id)
        {
            Form form = GetById(id);
            return form != null && form.Available && !form.Hidden;
        }

        public bool Exists(string id)
        {
            Form form = GetById(id);
            return form != null;
        }
    }
}
