using ShareMarket.TradeLog.DataEntities;
using ShareMarket.TradeLog.DataRepository.Generic.Implementation;
using ShareMarket.TradeLog.DataRepository.Interface;

namespace ShareMarket.TradeLog.DataRepository.Implementation
{
    public class OpenTradeRepository : GenericRepository<OpenTrade, long>, IOpenTradeRepository
    {
        public OpenTradeRepository(TradeLogDbContext tradeLogDbContext) : base(tradeLogDbContext)  {   }
    }
}