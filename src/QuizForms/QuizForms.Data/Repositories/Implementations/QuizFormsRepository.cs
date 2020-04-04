using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Models.Questions;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;


namespace QuizForms.Data.Repositories.Implementations
{
    // TODO: Improve this repo


    /// <summary>
    ///  Quiz forms repository
    /// </summary>
    public sealed class QuizFormsRepository : QuizFormsBaseRepository, IQuizFormsRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
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

        /// <summary>
        /// Update if a form is available.
        /// </summary>
        /// <param name="id">form id</param>
        /// <param name="available">new available state</param>        
        public void UpdateAvailable(string id, bool available)
        {
            foreach (string filename in Directory.GetFiles(FormsPath))
            {
                // Read the form from the file
                string json = File.ReadAllText(filename);
                Form form = JsonConvert.DeserializeObject<Form>(json);

                if (form.Id == id)
                {
                    // Update the state
                    form.Available = available;

                    // Write the form to the file
                    json = JsonConvert.SerializeObject(form);
                    File.WriteAllText(filename, json);

                    return;
                }
            }

            throw new Exception("Form not found");
        }

        /// <summary>
        /// Update if a form is hidden.
        /// </summary>
        /// <param name="id">form id</param>
        /// <param name="hidden">new hidden state</param>        
        public void UpdateHidden(string id, bool hidden)
        {
            foreach (string filename in Directory.GetFiles(FormsPath))
            {
                // Read the form from the file
                string json = File.ReadAllText(filename);
                Form form = JsonConvert.DeserializeObject<Form>(json);

                if (form.Id == id)
                {
                    // Update the state
                    form.Hidden = hidden;

                    // Write the form to the file
                    json = JsonConvert.SerializeObject(form);
                    File.WriteAllText(filename, json);

                    return;
                }
            }

            throw new Exception("Form not found");
        }
    }
}
