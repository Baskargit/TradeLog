using System;
using System.Collections.Generic;

namespace ShareMarket.TradeLog.DataEntities
{
    public class TradeStatus
    {
        public TradeStatus()
        {
            OpenTrade = new HashSet<OpenTrade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<OpenTrade> OpenTrade { get; set; }
    }
}