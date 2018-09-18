using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
    }

    public class QuestionRepository : RepositoryBase<Question>,
                                      IQuestionRepository
    {
        public QuestionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
