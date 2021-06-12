using System;
using FluentValidation;
using ShareMarket.TradeLog.BusinessEntities;
using ShareMarket.TradeLog.BusinessEntities.Enums;

namespace ShareMarket.TradeLog.BusinessRules
{
    public class SymbolValidator : AbstractValidator<Symbol>
    {
        public SymbolValidator(int dbOperation, bool isSymbolIdExists = false)
        {
            if (!dbOperation.Equals(DbOperation.READ))
            {
                RuleFor(x => x.Name).Must(x => string.IsNullOrWhiteSpace(x)).WithErrorCode("1002").WithMessage("Name cannot be null, empty or full of whitespaces");
                RuleFor(x => x.Code).Must(x => string.IsNullOrWhiteSpace(x)).WithErrorCode("1003").WithMessage("Code cannot be null, empty or full of whitespaces");
            }

            if (!dbOperation.Equals(DbOperation.CREATE))
            {
                RuleFor(x => x.Id).Must(x => isSymbolIdExists).WithErrorCode("1001").WithMessage("Invalid 'Id' provided");
            }            
        }
    }
}
