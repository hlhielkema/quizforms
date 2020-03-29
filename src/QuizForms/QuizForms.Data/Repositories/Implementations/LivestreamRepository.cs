using Microsoft.Extensions.Options;
using QuizForms.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizForms.Data.Repositories.Implementations
{
    public sealed class LivestreamRepository : QuizFormsBaseRepository, ILivestreamRepository
    {
        public LivestreamRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }
    }
}
