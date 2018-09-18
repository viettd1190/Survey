namespace Survey.Data.Infrastructure
{
    public class DbFactory : Disposable,
                             IDbFactory
    {
        private SurveyDbContext _dbContext;

        #region IDbFactory Members

        public SurveyDbContext Init()
        {
            return _dbContext ?? (_dbContext = new SurveyDbContext());
        }

        #endregion

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
