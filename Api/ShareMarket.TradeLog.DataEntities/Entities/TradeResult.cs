using System;
using System.Collections.Generic;

namespace ShareMarket.TradeLog.DataEntities
{
    public class TradeResult
    {
        public TradeResult()
        {
            CloseTrade = new HashSet<CloseTrade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CloseTrade> CloseTrade { get; set; }
    }
}