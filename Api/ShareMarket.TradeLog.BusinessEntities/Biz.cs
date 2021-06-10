using System.Collections.Generic;

namespace ShareMarket.TradeLog.BusinessEntities
{
    public class Biz<T> where T : class
    {
        public T Data { get; set; }
        public bool IsError { get; private set; }
        public List<Error> Errors { get; set; }
        
        public void AddError(string errorCode, string errorMessage)
        {
            if (Errors == null)
            {
                Errors = new List<Error>();
                this.IsError = true;
            }

            Errors.Add(Error.GetError(errorCode,errorMessage));
        }
    }
}