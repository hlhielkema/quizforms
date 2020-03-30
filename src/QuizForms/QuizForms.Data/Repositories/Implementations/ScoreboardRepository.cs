using Microsoft.Extensions.Options;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories.Implementations
{
    public sealed class ScoreboardRepository : QuizFormsBaseRepository, IScoreboardRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public ScoreboardRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }
    }
}
