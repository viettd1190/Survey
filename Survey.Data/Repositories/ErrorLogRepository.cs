using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
    }

    public class LogRepository : RepositoryBase<Log>,
                                 ILogRepository
    {
        public LogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
