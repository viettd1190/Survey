using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }

    public class UserRepository : RepositoryBase<User>,
                                  IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
