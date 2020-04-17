using QuizForms.Data.Models.Forms;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    /// <summary>
    /// Quiz forms repository
    /// </summary>
    public interface IQuizFormsRepository
    {
        /// <summary>
        /// Get if a form exists and is available.
        /// This also excludes hidden forms.
        /// </summary>
        /// <param name="id">form id</param>
        /// <returns>true=exists, false=does not exist</returns>
        bool ExistsAndAvailable(string id);

        /// <summary>
        /// Get if a form exists.
        /// </summary>
        /// <param name="id">form id</param>
        /// <returns>true=exists, false=does not exist</returns>
        bool Exists(string id);

        /// <summary>
        /// Get all forms that are visible.
        /// This includes unavailable forms.
        /// </summary>
        /// <returns>list with forms</returns>
        List<FormInfo> GetAllVisible();

        /// <summary>
        /// Get all forms.
        /// </summary>
        /// <returns>list with forms</returns>
        List<FormInfo> GetAll();

        /// <summary>
        /// Get the last time the forms collection changed.
        /// </summary>
        /// <returns>datetime</returns>
        DateTime GetLastChanged();

        /// <summary>
        /// Get a form by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>form or null if not found</returns>
        Form GetById(string id);

        /// <summary>
        /// Update if a form is available.
        /// </summary>
        /// <param name="id">form id</param>
        /// <param name="available">new available state</param>        
        void UpdateAvailable(string id, bool available);

        /// <summary>
        /// Update if a form is hidden.
        /// </summary>
        /// <param name="id">form id</param>
        /// <param name="hidden">new hidden state</param>        
        void UpdateHidden(string id, bool hidden);
    }
}
