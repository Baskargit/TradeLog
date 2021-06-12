using System;

namespace ShareMarket.TradeLog.BusinessEntities
{
    public class OpenTrade
    {
        public long Id { get; set; }
        public decimal UnitPrice { get; set; }
        public uint Quantity { get; set; }
        public DateTime Time { get; set; }
        public int SymbolId { get; set; }
        public int TradeTypeId { get; set; }
        public int TradeStatusId { get; set; }
    }
}
