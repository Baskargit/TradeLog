namespace ShareMarket.TradeLog.BusinessEntities
{
    public class Error
    {
        public Error() {   }

        public Error(string code, string message)
        {
            Code = code; Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }

        public static Error GetError(string code, string message)
        {
            return new Error() { Code = code, Message = message };
        }
    }
}