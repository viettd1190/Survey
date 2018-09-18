using Survey.Data.Infrastructure;

namespace Survey.Data.Repositories
{
    public interface ISurveyRepository : IRepository<Model.Models.Survey>
    {
    }

    public class SurveyRepository : RepositoryBase<Model.Models.Survey>,
                                    ISurveyRepository
    {
        public SurveyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
