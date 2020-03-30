using Microsoft.Extensions.Options;
using QuizForms.Data.Models.Livestream;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories.Implementations
{
    /// <summary>
    /// Livestream repository
    /// </summary>
    public sealed class LivestreamRepository : QuizFormsBaseRepository, ILivestreamRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public LivestreamRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }

        /// <summary>
        /// Create a Twitch livestream
        /// </summary>
        /// <param name="title">livestream (display) title</param>
        /// <param name="channel">Twitch channel name</param>
        /// <param name="visible">initial visible state</param>
        /// <returns>livestream id</returns>
        public Guid CreateTwitchLivestream(string title, string channel, bool visible)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a YouTube livestream
        /// </summary>
        /// <param name="title">livestream (display) title</param>
        /// <param name="url">Youtube iframe URL</param>
        /// <param name="visible">initial visible state</param>
        /// <returns>livestream id</returns>
        public Guid CreateYoutubeLivestream(string title, string channel, bool visible)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a livestream
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <returns>
        ///     true = found and deleted,
        ///     false = not found
        /// </returns>
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a livestream by its id
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <returns>livestream</returns>
        public Livestream Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all livestreams
        /// </summary>
        /// <returns>list with livestreams</returns>
        public List<Livestream> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all visible livestreams
        /// </summary>
        /// <returns>list with livestreams</returns>
        public List<Livestream> GetAllVisible()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the title of a livestream
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <param name="title">livestream (display) title</param>
        public void UpdateTitle(Guid id, string title)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the visible state of a livestream
        /// </summary>
        /// <param name="id">livestream id</param>
        /// <param name="visible">visible state</param>
        public void UpdateVisible(Guid id, bool visible)
        {
            throw new NotImplementedException();
        }
    }
}
