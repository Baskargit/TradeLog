using ShareMarket.TradeLog.DataEntities;
using ShareMarket.TradeLog.DataRepository.Generic.Implementation;
using ShareMarket.TradeLog.DataRepository.Interface;

namespace ShareMarket.TradeLog.DataRepository.Implementation
{
    public class SymbolTypeRepository : GenericRepository<SymbolType, int>, ISymbolTypeRepository
    {
        public SymbolTypeRepository(TradeLogDbContext tradeLogDbContext) : base(tradeLogDbContext)  {   }
    }
}