using QuizForms.Data.Models.Livestream;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    /// <summary>
    /// Livestream repository
    /// </summary>
    public interface ILivestreamRepository
    {
        /// <summary>
        /// Get a livestream by its id
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <returns>livestream</returns>
        Livestream Get(Guid id);

        /// <summary>
        /// Get all livestreams
        /// </summary>
        /// <returns>list with livestreams</returns>
        List<Livestream> GetAll();

        /// <summary>
        /// Get all visible livestreams
        /// </summary>
        /// <returns>list with livestreams</returns>
        List<Livestream> GetAllVisible();

        /// <summary>
        /// Create a Twitch livestream
        /// </summary>
        /// <param name="title">livestream (display) title</param>
        /// <param name="channel">Twitch channel name</param>
        /// <param name="visible">initial visible state</param>
        /// <returns>livestream id</returns>
        Guid CreateTwitchLivestream(string title, string channel, bool visible);

        /// <summary>
        /// Create a YouTube livestream
        /// </summary>
        /// <param name="title">livestream (display) title</param>
        /// <param name="url">Youtube iframe URL</param>
        /// <param name="visible">initial visible state</param>
        /// <returns>livestream id</returns>
        Guid CreateYoutubeLivestream(string title, string url, bool visible);

        /// <summary>
        /// Update the visible state of a livestream
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <param name="visible">visible state</param>
        void UpdateVisible(Guid id, bool visible);

        /// <summary>
        /// Update the title of a livestream
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <param name="title">livestream (display) title</param>
        void UpdateTitle(Guid id, string title);

        /// <summary>
        /// Delete a livestream
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <returns>
        ///     true = found and deleted,
        ///     false = not found
        /// </returns>
        bool Delete(Guid id);
    }
}
