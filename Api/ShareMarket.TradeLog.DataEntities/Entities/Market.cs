using System;
using System.Collections.Generic;

namespace ShareMarket.TradeLog.DataEntities
{
    public class Market
    {
        public Market()
        {
            SymbolType = new HashSet<SymbolType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<SymbolType> SymbolType { get; set; }
    }
}