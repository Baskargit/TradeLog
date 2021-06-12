using System;

namespace ShareMarket.TradeLog.DataEntities
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
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual OpenTrade OpenTrade { get; set; }
        public virtual TradeResult TradeResult { get; set; }
        public virtual TradeType TradeType { get; set; }
    }
}
