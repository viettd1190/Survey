using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface IFaqRepository : IRepository<Faq>
    {
    }

    public class FaqRepository : RepositoryBase<Faq>,
                                 IFaqRepository
    {
        public FaqRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
