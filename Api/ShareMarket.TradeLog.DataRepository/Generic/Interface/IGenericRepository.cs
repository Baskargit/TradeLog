using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShareMarket.TradeLog.DataRepository.Generic.Interface
{
    public interface IGenericRepository<TModel,TModelPrimaryKey> where TModel : class where TModelPrimaryKey : struct
    {
        IQueryable<TModel> Get();
        IQueryable<TModel> GetWith(Expression<Func<TModel,bool>> expression);
        void Add(TModel entity);
        void Update(TModel entity);
        void Delete(TModel entity);

        // Helper functions
        bool IsExists(Expression<Func<TModel,bool>> expression);
    }
}
