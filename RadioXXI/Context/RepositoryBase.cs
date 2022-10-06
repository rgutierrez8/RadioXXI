using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RadioXXI.Context
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RadioContext RepositoryContext { get; set; }

        public RepositoryBase(RadioContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            this.RepositoryContext.SaveChanges();
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> queryable = this.RepositoryContext.Set<T>();
            if (includes != null)
            {
                queryable = includes(queryable);
            }

            return queryable.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }
    }
}
