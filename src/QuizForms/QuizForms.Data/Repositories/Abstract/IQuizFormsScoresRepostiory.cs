using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    /// <summary>
    /// Quiz forms scores repository
    /// </summary>
    public interface IQuizFormsScoresRepostiory
    {
        /// <summary>
        /// Get the scores for answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>dictionary with a score for each answer</returns>
        Dictionary<string, int> GetScore(string formId, Guid answersId);

        /// <summary>
        /// Get the total score for an answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>the total score or null if not found</returns>
        int? GetTotalScore(string formId, Guid answersId);

        /// <summary>
        /// Get the total score for all answer sets
        /// </summary>
        /// <param name="formId">form id</param>        
        /// <returns>dictionary with answer sets id's and their total score</returns>
        Dictionary<Guid, int> GetTotalScores(string formId);

        /// <summary>
        /// Update the score for an answer set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <param name="scores">dictionary with a score for each answer</param>
        void UpdateScore(string formId, Guid answersId, Dictionary<string, int> scores);

        /// <summary>
        /// Delete the score for an answers set
        /// </summary>
        /// <param name="formId">form id</param>
        /// <param name="answersId">answers set id</param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        bool DeleteScore(string formId, Guid answersId);
    }
}
