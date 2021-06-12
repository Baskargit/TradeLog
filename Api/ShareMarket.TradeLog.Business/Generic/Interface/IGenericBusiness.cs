using System;
using System.Collections.Generic;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Business.Generic.Interface
{
    public interface IGenericBusiness<Model,ModelPrimaryKey> where Model : class where ModelPrimaryKey : struct
    {
        Biz<List<Model>> GetAll();
        Biz<Model> Get(ModelPrimaryKey id);
        Biz<Model> Add(Model entity);
        Biz<Model> Update(Model entity);
        Biz<Model> Delete(ModelPrimaryKey id);
    }
}
