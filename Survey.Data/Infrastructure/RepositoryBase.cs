using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Survey.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class

    {
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            DbSet = DbContext.Set<T>();
        }

        #region Properties

        protected SurveyDbContext DataContext;

        protected readonly IDbSet<T> DbSet;

        protected IDbFactory DbFactory { get; }

        protected SurveyDbContext DbContext => DataContext ?? (DataContext = DbFactory.Init());

        #endregion

        #region Implementation

        public virtual T Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            DataContext.Entry(entity)
                       .State = EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            return DbSet.Remove(entity);
        }

        public virtual T Delete(int id)
        {
            T entity = DbSet.Find(id);
            return DbSet.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = DbSet.Where(where)
                                          .AsEnumerable();
            foreach (T obj in objects)
                DbSet.Remove(obj);
        }

        public virtual T GetSingleById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where,
                                              string includes)
        {
            return DbSet.Where(where)
                        .ToList();
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return DbSet.Count(where);
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if(includes != null
               && includes.Any())
            {
                DbQuery<T> query = DataContext.Set<T>()
                                              .Include(includes.First());
                foreach (string include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return DataContext.Set<T>()
                              .AsQueryable();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression,
                                      string[] includes = null)
        {
            if(includes != null
               && includes.Any())
            {
                DbQuery<T> query = DataContext.Set<T>()
                                              .Include(includes.First());
                foreach (string include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }

            return DataContext.Set<T>()
                              .FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate,
                                               string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if(includes != null
               && includes.Any())
            {
                DbQuery<T> query = DataContext.Set<T>()
                                              .Include(includes.First());
                foreach (string include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where(predicate)
                            .AsQueryable();
            }

            return DataContext.Set<T>()
                              .Where(predicate)
                              .AsQueryable();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                                                     out int total,
                                                     int index = 0,
                                                     int size = 20,
                                                     string[] includes = null)
        {
            index--;
            if(index < 0)
            {
                index = 0;
            }

            if(size <= 0)
            {
                size = 20;
            }

            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if(includes != null
               && includes.Any())
            {
                DbQuery<T> query = DataContext.Set<T>()
                                              .Include(includes.First());
                foreach (string include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null
                                ? query.Where(predicate)
                                       .AsQueryable()
                                : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null
                                ? DataContext.Set<T>()
                                             .Where(predicate)
                                             .AsQueryable()
                                : DataContext.Set<T>()
                                             .AsQueryable();
            }

            total = _resetSet.Count();

            _resetSet = skipCount == 0
                            ? orderBy(_resetSet)
                                    .Take(size)
                            : orderBy(_resetSet)
                                    .Skip(skipCount)
                                    .Take(size);

            return _resetSet.AsQueryable();
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return DataContext.Set<T>()
                              .Count(predicate) > 0;
        }

        #endregion
    }
}
