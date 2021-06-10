using ShareMarket.TradeLog.DataEntities;
using ShareMarket.TradeLog.DataRepository.Generic.Implementation;
using ShareMarket.TradeLog.DataRepository.Interface;

namespace ShareMarket.TradeLog.DataRepository.Implementation
{
    public class TradeResultRepository : GenericRepository<TradeResult, int>, ITradeResultRepository
    {
        public TradeResultRepository(TradeLogDbContext tradeLogDbContext) : base(tradeLogDbContext)  {   }
    }
}