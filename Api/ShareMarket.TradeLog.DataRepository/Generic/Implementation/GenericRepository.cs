using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShareMarket.TradeLog.DataRepository.Generic.Interface;

namespace ShareMarket.TradeLog.DataRepository.Generic.Implementation
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        protected TradeLogDbContext DbContext {get;set;}

        public GenericRepository(TradeLogDbContext tradeLogDbContext)
        {
            DbContext = tradeLogDbContext;
        }

        public IQueryable<TModel> Get()
        {
            return DbContext.Set<TModel>().AsNoTracking();
        }

        public IQueryable<TModel> GetWith(Expression<Func<TModel, bool>> expression)
        {
            return DbContext.Set<TModel>().Where(expression).AsNoTracking();
        }

        public IQueryable<TModel> GetWithTracking()
        {
            return DbContext.Set<TModel>();
        }

        public IQueryable<TModel> GetWithTracking(Expression<Func<TModel, bool>> expression)
        {
            return DbContext.Set<TModel>().Where(expression);
        }

        public void Add(TModel entity)
        {
            DbContext.Set<TModel>().Add(entity);
            DbContext.SaveChanges();
        }

        public void Update(TModel entity)
        {
            DbContext.Attach(entity).State = EntityState.Modified;
            DbContext.Set<TModel>().Update(entity);
            DbContext.SaveChanges();
        }

        public void Delete(TModel entity)
        {
            DbContext.Set<TModel>().Remove(entity);
            DbContext.SaveChanges();
        }

        public bool IsExists(Expression<Func<TModel, bool>> expression)
        {
            return DbContext.Set<TModel>().Any(expression);
        }
    }
}
