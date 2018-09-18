using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface IResultRepository : IRepository<Result>
    {
    }

    public class ResultRepository : RepositoryBase<Result>,
                                    IResultRepository
    {
        public ResultRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
