using System;

namespace ShareMarket.TradeLog.BusinessEntities
{
    public class SymbolType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int MarketId { get; set; }
    }
}
