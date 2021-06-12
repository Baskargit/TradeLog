using System;
using FluentValidation;
using ShareMarket.TradeLog.BusinessEntities;
using ShareMarket.TradeLog.BusinessEntities.Enums;

namespace ShareMarket.TradeLog.BusinessRules
{
    public class OpenTradeValidator : AbstractValidator<OpenTrade>
    {
        public OpenTradeValidator(int dbOperation, bool isOpenTradeIdExists = false)
        {
            if (!dbOperation.Equals(DbOperation.READ))
            {
                
            }

            if (!dbOperation.Equals(DbOperation.CREATE))
            {
                RuleFor(x => x.Id).Must(x => isOpenTradeIdExists).WithErrorCode("1001").WithMessage("Invalid 'Id' provided");
            }            
        }
    }
}
