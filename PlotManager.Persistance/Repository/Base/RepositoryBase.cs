using Microsoft.EntityFrameworkCore;
using PlotManager.Application.Repositories.Base;
using PlotManager.Infrastructure.Persistance;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlotManager.Infrastructure.Persistance
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PlotManagerDbContext RepositoryContext { get; }

        public RepositoryBase(PlotManagerDbContext plotManagerDbContext)
        {
            RepositoryContext = plotManagerDbContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}