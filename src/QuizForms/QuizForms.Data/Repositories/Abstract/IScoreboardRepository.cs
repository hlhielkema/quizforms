using QuizForms.Data.Models.Scoreboard;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories.Abstract
{
    /// <summary>
    /// Scoreboard repository
    /// </summary>
    public interface IScoreboardRepository
    {
        /// <summary>
        /// Get a scoreboard
        /// </summary>
        /// <param name="scoreboardId">scoreboard id</param>
        /// <returns>scoreboard or null if not found</returns>
        Scoreboard Get(Guid scoreboardId);

        /// <summary>
        /// Get all scoreboards
        /// </summary>
        /// <returns>scoreboards list</returns>
        List<ScoreboardInfo> GetAll();

        /// <summary>
        /// Create a scoreboard
        /// </summary>
        /// <param name="title">scoreboard title</param>
        /// <param name="rounds">rounds list</param>
        /// <returns>scoreboard id</returns>
        Guid Create(string title, List<ScoreboardRound> rounds);

        /// <summary>
        /// Update a scoreboard
        /// </summary>
        /// <param name="scoreboard">scoreboard</param>
        void Update(Scoreboard scoreboard);

        /// <summary>
        /// Delete a scoreboard
        /// </summary>
        /// <param name="scoreboardId">scoreboard id </param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        bool Delete(Guid scoreboardId);
    }
}
