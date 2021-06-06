using System;
using System.Collections.Generic;

namespace ShareMarket.TradeLog.DataEntities
{
    public class SymbolType
    {
        public SymbolType()
        {
            Symbol = new HashSet<Symbol>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int MarketId { get; set; }

        public virtual Market Market { get; set; }
        public virtual ICollection<Symbol> Symbol { get; set; }
    }
}