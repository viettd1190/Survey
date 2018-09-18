using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
    }

    public class AnswerRepository : RepositoryBase<Answer>,
                                    IAnswerRepository
    {
        public AnswerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
