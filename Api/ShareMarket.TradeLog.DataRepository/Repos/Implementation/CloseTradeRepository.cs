using ShareMarket.TradeLog.DataEntities;
using ShareMarket.TradeLog.DataRepository.Generic.Implementation;
using ShareMarket.TradeLog.DataRepository.Interface;

namespace ShareMarket.TradeLog.DataRepository.Implementation
{
    public class CloseTradeRepository : GenericRepository<CloseTrade, long>, ICloseTradeRepository
    {
        public CloseTradeRepository(TradeLogDbContext tradeLogDbContext) : base(tradeLogDbContext)  {   }
    }
}