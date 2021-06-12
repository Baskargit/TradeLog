using System;
using System.Collections.Generic;

namespace ShareMarket.TradeLog.DataEntities
{
    public class OpenTrade
    {
        public OpenTrade()
        {
            CloseTrade = new HashSet<CloseTrade>();
        }

        public long Id { get; set; }
        public decimal UnitPrice { get; set; }
        public uint Quantity { get; set; }
        public DateTime Time { get; set; }
        public int SymbolId { get; set; }
        public int TradeTypeId { get; set; }
        public int TradeStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Symbol Symbol { get; set; }
        public virtual TradeStatus TradeStatus { get; set; }
        public virtual TradeType TradeType { get; set; }
        public virtual ICollection<CloseTrade> CloseTrade { get; set; }
    }
}