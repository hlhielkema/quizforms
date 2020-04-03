﻿using QuizForms.Data.Models.Forms;
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
        /// Get a form by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>form</returns>
        Form GetById(string id);
    }
}