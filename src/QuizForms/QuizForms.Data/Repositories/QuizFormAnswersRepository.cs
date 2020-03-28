using Microsoft.Extensions.Options;

namespace QuizForms.Data.Repositories
{
    public sealed class QuizFormAnswersRepository : QuizFormsBaseRepository, IQuizFormAnswersRepository
    {
        public QuizFormAnswersRepository(IOptions<QuizFormsSettings> settings) 
            : base(settings)
        { }
    }
}
