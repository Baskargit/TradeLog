using System;
using FluentValidation;
using ShareMarket.TradeLog.BusinessEntities;
using ShareMarket.TradeLog.BusinessEntities.Enums;

namespace ShareMarket.TradeLog.BusinessRules
{
    public class CloseTradeValidator : AbstractValidator<CloseTrade>
    {
        public CloseTradeValidator(int dbOperation, bool isCloseTradeIdExists = false)
        {
            if (!dbOperation.Equals(DbOperation.READ))
            {
                
            }

            if (!dbOperation.Equals(DbOperation.CREATE))
            {
                RuleFor(x => x.Id).Must(x => isCloseTradeIdExists).WithErrorCode("1001").WithMessage("Invalid 'Id' provided");
            }            
        }
    }
}
