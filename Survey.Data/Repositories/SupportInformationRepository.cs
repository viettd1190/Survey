using Survey.Data.Infrastructure;
using Survey.Model.Models;

namespace Survey.Data.Repositories
{
    public interface ISupportInformationRepository : IRepository<SupportInformation>
    {
    }

    public class SupportInformationRepository : RepositoryBase<SupportInformation>,
                                                ISupportInformationRepository
    {
        public SupportInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
