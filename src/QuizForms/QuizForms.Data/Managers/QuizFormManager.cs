using Newtonsoft.Json;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Models.Questions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuizForms.Data.Managers
{
    public static class QuizFormManager
    {       
        public static List<FormInfo> GetFormsFromDirectory(string directoryPath)
        {
            List<FormInfo> results = new List<FormInfo>();
            foreach (string filename in Directory.GetFiles(directoryPath))
            {
                string contents = File.ReadAllText(filename);
                FormInfo form = JsonConvert.DeserializeObject<FormInfo>(contents);
                results.Add(form);
            }
            return results;
        }

        public static Form GetFormByid(string directoryPath, string id)
        {
            foreach (string filename in Directory.GetFiles(directoryPath))
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

        public static Form ReadFormFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                string contents = File.ReadAllText(filename);
                Form form = JsonConvert.DeserializeObject<Form>(contents);
                ValidateAndCorrectForm(form);
                return form;
            }
            return null;
        }

        private static void ValidateAndCorrectForm(Form form)
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
    }
}
