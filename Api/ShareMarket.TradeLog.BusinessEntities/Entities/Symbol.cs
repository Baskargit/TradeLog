using System;

namespace ShareMarket.TradeLog.BusinessEntities
{
    public class Symbol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int SymbolTypeId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
