using QuizForms.Data.Models.Answers;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    public interface IQuizFormAnswersRepository
    {
        /// <summary>
        /// Create a set with answers
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="team">team name</param>
        /// <param name="answers">answers dictionary</param>
        /// <returns>form answers set</returns>
        Guid Create(string formId, string team, Dictionary<string, string> answers);

        /// <summary>
        /// Get all form answers sets
        /// </summary>
        /// <param name="formId">form id</param>
        /// <returns>list with form answer sets</returns>
        List<FormAnswersSet> GetAll(string formId);

        /// <summary>
        /// Get a form answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>form answers set or null if not found</returns>
        FormAnswersSet Get(string formId, Guid answersId);

        /// <summary>
        /// Delete a form answers set.
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        bool Delete(string formId, Guid answersId);
    }
}
