using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface IResultDetailRepository : IRepository<ResultDetail>
    {
    }

    public class ResultDetailRepository : RepositoryBase<ResultDetail>,
                                          IResultDetailRepository
    {
        public ResultDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
