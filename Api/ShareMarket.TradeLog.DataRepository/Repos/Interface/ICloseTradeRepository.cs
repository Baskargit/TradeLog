using ShareMarket.TradeLog.DataEntities;
using ShareMarket.TradeLog.DataRepository.Generic.Interface;

namespace ShareMarket.TradeLog.DataRepository.Interface
{
    public interface ICloseTradeRepository : IGenericRepository<CloseTrade,long>
    {
        
    }
}