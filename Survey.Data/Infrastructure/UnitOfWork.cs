namespace Survey.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;

        private SurveyDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public SurveyDbContext DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        #region IUnitOfWork Members

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        #endregion
    }
}
