using System;

namespace Survey.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        SurveyDbContext Init();
    }
}
