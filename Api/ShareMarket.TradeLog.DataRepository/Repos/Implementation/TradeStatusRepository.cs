using ShareMarket.TradeLog.DataEntities;
using ShareMarket.TradeLog.DataRepository.Generic.Implementation;
using ShareMarket.TradeLog.DataRepository.Interface;

namespace ShareMarket.TradeLog.DataRepository.Implementation
{
    public class TradeStatusRepository : GenericRepository<TradeStatus, int>, ITradeStatusRepository
    {
        public TradeStatusRepository(TradeLogDbContext tradeLogDbContext) : base(tradeLogDbContext)  {   }
    }
}