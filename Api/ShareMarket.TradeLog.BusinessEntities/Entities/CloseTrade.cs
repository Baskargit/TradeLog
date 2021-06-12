using System;

namespace ShareMarket.TradeLog.BusinessEntities
{
    public class CloseTrade
    {
        public long Id { get; set; }
        public decimal UnitPrice { get; set; }
        public uint Quantity { get; set; }
        public DateTime Time { get; set; }
        public long OpenTradeId { get; set; }
        public int TradeTypeId { get; set; }
        public int TradeResultId { get; set; }
    }
}
